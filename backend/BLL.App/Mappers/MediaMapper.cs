using AutoMapper;

namespace BLL.App.Mappers
{
    public class MediaMapper : BaseMapper<BLL.App.DTO.Media, DAL.App.DTO.Media>
    {
        public MediaMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}