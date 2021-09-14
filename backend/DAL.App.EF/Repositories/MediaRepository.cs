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
    public class MediaRepository : BaseRepository<DAL.App.DTO.Media, Domain.App.Media, AppDbContext>, IMediaRepository
    {
        public MediaRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new MediaMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Media>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(m => m.Game)
                .Include(m => m.MediaType)
                .Include(m => m.AppUser)
                .Where(u => userId == default || u.AppUserId == userId)
                .Select(c => Mapper.Map(c)!);
            return await resQuery!.ToListAsync();
        }
        
        public override async Task<DTO.Media?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(m => m.Game)
                .Include(m => m.MediaType)
                .Include(m => m.AppUser);
            if (userId != default) query = query.Where(u => u!.AppUserId == userId);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}