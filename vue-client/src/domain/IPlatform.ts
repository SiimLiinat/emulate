import IPlatformAdd from '@/domain/IPlatformAdd'

export default interface IPlatform extends IPlatformAdd {
    id: string;
    companyId: string;
    platformTypeId: string;
    name: string;
    code: string;
    companyName: string;
    platformTypeType: string;
}
