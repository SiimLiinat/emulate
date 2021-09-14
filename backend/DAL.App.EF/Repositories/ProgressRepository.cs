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
    public class ProgressRepository : BaseRepository<DAL.App.DTO.Progress, Domain.App.Progress, AppDbContext>, IProgressRepository
    {
        public ProgressRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProgressMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Progress>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(u => u.AppUser)
                .Include(u => u.CompatibilityType)
                .Include(u => u.Configuration)
                .Include(u => u.Game)
                .Where(u => userId == default || u.AppUserId == userId)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Progress?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(u => u.AppUser)
                .Include(u => u.CompatibilityType)
                .Include(u => u.Configuration)
                .Include(u => u.Game);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}