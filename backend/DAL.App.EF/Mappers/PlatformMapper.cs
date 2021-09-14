using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PlatformMapper : BaseMapper<DAL.App.DTO.Platform, Domain.App.Platform>, IBaseMapper<DAL.App.DTO.Platform, Domain.App.Platform>
    {
        public PlatformMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}