using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class CompatibilityTypeMapper : BaseMapper<DAL.App.DTO.CompatibilityType, Domain.App.CompatibilityType>, IBaseMapper<DAL.App.DTO.CompatibilityType, Domain.App.CompatibilityType>
    {
        public CompatibilityTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}