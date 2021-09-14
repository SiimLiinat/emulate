using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class GameService : BaseEntityService<IAppUnitOfWork, IGameRepository, BllAppDTO.Game, DALAppDTO.Game>, IGameService
    {
        public GameService(IAppUnitOfWork serviceUow, IGameRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameMapper(mapper))
        {
        }
        
        public async Task<IEnumerable<BllAppDTO.Game>> GetAllGames()
        {
            var res = (await ServiceUow.Games.GetAllAsync()).Select(c => Mapper.Map(c)).ToList();

            foreach (var gameDto in res)
            {
                var compatibilityType = GetMostPopularGameCompatibilityType(gameDto!.Id);
                if (compatibilityType != null)
                {
                    gameDto!.CompatibilityType = compatibilityType.Type;
                    gameDto.CompatibilityRank = compatibilityType.Rating!;
                }
                gameDto.CompatibilityPercentage = GetGameCompatibilityPercentage(gameDto.Id);
            }

            return res!;
        }
        
        public async Task<BllAppDTO.Game?> GetGame(Guid id)
        {
            var res = Mapper.Map(await ServiceUow.Games.FirstOrDefaultAsync(id));
            if (res == null) return res;
            var compatibilityType = GetMostPopularGameCompatibilityType(res!.Id);
            if (compatibilityType != null)
            {
                res!.CompatibilityType = compatibilityType.Type;
                res.CompatibilityRank = compatibilityType.Rating!;
            }
            res.CompatibilityPercentage = GetGameCompatibilityPercentage(res.Id);
            return res;
        }
        
        public Dictionary<DALAppDTO.CompatibilityType, int>? GetGameCompatibilityTypeCounts(Guid gameId)
        {
            var query = ServiceUow.Games.FirstOrDefaultAsync(gameId).Result?.Progresses;
            if (query == null) return null;
            var dictionary = new Dictionary<DALAppDTO.CompatibilityType, int>();
            foreach (var progress in query)
            {
                if (progress.CompatibilityTypeId == null) continue;
                var compatibilityType = progress.CompatibilityType!;
                if (dictionary.ContainsKey(compatibilityType))
                    dictionary[compatibilityType]++;
                else
                {
                    dictionary.Add(compatibilityType, 1);
                }
            }

            return dictionary;
        }
        private float? GetGameCompatibilityPercentage(Guid id)
        {
            var dict = GetGameCompatibilityTypeCounts(id);
            if (dict == null || dict.Count == 0) return null;
            var sortedDict = (from entry in dict orderby entry.Value descending select entry).ToList();
            var totalCount = sortedDict.Sum(x => x.Value);
            return sortedDict.First().Value / totalCount * 100;
        }
        
        
        private DALAppDTO.CompatibilityType? GetMostPopularGameCompatibilityType(Guid id)
        {
            var dict = GetGameCompatibilityTypeCounts(id);
            if (dict == null || dict.Count == 0) return null;
            var sortedDict = from entry in dict orderby entry.Value descending select entry;
            return sortedDict.First().Key;
        }
    }
}