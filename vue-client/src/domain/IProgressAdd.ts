export default interface IProgressAdd {
    appUserId: string;
    gameId: string;
    configurationId: string | undefined;
    compatibilityTypeId: string | undefined;
    public: boolean;
    time: number;
    score: number;
    review: string | undefined;
}
