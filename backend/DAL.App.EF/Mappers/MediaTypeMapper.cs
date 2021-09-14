using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class MediaTypeMapper : BaseMapper<DAL.App.DTO.MediaType, Domain.App.MediaType>, IBaseMapper<DAL.App.DTO.MediaType, Domain.App.MediaType>
    {
        public MediaTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}