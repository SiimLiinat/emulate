using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class MediaMapper : BaseMapper<DAL.App.DTO.Media, Domain.App.Media>, IBaseMapper<DAL.App.DTO.Media, Domain.App.Media>
    {
        public MediaMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}