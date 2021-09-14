using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ConfigurationMapper : BaseMapper<DAL.App.DTO.Configuration, Domain.App.Configuration>, IBaseMapper<DAL.App.DTO.Configuration, Domain.App.Configuration>
    {
        public ConfigurationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}