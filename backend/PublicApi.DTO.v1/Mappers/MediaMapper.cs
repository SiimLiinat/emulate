using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class MediaMapper
    {
        public static BLL.App.DTO.Media MapToBll(MediaAdd mediaAdd)
        {
            var res = new BLL.App.DTO.Media
            {
                MediaTypeId = mediaAdd.MediaTypeId,
                GameId = mediaAdd.GameId,
                AppUserId = mediaAdd.AppUserId,
                Url = mediaAdd.Url
            };
            return res;
        }
        
        public static BLL.App.DTO.Media MapToBll(Media media)
        {
            var res = new BLL.App.DTO.Media
            {
                Id = media.Id,
                MediaTypeId = media.MediaTypeId,
                GameId = media.GameId,
                AppUserId = media.AppUserId,
                Url = media.Url,
            };
            return res;
        }
        
        public static Media MapToApi(BLL.App.DTO.Media media)
        {
            var res = new Media
            {
                Id = media.Id,
                MediaTypeId = media.MediaTypeId,
                GameId = media.GameId,
                AppUserId = media.AppUserId!.Value,
                Url = media.Url,
                MediaTypeType = media.MediaType!.Type,
                UserName = media.AppUser!.UserName
            };
            return res;
        }
    }
}