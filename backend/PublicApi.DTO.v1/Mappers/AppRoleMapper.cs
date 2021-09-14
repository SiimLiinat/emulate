using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class AppRoleMapper
    {
        public static Domain.App.Identity.AppRole MapToDomain(AppRoleAdd appRoleAdd)
        {
            var res = new Domain.App.Identity.AppRole
            {
                Name = appRoleAdd.Name,
            };
            return res;
        }
        
        public static Domain.App.Identity.AppRole MapToDomain(AppRole appRole)
        {
            var res = new Domain.App.Identity.AppRole
            {
                Id = appRole.Id,
                Name = appRole.Name,
                NormalizedName = appRole.NormalizedName
            };
            return res;
        }
                
        public static AppRole MapToApi(Domain.App.Identity.AppRole appRole)
        {
            var res = new AppRole
            {
                Id = appRole.Id,
                Name = appRole.Name,
                NormalizedName = appRole.NormalizedName
            };
            return res;
        }
    }
}