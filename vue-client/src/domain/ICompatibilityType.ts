import ICompatibilityTypeAdd from './ICompatibilityTypeAdd';

export default interface ICompatibilityType extends ICompatibilityTypeAdd {
    id: string | undefined;
    type: string;
    description: string;
    rating: number;
}
