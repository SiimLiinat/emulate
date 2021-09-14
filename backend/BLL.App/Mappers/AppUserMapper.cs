using AutoMapper;

namespace BLL.App.Mappers
{
    public class AppUserMapper : BaseMapper<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>
    {
        public AppUserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}