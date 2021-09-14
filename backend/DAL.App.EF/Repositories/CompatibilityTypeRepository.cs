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
    public class CompatibilityTypeRepository : BaseRepository<DAL.App.DTO.CompatibilityType, Domain.App.CompatibilityType, AppDbContext>, ICompatibilityTypeRepository
    {
        public CompatibilityTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CompatibilityTypeMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.CompatibilityType>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(c => c.Progresses)
                .Include(c => c.Compatibilities)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.CompatibilityType?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(c => c.Progresses)
                .Include(c => c.Compatibilities);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}