import IPlatformTypeAdd from '@/domain/IPlatformTypeAdd'

export default interface IPlatformType extends IPlatformTypeAdd {
    id: string | undefined;
    type: string;
    platformCount: number;
}
