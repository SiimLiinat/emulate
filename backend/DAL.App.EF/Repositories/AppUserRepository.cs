using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class AppUserRepository : BaseRepository<DAL.App.DTO.Identity.AppUser, AppUser, AppDbContext>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AppUserMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Identity.AppUser>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(u => u.Configurations)
                .Include(u => u.Medias)
                .Include(u => u.Progresses)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Identity.AppUser?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(u => u.Configurations)
                .Include(u => u.Medias)
                .Include(u => u.Progresses);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}