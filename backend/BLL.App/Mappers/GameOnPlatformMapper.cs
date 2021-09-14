using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class GameOnPlatformMapper : BaseMapper<BLL.App.DTO.GameOnPlatform, DAL.App.DTO.GameOnPlatform>, IBaseMapper<BLL.App.DTO.GameOnPlatform, DAL.App.DTO.GameOnPlatform>
    {
        public GameOnPlatformMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}