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
    public class ProgressService : BaseEntityService<IAppUnitOfWork, IProgressRepository, BllAppDTO.Progress, DALAppDTO.Progress>, IProgressService
    {
        public ProgressService(IAppUnitOfWork serviceUow, IProgressRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProgressMapper(mapper))
        {
        }
        
        public async Task<IEnumerable<BllAppDTO.Progress>> GetAllProgresses(Guid? userId, Guid? gameId)
        {
            IEnumerable<DALAppDTO.Progress> progresses;
            if (userId.HasValue)
            {
                progresses = await ServiceUow.Progresses.GetAllAsync(userId.Value);
            }
            else
            {
                progresses = await ServiceUow.Progresses.GetAllAsync();
            }
            var res = progresses.Select(m => Mapper.Map(m)).ToList()!;
            if (gameId != null) res = res.FindAll(g => g!.GameId == gameId);

            return res!;
        }
    }
}