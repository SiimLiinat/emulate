using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class MediaTypeMapper
    {
        public static BLL.App.DTO.MediaType MapToBll(MediaTypeAdd mediaTypeAdd)
        {
            var res = new BLL.App.DTO.MediaType
            {
                Type = mediaTypeAdd.Type,
                Description = mediaTypeAdd.Description
            };
            return res;
        }
        
        public static BLL.App.DTO.MediaType MapToBll(MediaType mediaType)
        {
            var res = new BLL.App.DTO.MediaType
            {
                Id = mediaType.Id,
                Type = mediaType.Type,
                Description = mediaType.Description
            };
            return res;
        }
        
        public static MediaType MapToApi(BLL.App.DTO.MediaType mediaType)
        {
            var res = new MediaType
            {
                Id = mediaType.Id,
                Type = mediaType.Type,
                Description = mediaType.Description
            };
            return res;
        }
    }
}