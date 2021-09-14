using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class CompanyMapper
    {
        public static BLL.App.DTO.Company MapToBll(CompanyAdd companyAdd)
        {
            var res = new BLL.App.DTO.Company
            {
                Name = companyAdd.Name,
            };
            return res;
        }
        
        public static BLL.App.DTO.Company MapToBll(Company company)
        {
            var res = new BLL.App.DTO.Company
            {
                Id = company.Id,
                Name = company.Name,
                DevelopedGamesCount = company.DevelopedCount,
                PublishedGamesCount = company.PublishedCount
            };
            return res;
        }
        
        public static Company MapToApi(BLL.App.DTO.Company company)
        {
            var res = new Company
            {
                Id = company.Id,
                Name = company.Name,
                DevelopedCount = company.DevelopedGamesCount,
                PublishedCount = company.PublishedGamesCount
            };
            return res;
        }
    }
}