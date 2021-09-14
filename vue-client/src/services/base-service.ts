import { IFetchResponse } from "@/types/IFetchResponse";
import { IQueryParams } from "@/types/IQueryParams";
import axios from 'axios';
import IJwtResponse from '@/types/jwt/IJwtResponse'
import { IMessage } from '@/types/IMessage'

const apiEndpoint = 'https://localhost:5001/api/';
// const apiEndpoint = 'https://siliin.azurewebsites.net/api/';

export class BaseService<TEntity> {
    private apiEndpointUrl;
    constructor(protected url: string, private jwt?: string) {
        console.log('BaseService.constructor');
        this.apiEndpointUrl = apiEndpoint + url;
    }

    private authHeaders = this.jwt !== undefined
        ? {
            'Cache-Control': 'no-cache',
            Pragma: 'no-cache',
            Expires: '0',
            Authorization: 'Bearer ' + this.jwt
        } : {
            'Cache-Control': 'no-cache',
            Pragma: 'no-cache',
            Expires: '0',
        };

    async getAll(queryParams?: IQueryParams,): Promise<IFetchResponse<TEntity[]>> {
        try {
            const response = await axios.get(this.apiEndpointUrl, {
                params: queryParams,
                headers: this.authHeaders
            });
            if (response.status >= 200 && response.status < 300) {
                return {
                    ok: true,
                    statusCode: response.status,
                    data: response.data as TEntity[],
                };
            }
            return {
                ok: false,
                statusCode: response.status,
                errorMessage: response.statusText,
            };
        } catch (reason) {
            return {
                ok: false,
                statusCode: 0,
                errorMessage: reason
            };
        }
    }

    async get(id: string, queryParams?: IQueryParams): Promise<IFetchResponse<TEntity>> {
        const url = this.apiEndpointUrl + '/' + id;
        if (queryParams !== undefined) {
            // TODO: add query params to url
        }
        try {
            const response = await axios.get(url, { headers: this.authHeaders })
            if (response.status >= 200 && response.status < 300) {
                return {
                    ok: true,
                    statusCode: response.status,
                    data: response.data as TEntity,
                };
            }
            return {
                ok: false,
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                ok: false,
                statusCode: 0,
                errorMessage: reason
            };
        }
    }

    async post(entity: TEntity): Promise<IFetchResponse<IJwtResponse | IMessage>> {
        try {
            const response = await axios.post(this.apiEndpointUrl, entity, { headers: this.authHeaders });
            if (response.status >= 200 && response.status < 300) {
                return {
                    ok: true,
                    statusCode: response.status
                };
            }
            return {
                ok: false,
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                ok: false,
                statusCode: 0,
                errorMessage: reason
            };
        }
    }

    async put(id: string, entity: TEntity): Promise<IFetchResponse<IJwtResponse | IMessage>> {
        try {
            const url = this.apiEndpointUrl + '/' + id;
            const response = await axios.put(url, entity, { headers: this.authHeaders });
            if (response.status >= 200 && response.status < 300) {
                return {
                    ok: true,
                    statusCode: response.status,
                    data: undefined
                };
            }
            return {
                ok: false,
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                ok: false,
                statusCode: 0,
                errorMessage: reason
            };
        }
    }

    async delete(id: string): Promise<IFetchResponse<IJwtResponse | IMessage>> {
        try {
            const url = this.apiEndpointUrl + '/' + id
            const response = await axios.delete(url, { headers: this.authHeaders });
            if (response.status >= 200 && response.status < 300) {
                return {
                    ok: true,
                    statusCode: response.status,
                    data: undefined
                };
            }
            return {
                ok: false,
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                ok: false,
                statusCode: 0,
                errorMessage: reason
            };
        }
    }
}
