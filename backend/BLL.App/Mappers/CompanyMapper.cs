using AutoMapper;

namespace BLL.App.Mappers
{
    public class CompanyMapper : BaseMapper<BLL.App.DTO.Company, DAL.App.DTO.Company>
    {
        public CompanyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}