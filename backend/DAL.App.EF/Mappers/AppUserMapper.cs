using AutoMapper;
using Domain.App.Identity;

namespace DAL.App.EF.Mappers
{
    public class AppUserMapper : BaseMapper<DAL.App.DTO.Identity.AppUser, AppUser>
    {
        public AppUserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}