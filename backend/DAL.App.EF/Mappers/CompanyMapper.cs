using AutoMapper;

namespace DAL.App.EF.Mappers
{
    public class CompanyMapper : BaseMapper<DAL.App.DTO.Company, Domain.App.Company>
    {
        public CompanyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}