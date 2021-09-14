using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class GameOnPlatformMapper : BaseMapper<DAL.App.DTO.GameOnPlatform, Domain.App.GameOnPlatform>, IBaseMapper<DAL.App.DTO.GameOnPlatform, Domain.App.GameOnPlatform>
    {
        public GameOnPlatformMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}