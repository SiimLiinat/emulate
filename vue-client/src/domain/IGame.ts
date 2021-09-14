import IGameAdd from '@/domain/IGameAdd'

export default interface IGame extends IGameAdd {
    id: string;
    devCompanyId: string;
    pubCompanyId: string;
    name: string;
    releaseDate: string;
    devCompanyName: string;
    pubCompanyName: string;
    compatibilityType: string;
    compatibilityRank: number;
    compatibilityPercentage: string;
    platforms: string[];
    genres: string[];
    coverArt: string;
}
