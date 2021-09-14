<template>
    <div class="card card-body mt-5">
        <h1>Roles</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/role/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Type
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="appRole of roles" v-bind:key="appRole">
                    <td>
                        {{appRole.name}}
                    </td>
                    <td>
                        <router-link :to="'/role/edit/' + appRole.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/role/details/' + appRole.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/role/delete/' + appRole.id">Delete</router-link>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
        <div v-else>
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IAppRole from '@/domain/IAppRole';

export default class RoleIndex extends Vue {
    private roles: IAppRole[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IAppRole>('v1/appRoles', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.roles = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
