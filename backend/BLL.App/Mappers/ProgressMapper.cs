using AutoMapper;

namespace BLL.App.Mappers
{
    public class ProgressMapper : BaseMapper<BLL.App.DTO.Progress, DAL.App.DTO.Progress>
    {
        public ProgressMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}