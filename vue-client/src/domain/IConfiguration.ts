import IConfigurationAdd from '@/domain/IConfigurationAdd'

export default interface IConfiguration extends IConfigurationAdd {
    id: string;
    appUserId: string;
    configurationString: string;
    creationDt: string;
}
