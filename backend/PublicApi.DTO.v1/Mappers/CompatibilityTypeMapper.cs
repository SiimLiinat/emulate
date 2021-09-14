using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class CompatibilityTypeMapper
    {
        public static BLL.App.DTO.CompatibilityType MapToBll(CompatibilityTypeAdd compatibilityTypeAdd)
        {
            var res = new BLL.App.DTO.CompatibilityType
            {
                Type = compatibilityTypeAdd.Type,
                Description = compatibilityTypeAdd.Description,
                Rating = compatibilityTypeAdd.Rating
            };
            return res;
        }
        
        public static BLL.App.DTO.CompatibilityType MapToBll(CompatibilityType compatibilityType)
        {
            var res = new BLL.App.DTO.CompatibilityType
            {
                Id = compatibilityType.Id,
                Type = compatibilityType.Type,
                Description = compatibilityType.Description,
                Rating = compatibilityType.Rating
            };
            return res;
        }
        
        public static CompatibilityType MapToApi(BLL.App.DTO.CompatibilityType compatibilityType)
        {
            var res = new CompatibilityType
            {
                Id = compatibilityType.Id,
                Type = compatibilityType.Type,
                Description = compatibilityType.Description,
                Rating = compatibilityType.Rating
            };
            return res;
        }
    }
}