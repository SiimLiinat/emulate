using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class AppUserMapper
    {
        public static Domain.App.Identity.AppUser MapToDomain(AppUser appUser)
        {
            var res = new Domain.App.Identity.AppUser
            {
                Id = appUser.Id,
                Email = appUser.Email,
                UserName = appUser.UserName,
                PasswordHash = appUser.PasswordHash,
                SecurityStamp = appUser.SecurityStamp,
                LockoutEnd = appUser.LockoutEnd,
                ProfilePicture = appUser.ProfilePicture
            };
            return res;
        }
                
        public static AppUser MapToApi(Domain.App.Identity.AppUser appUser)
        {
            var res = new AppUser
            {
                Id = appUser.Id,
                Email = appUser.Email,
                UserName = appUser.UserName,
                PasswordHash = appUser.PasswordHash,
                SecurityStamp = appUser.SecurityStamp,
                LockoutEnd = appUser.LockoutEnd,
                ProfilePicture = appUser.ProfilePicture
            };
            return res;
        }
    }
}