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
    public class ConfigurationRepository : BaseRepository<DAL.App.DTO.Configuration, Domain.App.Configuration, AppDbContext>, IConfigurationRepository
    {
        public ConfigurationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ConfigurationMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Configuration>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(c => c.AppUser)
                .Where(u => userId == default || u.AppUserId == userId)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Configuration?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query.Include(c => c.AppUser);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}