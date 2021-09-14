using AutoMapper;

namespace BLL.App.Mappers
{
    public class PlatformMapper : BaseMapper<BLL.App.DTO.Platform, DAL.App.DTO.Platform>
    {
        public PlatformMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}