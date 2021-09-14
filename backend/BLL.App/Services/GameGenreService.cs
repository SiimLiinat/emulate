using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class GameGenreService : BaseEntityService<IAppUnitOfWork, IGameGenreRepository, BllAppDTO.GameGenre, DALAppDTO.GameGenre>, IGameGenreService
    {
        public GameGenreService(IAppUnitOfWork serviceUow, IGameGenreRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameGenreMapper(mapper))
        {
        }
    }
}