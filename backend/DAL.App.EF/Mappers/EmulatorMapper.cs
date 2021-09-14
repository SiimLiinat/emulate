using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class EmulatorMapper : BaseMapper<DAL.App.DTO.Emulator, Domain.App.Emulator>, IBaseMapper<DAL.App.DTO.Emulator, Domain.App.Emulator>
    {
        public EmulatorMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}