using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ICompatibilityService : IBaseEntityService<BllAppDTO.Compatibility, DALAppDTO.Compatibility>, ICompatibilityRepositoryCustom<BllAppDTO.Compatibility>
    {
        public Task<IEnumerable<BllAppDTO.Compatibility>> GetAllCompatibilities(Guid? gameId);
    }
}