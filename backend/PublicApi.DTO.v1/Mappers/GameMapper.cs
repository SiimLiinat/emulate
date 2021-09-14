using System;
using System.Linq;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class GameMapper
    {
        public static BLL.App.DTO.Game MapToBll(GameAdd gameAdd)
        {
            var res = new BLL.App.DTO.Game
            {
                DevCompanyId = gameAdd.DevCompanyId,
                PubCompanyId = gameAdd.PubCompanyId,
                Name = gameAdd.Name,
                ReleaseDate = gameAdd.ReleaseDate
            };
            return res;
        }
        
        public static BLL.App.DTO.Game MapToBll(Game game)
        {
            var res = new BLL.App.DTO.Game
            {
                Id = game.Id,
                DevCompanyId = game.DevCompanyId,
                PubCompanyId = game.PubCompanyId,
                Name = game.Name,
                ReleaseDate = DateTime.Parse(game.ReleaseDate),
                CompatibilityType = game.CompatibilityType,
                CompatibilityPercentage = game.CompatibilityPercentage,
                CompatibilityRank = game.CompatibilityRank
            };
            return res;
        }
        
        public static Game MapToApi(BLL.App.DTO.Game game)
        {
            var res = new Game
            {
                Id = game.Id,
                DevCompanyId = game.DevCompanyId,
                PubCompanyId = game.PubCompanyId,
                Name = game.Name,
                ReleaseDate = game.ReleaseDate.ToString("yyyy-MM-dd HH:mm:ss"),
                DevCompanyName = game.DevCompany!.Name,
                PubCompanyName = game.PubCompany?.Name!,
                CompatibilityType = game.CompatibilityType,
                CompatibilityPercentage = game.CompatibilityPercentage,
                CompatibilityRank = game.CompatibilityRank,
                Platforms = game.GameOnPlatforms!.OrderBy(g => g.Platform!.Code).Select(g => g.Platform!.Code).ToArray(),
                Genres = game.GameGenres!.OrderBy(g => g.Genre!.Type).Select(g => g.Genre!.Type).ToArray(),
                CoverArt = game.Medias?.FirstOrDefault(m => m.MediaType!.Type == "Cover Art")?.Url
            };
            return res;
        }
    }
}