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
    public class GenreService : BaseEntityService<IAppUnitOfWork, IGenreRepository, BllAppDTO.Genre, DALAppDTO.Genre>, IGenreService
    {
        public GenreService(IAppUnitOfWork serviceUow, IGenreRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GenreMapper(mapper))
        {
        }
    }
}