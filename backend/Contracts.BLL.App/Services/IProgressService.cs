using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProgressService : IBaseEntityService<BllAppDTO.Progress, DALAppDTO.Progress>, IProgressRepositoryCustom<BllAppDTO.Progress>
    {
        public Task<IEnumerable<BllAppDTO.Progress>> GetAllProgresses(Guid? userId, Guid? gameId);
    }
}