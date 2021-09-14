using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IGameGenreService : IBaseEntityService<BllAppDTO.GameGenre, DALAppDTO.GameGenre>, IGameGenreRepositoryCustom<BllAppDTO.GameGenre>
    {
    }
}