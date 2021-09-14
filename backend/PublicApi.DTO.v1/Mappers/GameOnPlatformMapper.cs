using System;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class GameOnPlatformMapper
    {
        public static BLL.App.DTO.GameOnPlatform MapToBll(GameOnPlatformAdd gameOnPlatformAdd)
        {
            var res = new BLL.App.DTO.GameOnPlatform
            {
                GameId = gameOnPlatformAdd.GameId,
                PlatformId = gameOnPlatformAdd.PlatformId,
                ReleaseDate = gameOnPlatformAdd.ReleaseDate
            };
            return res;
        }
        
        public static BLL.App.DTO.GameOnPlatform MapToBll(GameOnPlatform gameOnPlatform)
        {
            var res = new BLL.App.DTO.GameOnPlatform
            {
                Id = gameOnPlatform.Id,
                GameId = gameOnPlatform.GameId,
                PlatformId = gameOnPlatform.PlatformId,
                ReleaseDate = DateTime.Parse(gameOnPlatform.ReleaseDate),
            };
            return res;
        }
        
        public static GameOnPlatform MapToApi(BLL.App.DTO.GameOnPlatform gameOnPlatform)
        {
            var res = new GameOnPlatform
            {
                Id = gameOnPlatform.Id,
                GameId = gameOnPlatform.GameId,
                PlatformId = gameOnPlatform.PlatformId,
                ReleaseDate = gameOnPlatform.ReleaseDate.ToString("yyyy-MM-dd HH:mm:ss"),
                GameName = gameOnPlatform.Game!.Name,
                PlatformName = gameOnPlatform.Platform!.Name
            };
            return res;
        }
    }
}