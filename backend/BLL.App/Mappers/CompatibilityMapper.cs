using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class CompatibilityMapper : BaseMapper<BLL.App.DTO.Compatibility, DAL.App.DTO.Compatibility>, IBaseMapper<BLL.App.DTO.Compatibility, DAL.App.DTO.Compatibility>
    {
        public CompatibilityMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}