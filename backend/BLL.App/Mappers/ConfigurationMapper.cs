using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ConfigurationMapper : BaseMapper<BLL.App.DTO.Configuration, DAL.App.DTO.Configuration>, IBaseMapper<BLL.App.DTO.Configuration, DAL.App.DTO.Configuration>
    {
        public ConfigurationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}