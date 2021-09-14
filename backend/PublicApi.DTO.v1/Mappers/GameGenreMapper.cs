using PublicApi.DTO.v1.APIs;

namespace PublicApi.DTO.v1.Mappers
{
    public static class GameGenreMapper
    {
        public static BLL.App.DTO.GameGenre MapToBll(GameGenreAdd gameGenreAdd)
        {
            var res = new BLL.App.DTO.GameGenre
            {
                GenreId = gameGenreAdd.GenreId,
                GameId = gameGenreAdd.GameId
            };
            return res;
        }
        
        public static BLL.App.DTO.GameGenre MapToBll(GameGenre gameGenre)
        {
            var res = new BLL.App.DTO.GameGenre
            {
                Id = gameGenre.Id,
                GenreId = gameGenre.GenreId,
                GameId = gameGenre.GameId,
            };
            return res;
        }
        
        public static GameGenre MapToApi(BLL.App.DTO.GameGenre gameGenre)
        {
            var res = new GameGenre
            {
                Id = gameGenre.Id,
                GenreId = gameGenre.GenreId,
                GameId = gameGenre.GameId,
                GenreType = gameGenre.Genre!.Type,
                GameName = gameGenre.Game!.Name
            };
            return res;
        }
    }
}