using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class ProgressMapper : BaseMapper<DAL.App.DTO.Progress, Domain.App.Progress>
    {
        public ProgressMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}