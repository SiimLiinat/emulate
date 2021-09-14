export interface IFetchResponse<TData> {
    ok: boolean;
    statusCode: number;
    errorMessage?: string;
    data?: TData;
}
