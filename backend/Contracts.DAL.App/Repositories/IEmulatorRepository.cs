using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IEmulatorRepository : IBaseRepository<global::DAL.App.DTO.Emulator>, IEmulatorRepositoryCustom<global::DAL.App.DTO.Emulator>
    {
        
    }
    
    public interface IEmulatorRepositoryCustom<TEntity>
    {
    }
}