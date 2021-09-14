<template>
    <div class="card card-body mt-5">
        <h1>Users</h1>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Email
                    </th>
                    <th>
                        Username
                    </th>
                    <th>
                        Locked out
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="user of users" v-bind:key="user">
                    <td>
                        {{user.email}}
                    </td>
                    <td>
                        {{user.userName}}
                    </td>
                    <td>
                        {{user.lockoutEnd || "false"}}
                    </td>
                    <td>
                        <router-link :to="'/user/edit/' + user.id">Edit</router-link>
                        <span > | </span>
                        <router-link :to="'/user/details/' + user.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/user/delete/' + user.id">Delete</router-link>
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
import IAppUser from '@/domain/IAppUser';

export default class UserIndex extends Vue {
    private users: IAppUser[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.users = data.data!;
                console.log(data!.data)
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
