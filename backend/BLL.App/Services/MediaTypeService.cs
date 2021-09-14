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
    public class MediaTypeService : BaseEntityService<IAppUnitOfWork, IMediaTypeRepository, BllAppDTO.MediaType, DALAppDTO.MediaType>, IMediaTypeService
    {
        public MediaTypeService(IAppUnitOfWork serviceUow, IMediaTypeRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new MediaTypeMapper(mapper))
        {
        }
    }
}