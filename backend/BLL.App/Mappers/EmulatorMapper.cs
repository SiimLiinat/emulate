using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class EmulatorMapper : BaseMapper<BLL.App.DTO.Emulator, DAL.App.DTO.Emulator>, IBaseMapper<BLL.App.DTO.Emulator, DAL.App.DTO.Emulator>
    {
        public EmulatorMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}