using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class CompatibilityMapper : BaseMapper<DAL.App.DTO.Compatibility, Domain.App.Compatibility>, IBaseMapper<DAL.App.DTO.Compatibility, Domain.App.Compatibility>
    {
        public CompatibilityMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}