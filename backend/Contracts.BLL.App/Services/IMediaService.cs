using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IMediaService : IBaseEntityService<BllAppDTO.Media, DALAppDTO.Media>, IMediaRepositoryCustom<BllAppDTO.Media>
    {
        public Task<IEnumerable<BllAppDTO.Media>> GetAllMedias(Guid? userId, Guid? gameId);
    }
}