import ICompatibilityAdd from '@/domain/ICompatibilityAdd'

export default interface ICompatibility extends ICompatibilityAdd {
    id: string;
    compatibilityTypeId: string;
    emulatorId: string;
    gameOnPlatformId: string;
    date: string;
    gameName: string;
    platformName: string;
    emulatorName: string;
    compatibilityTypeType: string;
    compatibilityTypeRank: number;
}
