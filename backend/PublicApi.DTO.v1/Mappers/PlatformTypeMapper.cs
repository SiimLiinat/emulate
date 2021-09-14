using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class PlatformTypeMapper
    {
        public static BLL.App.DTO.PlatformType MapToBll(PlatformTypeAdd platformTypeAdd)
        {
            var res = new BLL.App.DTO.PlatformType
            {
                Type = platformTypeAdd.Type,
                PlatformsCount = 0
            };
            return res;
        }
        
        public static BLL.App.DTO.PlatformType MapToBll(PlatformType platformType)
        {
            var res = new BLL.App.DTO.PlatformType
            {
                Id = platformType.Id,
                Type = platformType.Type,
                PlatformsCount = platformType.PlatformCount
            };
            return res;
        }
        
        public static PlatformType MapToApi(BLL.App.DTO.PlatformType platformType)
        {
            var res = new PlatformType
            {
                Id = platformType.Id,
                Type = platformType.Type,
                PlatformCount = platformType.PlatformsCount
            };
            return res;
        }
    }
}