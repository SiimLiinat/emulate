using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class PlatformMapper
    {
        public static BLL.App.DTO.Platform MapToBll(PlatformAdd platformAdd)
        {
            var res = new BLL.App.DTO.Platform
            {
                CompanyId = platformAdd.CompanyId,
                PlatformTypeId = platformAdd.PlatformTypeId,
                Name = platformAdd.Name,
                Code = platformAdd.Code
            };
            return res;
        }
        
        public static BLL.App.DTO.Platform MapToBll(Platform platform)
        {
            var res = new BLL.App.DTO.Platform
            {
                Id = platform.Id,
                CompanyId = platform.CompanyId,
                PlatformTypeId = platform.PlatformTypeId,
                Name = platform.Name,
                Code = platform.Code,
            };
            return res;
        }
        
        public static Platform MapToApi(BLL.App.DTO.Platform platform)
        {
            var res = new Platform
            {
                Id = platform.Id,
                CompanyId = platform.CompanyId,
                PlatformTypeId = platform.PlatformTypeId,
                Name = platform.Name,
                Code = platform.Code,
                CompanyName = platform.Company!.Name,
                PlatformTypeType = platform.PlatformType!.Type
            };
            return res;
        }
    }
}