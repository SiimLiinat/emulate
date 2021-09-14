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
    public class MediaService : BaseEntityService<IAppUnitOfWork, IMediaRepository, BllAppDTO.Media, DALAppDTO.Media>, IMediaService
    {
        public MediaService(IAppUnitOfWork serviceUow, IMediaRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new MediaMapper(mapper))
        {
        }
        
        public async Task<IEnumerable<BllAppDTO.Media>> GetAllMedias(Guid? userId, Guid? gameId)
        {
            IEnumerable<DALAppDTO.Media> medias;
            if (userId.HasValue)
            {
                medias = await ServiceUow.Medias.GetAllAsync(userId.Value);
            }
            else
            {
                medias = await ServiceUow.Medias.GetAllAsync();
            }
            var res = medias.Select(m => Mapper.Map(m)).ToList()!;
            if (gameId != null) res = res.FindAll(g => g!.GameId != null && g.GameId == gameId);

            return res!;
        }
    }
}