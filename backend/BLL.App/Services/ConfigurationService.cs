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
    public class ConfigurationService : BaseEntityService<IAppUnitOfWork, IConfigurationRepository, BllAppDTO.Configuration, DALAppDTO.Configuration>, IConfigurationService
    {
        public ConfigurationService(IAppUnitOfWork serviceUow, IConfigurationRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ConfigurationMapper(mapper))
        {
        }
    }
}