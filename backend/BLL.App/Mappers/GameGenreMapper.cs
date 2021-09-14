using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class GameGenreMapper : BaseMapper<BLL.App.DTO.GameGenre, DAL.App.DTO.GameGenre>, IBaseMapper<BLL.App.DTO.GameGenre, DAL.App.DTO.GameGenre>
    {
        public GameGenreMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}