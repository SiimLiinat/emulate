using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IGameService : IBaseEntityService<BllAppDTO.Game, DALAppDTO.Game>, IGameRepositoryCustom<BllAppDTO.Game>
    {
        public Task<IEnumerable<BllAppDTO.Game>> GetAllGames();
        public Task<BllAppDTO.Game?> GetGame(Guid id);
    }
}