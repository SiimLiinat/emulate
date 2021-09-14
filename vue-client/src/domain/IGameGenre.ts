import IGameGenreAdd from '@/domain/IGameGenreAdd'

export default interface IGameGenre extends IGameGenreAdd {
    id: string;
    gameId: string;
    genreId: string;
    gameName: string;
    genreType: string;
}
