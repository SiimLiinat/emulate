using AutoMapper;

namespace BLL.App.Mappers
{
    public class AppRoleMapper : BaseMapper<BLL.App.DTO.Identity.AppRole, DAL.App.DTO.Identity.AppRole>
    {
        public AppRoleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}