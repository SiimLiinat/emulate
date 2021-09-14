import IEmulatorAdd from '@/domain/IEmulatorAdd'

export default interface IEmulator extends IEmulatorAdd {
    id: string;
    platformId: string;
    name: string;
    url: string;
    picture: string;
    platformName: string;
    programCount: number;
}
