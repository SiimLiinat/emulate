using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using Game = Domain.App.Game;
using DALAppDTO = DAL.App.DTO;

namespace TestProject.UnitTests
{
    public class GameServiceUnitTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IGameRepository _gameRepository;
        private readonly AppUnitOfWork _uow;
        private readonly GameService _gameService;
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;

        public GameServiceUnitTests(ITestOutputHelper testOutputHelper)
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
                cfg.CreateMap<Domain.App.Game, DALAppDTO.Game>()
                    .ForMember(m => m.DevCompany, i => i.Ignore())
                    .ForMember(m => m.PubCompany, i => i.Ignore())
                    .ForMember(m => m.GameGenres, i => i.Ignore())
                    .ForMember(m => m.GameOnPlatforms, i => i.Ignore())
                    .ForMember(m => m.Medias, i => i.Ignore())
                    .ForMember(m => m.Progresses, i => i.Ignore());
                cfg.CreateMap<DAL.App.DTO.Game, BLL.App.DTO.Game>()
                    .ForMember(m => m.DevCompany, i => i.Ignore())
                    .ForMember(m => m.PubCompany, i => i.Ignore())
                    .ForMember(m => m.CompatibilityRank, i => i.Ignore())
                    .ForMember(m => m.CompatibilityType, i => i.Ignore())
                    .ForMember(m => m.CompatibilityPercentage, i => i.Ignore())
                    
                    .ForMember(m => m.GameGenres, i => i.Ignore())
                    .ForMember(m => m.GameOnPlatforms, i => i.Ignore())
                    .ForMember(m => m.Medias, i => i.Ignore());
            });
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
            _uow = new AppUnitOfWork(_ctx, _mapper);
            _gameRepository = new GameRepository(_ctx, _mapper);
            _gameService = new GameService(_uow, _gameRepository, _mapper);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task Action_GetAllGames(int count)
        {
            await SeedData(count);
            var result = await _gameService.GetAllGames();
            Assert.Equal(count, result.Count());
        }
        
        [Fact]
        public async Task Action_GetGame()
        {
            var nullGame = await _gameService.GetGame(Guid.Empty);
            Assert.Null(nullGame);
            
            var guid = await SeedData();
            
            var game = await _gameService.FirstOrDefaultAsync(guid);
            Assert.NotNull(game);
            Assert.True(game!.Name != null);
        }
        
        private async Task<Guid> SeedData(int count = 1)
        {
            Guid guid = Guid.Empty;
            for (int i = 0; i < count; i++)
            {
                var company = new Company()
                {
                    Name = Guid.NewGuid().ToString("n")[..8]
                };
                _ctx.Companies.Add(company);
                guid = Guid.NewGuid();
                var game = new Game()
                {
                    Id = guid,
                    DevCompany = company,
                    PubCompany = company,
                    Name = Guid.NewGuid().ToString("n")[..8],
                    ReleaseDate = new DateTime(2000 + count, count % 12, count % 28)
                };
                _ctx.Games.Add(game);
            }
            await _ctx.SaveChangesAsync();
            return guid;
        }
    }
}