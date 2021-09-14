import IGameOnPlatformAdd from '@/domain/IGameOnPlatformAdd';

export default interface IGameOnPlatform extends IGameOnPlatformAdd {
    id: string;
    gameId: string;
    platformId: string;
    releaseDate: string;
    gameName: string;
    platformName: string;
}
