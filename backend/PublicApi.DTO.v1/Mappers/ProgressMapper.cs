using System;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class ProgressMapper
    {
        public static BLL.App.DTO.Progress MapToBll(ProgressAdd progressAdd)
        {
            var res = new BLL.App.DTO.Progress
            {
                AppUserId = progressAdd.AppUserId,
                GameId = progressAdd.GameId,
                ConfigurationId = progressAdd.ConfigurationId,
                CompatibilityTypeId = progressAdd.CompatibilityTypeId,
                Public = progressAdd.Public,
                Time = progressAdd.Time * 3600,
                Score = progressAdd.Score,
                CreatedAt = DateTime.Now,
                EditedAt = null,
                Review = progressAdd.Review,
            };
            return res;
        }
        
        public static BLL.App.DTO.Progress MapToBll(Progress progress)
        {
            var res = new BLL.App.DTO.Progress
            {
                Id = progress.Id,
                AppUserId = progress.AppUserId,
                GameId = progress.GameId,
                ConfigurationId = progress.ConfigurationId,
                CompatibilityTypeId = progress.CompatibilityTypeId,
                Public = progress.Public,
                Time = (int) progress.Time * 3600,
                Score = progress.Score,
                CreatedAt = DateTime.Parse(progress.CreatedAt),
                EditedAt = progress.EditedAt != null ? DateTime.Parse(progress.EditedAt) : null,
                Review = progress.Review,
            };
            return res;
        }
        
        public static Progress MapToApi(BLL.App.DTO.Progress progress)
        {
            var res = new Progress
            {
                Id = progress.Id,
                AppUserId = progress.AppUserId,
                GameId = progress.GameId,
                ConfigurationId = progress.ConfigurationId,
                CompatibilityTypeId = progress.CompatibilityTypeId,
                Public = progress.Public,
                Time = progress.Time / 3600.0f,
                Score = progress.Score,
                CreatedAt = progress.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                EditedAt = progress.EditedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
                Review = progress.Review,
                AppUserName = progress.AppUser!.UserName,
                AppUserProfile = progress.AppUser!.ProfilePicture,
                GameName = progress.Game!.Name,
                ConfigurationString = progress.Configuration?.ConfigurationString,
                CompatibilityTypeType = progress.CompatibilityType?.Type,
                CompatibilityTypeRank = progress.CompatibilityType?.Rating
            };
            return res;
        }
    }
}