using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Services;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using DALAppDTO = DAL.App.DTO;

namespace TestProject.UnitTests
{
    public class EmulatorServiceUnitTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IEmulatorRepository _emulatorRepository;
        private readonly AppUnitOfWork _uow;
        private readonly EmulatorService _emulatorService;
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;

        public EmulatorServiceUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            _ctx.ChangeTracker.Clear();
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.App.Emulator, DALAppDTO.Emulator>()
                    .ForMember(e => e.Platform, i => i.Ignore())
                    .ForMember(e => e.Compatibilities, i => i.Ignore());
                cfg.CreateMap<DAL.App.DTO.Emulator, BLL.App.DTO.Emulator>()
                    .ForMember(e => e.Platform, i => i.Ignore())
                    .ForMember(e => e.Compatibilities, i => i.Ignore());
            });
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
            _uow = new AppUnitOfWork(_ctx, _mapper);
            _emulatorRepository = new EmulatorRepository(_ctx, _mapper);
            _emulatorService = new EmulatorService(_uow, _emulatorRepository, _mapper);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task Action_GetAllEmulators(int count)
        {
            await SeedData(count);
            var result = await _emulatorService.GetAllAsync();
            Assert.Equal(count, result.Count());
        }
        
        [Fact]
        public async Task Action_GetEmulator()
        {
            var nullEmulator = await _emulatorService.FirstOrDefaultAsync(Guid.Empty);
            Assert.Null(nullEmulator);
            
            var guid = await SeedData();
            
            // _testOutputHelper.WriteLine(_emulatorRepository.GetAllAsync().Result.Count().ToString());
            // _testOutputHelper.WriteLine(_uow.Emulators.GetAllAsync().Result.Count().ToString());
            _testOutputHelper.WriteLine(_emulatorService.GetAllAsync().Result.Count().ToString());
            foreach (var i in _emulatorService.GetAllAsync().Result)
            {
                _testOutputHelper.WriteLine(guid.ToString());
                _testOutputHelper.WriteLine(i.Id.ToString());
            }
            // var result = await _emulatorService.GetAllAsync();
            // _testOutputHelper.WriteLine(result.Count().ToString());
            // var emulatorId = result.FirstOrDefault()?.Id;
            
            var emulator = await _emulatorService.FirstOrDefaultAsync(guid);
            Assert.NotNull(emulator);
            Assert.True(emulator!.Name != null);
        }
        
        [Fact]
        public async Task Action_GetEmulatorPlatformName()
        {
            var guid = await SeedData();
            var result = await _emulatorService.FirstOrDefaultAsync(guid);
            var platformName = result?.Platform!.Name;
            Assert.NotNull(platformName);
            Assert.Equal("NewPlatform0", platformName);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task Action_GetEmulatorProgramCount(int count)
        {
            var guid = await SeedData(count);
            var result = await _emulatorService.FirstOrDefaultAsync(guid);
            var programCount = result?.Compatibilities!.Count;
            Assert.Equal(count, programCount);
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
                _ctx.CompatibilityTypes.Add(new CompatibilityType()
                {
                    Type = Guid.NewGuid().ToString("n")[..8],
                    Rating = i + 1
                });
                await _ctx.SaveChangesAsync();
                var compatibility = new Compatibility()
                {
                    GameOnPlatform = _ctx.GameOnPlatforms.Last(),
                    Emulator = _ctx.Emulators.First(),
                    CompatibilityType = _ctx.CompatibilityTypes.Last(),
                    Date = DateTime.Today
                };
                _ctx.Compatibilities.Add(compatibility);
                _ctx.Emulators.Add(new Domain.App.Emulator()
                {
                    Platform = platform,
                    Name = Guid.NewGuid().ToString("n")[..8],
                    Url = Guid.NewGuid().ToString("n")[..8],
                    Picture = Guid.NewGuid().ToString("n")[..8],
                    Compatibilities = new List<Compatibility>{compatibility},
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
                await _ctx.SaveChangesAsync();
            }
            await _ctx.SaveChangesAsync();
            return _ctx.Emulators.First().Id;
        }
    }
}