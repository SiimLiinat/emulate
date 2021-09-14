using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MediaTypeRepository : BaseRepository<DAL.App.DTO.MediaType, Domain.App.MediaType, AppDbContext>, IMediaTypeRepository
    {
        public MediaTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new MediaTypeMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.MediaType>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query.Include(m => m.Medias)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.MediaType?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query.Include(m => m.Medias);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}