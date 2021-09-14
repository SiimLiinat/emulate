using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class GameGenreMapper : BaseMapper<DAL.App.DTO.GameGenre, Domain.App.GameGenre>, IBaseMapper<DAL.App.DTO.GameGenre, Domain.App.GameGenre>
    {
        public GameGenreMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}