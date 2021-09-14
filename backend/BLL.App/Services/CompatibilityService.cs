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
    public class CompatibilityService : BaseEntityService<IAppUnitOfWork, ICompatibilityRepository, BllAppDTO.Compatibility, DALAppDTO.Compatibility>, ICompatibilityService
    {
        public CompatibilityService(IAppUnitOfWork serviceUow, ICompatibilityRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new CompatibilityMapper(mapper))
        {
        }
        
        public async Task<IEnumerable<BllAppDTO.Compatibility>> GetAllCompatibilities(Guid? gameId)
        {
            var res = (await ServiceUow.Compatibilities.GetAllAsync()).Select(c => Mapper.Map(c)).ToList();
            if (gameId != null) res = res.FindAll(g => g!.GameOnPlatform!.GameId == gameId);
            return res!;
        }
    }
}