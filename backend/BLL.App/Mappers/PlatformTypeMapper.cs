using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PlatformTypeMapper : BaseMapper<BLL.App.DTO.PlatformType, DAL.App.DTO.PlatformType>, IBaseMapper<BLL.App.DTO.PlatformType, DAL.App.DTO.PlatformType>
    {
        public PlatformTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}