import { ICompanyAdd } from "./ICompanyAdd";

export default interface ICompany extends ICompanyAdd {
    id: string | undefined;
    name: string;
    publishedCount: number;
    developedCount: number;
}
