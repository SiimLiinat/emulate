using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using CompatibilityType = Domain.App.CompatibilityType;

namespace DAL.App.EF.Repositories
{
    public class  GameRepository : BaseRepository<Game, Domain.App.Game, AppDbContext>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameMapper(mapper))
        {
        }

        public override async Task<IEnumerable<Game>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(g => g.DevCompany)
                .Include(g => g.PubCompany)
                .Include(g => g.Progresses)
                    .ThenInclude(p => p.CompatibilityType)
                .Include(g => g.Medias)
                    .ThenInclude(m => m.MediaType)
                .Include(g => g.GameOnPlatforms)
                    .ThenInclude(g => g.Platform)
                .Include(g => g.GameGenres)
                    .ThenInclude(g => g.Genre)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<Game?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(g => g.DevCompany)
                .Include(g => g.PubCompany)
                .Include(g => g.Progresses)
                    .ThenInclude(p => p.CompatibilityType)
                .Include(g => g.Medias)
                    .ThenInclude(m => m.MediaType)
                .Include(g => g.GameOnPlatforms)
                    .ThenInclude(g => g.Platform)
                .Include(g => g.GameGenres)
                    .ThenInclude(g => g.Genre);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}