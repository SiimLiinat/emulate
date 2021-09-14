using System;
using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class CompatibilityMapper
    {
        public static BLL.App.DTO.Compatibility MapToBll(CompatibilityAdd compatibilityAdd)
        {
            var res = new BLL.App.DTO.Compatibility
            {
                CompatibilityTypeId = compatibilityAdd.CompatibilityTypeId,
                GameOnPlatformId = compatibilityAdd.GameOnPlatformId,
                EmulatorId = compatibilityAdd.EmulatorId,
                Date = compatibilityAdd.Date.ToUniversalTime()
            };
            return res;
        }
        
        public static BLL.App.DTO.Compatibility MapToBll(Compatibility compatibility)
        {
            var res = new BLL.App.DTO.Compatibility
            {
                Id = compatibility.Id,
                CompatibilityTypeId = compatibility.CompatibilityTypeId,
                GameOnPlatformId = compatibility.GameOnPlatformId,
                EmulatorId = compatibility.EmulatorId,
                Date = DateTime.Parse(compatibility.Date),
            };
            return res;
        }
        
        public static Compatibility MapToApi(BLL.App.DTO.Compatibility compatibility)
        {
            var res = new Compatibility
            {
                Id = compatibility.Id,
                CompatibilityTypeId = compatibility.CompatibilityTypeId,
                GameOnPlatformId = compatibility.GameOnPlatformId,
                EmulatorId = compatibility.EmulatorId,
                Date = compatibility.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                GameName = compatibility.GameOnPlatform!.Game!.Name,
                PlatformName = compatibility.GameOnPlatform!.Platform!.Name,
                EmulatorName = compatibility.Emulator!.Name,
                CompatibilityTypeType = compatibility.CompatibilityType!.Type,
                CompatibilityTypeRank = compatibility.CompatibilityType.Rating
            };
            return res;
        }
    }
}