import IMediaAdd from '@/domain/IMediaAdd'

export default interface IMedia extends IMediaAdd {
    id: string;
    appUserId: string | undefined;
    gameId: string | undefined;
    mediaTypeId: string;
    url: string;
    mediaTypeType: string;
    userName: string;
}
