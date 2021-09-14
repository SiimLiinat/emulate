using AutoMapper;
using Domain.App.Identity;

namespace DAL.App.EF.Mappers
{
    public class AppRoleMapper : BaseMapper<DAL.App.DTO.Identity.AppRole, AppRole>
    {
        public AppRoleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}