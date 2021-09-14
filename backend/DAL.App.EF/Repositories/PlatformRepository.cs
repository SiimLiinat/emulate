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
    public class PlatformRepository : BaseRepository<DAL.App.DTO.Platform, Domain.App.Platform, AppDbContext>, IPlatformRepository
    {
        public PlatformRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PlatformMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Platform>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(p => p.Company)
                .Include(p => p.PlatformType)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Platform?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(p => p.Company)
                .Include(p => p.PlatformType);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}