using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class GenreMapper
    {
        public static BLL.App.DTO.Genre MapToBll(GenreAdd genreAdd)
        {
            var res = new BLL.App.DTO.Genre
            {
                Type = genreAdd.Type
            };
            return res;
        }
        
        public static BLL.App.DTO.Genre MapToBll(Genre genre)
        {
            var res = new BLL.App.DTO.Genre
            {
                Id = genre.Id,
                Type = genre.Type
            };
            return res;
        }
        
        public static Genre MapToApi(BLL.App.DTO.Genre genre)
        {
            var res = new Genre
            {
                Id = genre.Id,
                Type = genre.Type
            };
            return res;
        }
    }
}