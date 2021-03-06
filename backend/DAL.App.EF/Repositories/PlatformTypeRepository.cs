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
    public class PlatformTypeRepository : BaseRepository<DAL.App.DTO.PlatformType, Domain.App.PlatformType, AppDbContext>, IPlatformTypeRepository
    {
        public PlatformTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PlatformTypeMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.PlatformType>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query.Include(p => p.Platforms)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.PlatformType?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query.Include(p => p.Platforms);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}