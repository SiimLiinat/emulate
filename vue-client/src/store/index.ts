import { createStore } from 'vuex'
import axios from 'axios'
import jwtDecode from 'jwt-decode'
import IRegisterInfo from '@/types/account/IRegisterInfo'
import ILoginInfo from '@/types/account/ILoginInfo'
import IJwtResponse from '@/types/jwt/IJwtResponse'
import IDecodedJwt from '@/types/jwt/IDecodedJwt'
import IState from '@/types/state/IState'

export const initialState: IState = {
    token: undefined,
    name: undefined,
    role: undefined,
    id: undefined
}

export default createStore({
    state: initialState,
    mutations: {
        logOut (state: IState): void {
            state.token = undefined;
            state.name = undefined;
            state.role = undefined;
            state.id = undefined;
            localStorage.clear();
        },
        logIn (state: IState, jwtResponse: IJwtResponse): void {
            state.token = jwtResponse.token;
            localStorage.setItem('jwt', jwtResponse.token);
            const decodedToken: IDecodedJwt = jwtDecode(jwtResponse.token);
            state.name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
            state.role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            state.id = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
        },
    },
    actions: {
        async logIn (context, login: ILoginInfo): Promise<void> {
            const loginDataString = JSON.stringify(login);
            await axios.post('https://localhost:5001/api/v1/account/login', loginDataString, {
                headers: { 'Content-type': 'application/json' }
            }).then(data => {
                if (data.status === 200) context.commit('logIn', data.data);
            }).catch(error => error);
        },
        signIn (context, token: string): void {
            context.commit('logIn', { token: token });
        },
        async register (context, register: IRegisterInfo): Promise<void > {
            const registerDataString = JSON.stringify(register);
            const response = await axios.post('https://localhost:5001/api/v1/account/register', registerDataString, {
                headers: { 'Content-type': 'application/json' }
            }).then(data => {
                if (data.status === 200) context.commit('logIn', data.data);
            }).catch(error => error);
        }
    },
    getters: {
    },
    modules: {
    }
})
