import IGenreAdd from '@/domain/IGenreAdd'

export default interface IGenre extends IGenreAdd {
    id: string;
    type: string;
}
