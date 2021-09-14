using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class EmulatorService : BaseEntityService<IAppUnitOfWork, IEmulatorRepository, BllAppDTO.Emulator, DALAppDTO.Emulator>, IEmulatorService
    {
        public EmulatorService(IAppUnitOfWork serviceUow, IEmulatorRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new EmulatorMapper(mapper))
        {
        }
    }
}