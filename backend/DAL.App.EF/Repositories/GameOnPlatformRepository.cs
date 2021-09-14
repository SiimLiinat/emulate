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
    public class GameOnPlatformRepository : BaseRepository<DAL.App.DTO.GameOnPlatform, Domain.App.GameOnPlatform, AppDbContext>, IGameOnPlatformRepository
    {
        public GameOnPlatformRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameOnPlatformMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.GameOnPlatform>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(g => g.Game)
                .Include(g => g.Platform)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.GameOnPlatform?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(g => g.Game)
                .Include(g => g.Platform);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}