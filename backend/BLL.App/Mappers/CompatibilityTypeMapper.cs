using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class CompatibilityTypeMapper : BaseMapper<BLL.App.DTO.CompatibilityType, DAL.App.DTO.CompatibilityType>,
        IBaseMapper<BLL.App.DTO.CompatibilityType, DAL.App.DTO.CompatibilityType>
    {
        public CompatibilityTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}