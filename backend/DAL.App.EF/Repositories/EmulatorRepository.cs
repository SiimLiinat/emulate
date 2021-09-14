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
    public class EmulatorRepository : BaseRepository<DAL.App.DTO.Emulator, Domain.App.Emulator, AppDbContext>, IEmulatorRepository
    {
        public EmulatorRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new EmulatorMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Emulator>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query
                .Include(e => e.Platform)
                .Include(e => e.Compatibilities)
                .Select(c => Mapper.Map(c));
            var res = await resQuery.ToListAsync();
            return res!;
        }

        public override async Task<DAL.App.DTO.Emulator?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(e => e.Platform)
                .Include(e => e.Compatibilities);
            var res = Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
            return res;
        }
    }
}