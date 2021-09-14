using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PlatformTypeMapper : BaseMapper<DAL.App.DTO.PlatformType, Domain.App.PlatformType>, IBaseMapper<DAL.App.DTO.PlatformType, Domain.App.PlatformType>
    {
        public PlatformTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}