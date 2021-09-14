import IAppRoleAdd from '@/domain/IAppRoleAdd'

export default interface IAppRole extends IAppRoleAdd{
    id: string;
    name: string;
    normalizedName: string;
}
