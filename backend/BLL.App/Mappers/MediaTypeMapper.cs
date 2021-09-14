using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class MediaTypeMapper : BaseMapper<BLL.App.DTO.MediaType, DAL.App.DTO.MediaType>, IBaseMapper<BLL.App.DTO.MediaType, DAL.App.DTO.MediaType>
    {
        public MediaTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}