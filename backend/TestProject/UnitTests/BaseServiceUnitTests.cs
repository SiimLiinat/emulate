using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Base.Services;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Game = Domain.App.Game;


namespace TestProject.UnitTests
{
    public class BaseServiceUnitTests
    {
        private readonly AppDbContext _ctx;
        private readonly IBaseRepository<DAL.App.DTO.Emulator> _baseRepository;
        private readonly AppUnitOfWork _uow;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IMapper _mapper;
        private readonly Contracts.DAL.Base.Mappers.IBaseMapper<DAL.App.DTO.Emulator, Domain.App.Emulator> _baseMapper;
        private readonly BaseEntityService<AppUnitOfWork, IBaseRepository<DAL.App.DTO.Emulator>, BLL.App.DTO.Emulator,
            DAL.App.DTO.Emulator> _baseEntityService;

        public BaseServiceUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper; 
            
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.App.Emulator, DAL.App.DTO.Emulator>()
                    .ForMember(e => e.Platform, i => i.Ignore())
                    .ForMember(e => e.Compatibilities, i => i.Ignore());
                cfg.CreateMap<DAL.App.DTO.Emulator, Domain.App.Emulator>()
                    .ForMember(e => e.Platform, i => i.Ignore())
                    .ForMember(e => e.Compatibilities, i => i.Ignore());
                cfg.CreateMap<DAL.App.DTO.Emulator, BLL.App.DTO.Emulator>()
                    .ForMember(e => e.Platform, i => i.Ignore())
                    .ForMember(e => e.Compatibilities, i => i.Ignore());
                cfg.CreateMap<BLL.App.DTO.Emulator, DAL.App.DTO.Emulator>()
                    .ForMember(e => e.Platform, i => i.Ignore())
                    .ForMember(e => e.Compatibilities, i => i.Ignore());
                cfg.CreateMap<BLL.App.DTO.Emulator, Emulator>()
                    .ForMember(e => e.Platform, i => i.Ignore())
                    .ForMember(e => e.Compatibilities, i => i.Ignore());
                cfg.CreateMap<Domain.App.Emulator, BLL.App.DTO.Emulator>()
                    .ForMember(e => e.Platform, i => i.Ignore())
                    .ForMember(e => e.Compatibilities, i => i.Ignore());
            });
            
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
            
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            // var logger = loggerFactory.CreateLogger<>();
            
            _uow = new AppUnitOfWork(_ctx, _mapper);
            _baseMapper = new DAL.App.EF.Mappers.BaseMapper<DAL.App.DTO.Emulator, Domain.App.Emulator>(_mapper);
            _baseRepository = new BaseRepository<DAL.App.DTO.Emulator, Domain.App.Emulator, AppDbContext >(_ctx, _baseMapper);
            _baseEntityService = new BaseEntityService<AppUnitOfWork, IBaseRepository<DAL.App.DTO.Emulator>, BLL.App.DTO.Emulator,
                DAL.App.DTO.Emulator>(_uow, _baseRepository, new BLL.App.Mappers.EmulatorMapper(_mapper));
            _ctx.ChangeTracker.Clear();
        }
        
        [Fact]
        public async Task Action_Add()
        {
            var result = await _baseEntityService.GetAllAsync();
            Assert.Empty(result);
            
            var guid = await SeedData();
            var emulator = await _baseEntityService.FirstOrDefaultAsync(guid);
            Assert.NotNull(emulator);
            
            emulator!.Id = Guid.NewGuid();
            emulator.Name = Guid.NewGuid().ToString("n")[..8];
            emulator.Url = Guid.NewGuid().ToString("n")[..8];
            
            var addedEmulator = _baseEntityService.Add(emulator);
            Assert.NotNull(addedEmulator);
        }

        [Fact]
        public async Task Action_GetUpdatedEntityAfterSaveChanges()
        {
            var guid = await SeedData();
            var result = await _baseEntityService.FirstOrDefaultAsync(guid);
            Assert.NotNull(result);
            _ctx.ChangeTracker.Clear();
            _baseEntityService.Add(result!);
            var updatedEntity = _baseEntityService.GetUpdatedEntityAfterSaveChanges(result!);
            Assert.NotNull(updatedEntity);
            Assert.NotNull(_baseEntityService.FirstOrDefaultAsync(updatedEntity.Id));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task Action_GetAll(int count)
        {
            await SeedData(count);
            var result = await _baseEntityService.GetAllAsync();
            Assert.Equal(count, result.Count());
        }
        
        [Fact]
        public async Task Action_Get()
        {
            var result = await _baseEntityService.FirstOrDefaultAsync(Guid.Empty);
            Assert.Null(result);
            
            var guid = await SeedData();
            result = await _baseEntityService.FirstOrDefaultAsync(guid);
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task Action_Exists()
        {
            var result = await _baseEntityService.ExistsAsync(Guid.Empty);
            Assert.False(result);
            
            var guid = await SeedData();
            result = await _baseEntityService.ExistsAsync(guid);
            Assert.True(result);
        }
        
        [Fact]
        public async Task Action_Remove()
        {
            var guid = await SeedData(5);
            Assert.Equal(5, _baseEntityService.GetAllAsync().Result.Count());
            var result = await _baseEntityService.FirstOrDefaultAsync(guid);
            Assert.NotNull(result);
            _ctx.ChangeTracker.Clear();
            var removedEntity = _baseEntityService.Remove(result!);
            Assert.NotNull(removedEntity);
            Assert.DoesNotContain(removedEntity, _baseEntityService.GetAllAsync().Result);
        }
        
        [Fact]
        public async Task Action_RemoveAsync()
        {
            var guid = await SeedData(5);
            Assert.Equal(5, _baseEntityService.GetAllAsync().Result.Count());
            _ctx.ChangeTracker.Clear();
            var removedEntity = await _baseEntityService.RemoveAsync(guid);
            Assert.NotNull(removedEntity);
            Assert.DoesNotContain(removedEntity, _baseEntityService.GetAllAsync().Result);
        }
        
        [Fact]
        public async Task Action_Update()
        {
            var guid = await SeedData();
            var result = await _baseEntityService.FirstOrDefaultAsync(guid);
            Assert.NotNull(result);
            var resultName = result!.Name;
            result.Name = Guid.NewGuid().ToString("n")[..8];
            _ctx.ChangeTracker.Clear();
            var updatedEntity = _baseEntityService.Update(result);
            Assert.NotNull(updatedEntity);
            Assert.NotEqual(updatedEntity!.Name, resultName);
        }
        
        
        private async Task<Guid> SeedData(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                var company = new Company()
                {
                    Name = Guid.NewGuid().ToString("n")[..8]
                };
                _ctx.Companies.Add(company);
                var platformType = new PlatformType()
                {
                    Type = Guid.NewGuid().ToString("n")[..8],
                };
                _ctx.PlatformTypes.Add(platformType);
                
                var platform = new Platform()
                {
                    Name = "NewPlatform" + i,
                    Code = Guid.NewGuid().ToString("n")[..3],
                    Company = company,
                    PlatformType = platformType
                };
                _ctx.Platforms.Add(platform);
                _ctx.Emulators.Add(new Domain.App.Emulator()
                {
                    Platform = platform,
                    Name = Guid.NewGuid().ToString("n")[..8],
                    Url = Guid.NewGuid().ToString("n")[..8],
                    Picture = Guid.NewGuid().ToString("n")[..8],
                });
                await _ctx.SaveChangesAsync();
                _ctx.Games.Add(new Game()
                {
                    DevCompany = _ctx.Companies.Last(),
                    PubCompany = _ctx.Companies.First(),
                    Name = Guid.NewGuid().ToString("n")[..8],
                    ReleaseDate = DateTime.Now
                });
                await _ctx.SaveChangesAsync();
                _ctx.GameOnPlatforms.Add(new GameOnPlatform()
                {
                    Game = new Game(),
                    Platform = _ctx.Platforms.Last(),
                    ReleaseDate = DateTime.Now
                });
                await _ctx.SaveChangesAsync();
                _ctx.CompatibilityTypes.Add(new CompatibilityType()
                {
                    Type = Guid.NewGuid().ToString("n")[..8],
                    Rating = i + 1
                });
                await _ctx.SaveChangesAsync();
                _ctx.Compatibilities.Add(new Compatibility()
                {
                    GameOnPlatform = _ctx.GameOnPlatforms.Last(),
                    Emulator = _ctx.Emulators.First(),
                    CompatibilityType = _ctx.CompatibilityTypes.Last(),
                    Date = DateTime.Today
                });
                await _ctx.SaveChangesAsync();
            }
            await _ctx.SaveChangesAsync();
            return _ctx.Emulators.First().Id;
        }
    }
}