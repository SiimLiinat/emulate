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
    public class CompanyRepository : BaseRepository<DAL.App.DTO.Company, Domain.App.Company, AppDbContext>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CompanyMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Company>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(c => c.DevelopedGames)
                .Include(c => c.PublishedGames)
                .Include(c => c.Platforms)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Company?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(c => c.DevelopedGames)
                .Include(c => c.PublishedGames)
                .Include(c => c.Platforms);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}