import IMediaTypeAdd from './IMediaTypeAdd';

export default interface IMediaType extends IMediaTypeAdd {
    id: string | undefined;
    type: string;
    description: string;
}
