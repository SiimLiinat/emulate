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
    public class CompatibilityRepository : BaseRepository<DAL.App.DTO.Compatibility, Domain.App.Compatibility, AppDbContext>, ICompatibilityRepository
    {
        public CompatibilityRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CompatibilityMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Compatibility>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(c => c.CompatibilityType)
                .Include(c => c.Emulator)
                .Include(c => c.GameOnPlatform)
                    .ThenInclude(g => g!.Game)
                .Include(c => c.GameOnPlatform)
                    .ThenInclude(g => g!.Platform)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Compatibility?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(c => c.CompatibilityType)
                .Include(c => c.Emulator)
                .Include(c => c.GameOnPlatform)
                    .ThenInclude(g => g!.Game)
                .Include(c => c.GameOnPlatform)
                    .ThenInclude(g => g!.Platform);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}