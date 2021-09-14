import IProgressAdd from '@/domain/IProgressAdd'

export default interface IProgress extends IProgressAdd {
    id: string;

    appUserId: string;
    gameId: string;
    configurationId: string | undefined;
    compatibilityTypeId: string | undefined;
    public: boolean;
    time: number;
    score: number;
    createdAt: string;
    editedAt: string | undefined;
    review: string | undefined;

    appUserName: string;
    appUserProfile: string | undefined;
    gameName: string;
    configurationString: string | undefined;
    compatibilityTypeType: string | undefined;
    compatibilityTypeRank: number | undefined;
}
