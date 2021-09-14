<template>
    <div class="card card-body mt-5">
        <h1>Platforms</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/platform/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Company
                    </th>
                    <th>
                        PlatformType
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Code
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="platform of platforms" v-bind:key="platform">
                    <td>
                        {{platform.companyName}}
                    </td>
                    <td>
                        {{platform.platformTypeType}}
                    </td>
                    <td>
                        {{platform.name}}
                    </td>
                    <td>
                        {{platform.code}}
                    </td>
                    <td>
                        <router-link :to="'/platform/edit/' + platform.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/platform/details/' + platform.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/platform/delete/' + platform.id">Delete</router-link>
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
import IPlatform from '@/domain/IPlatform';
import store from '@/store';
import { BaseService } from '@/services/base-service';

export default class PlatformIndex extends Vue {
    private platforms: IPlatform[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IPlatform>('v1/platforms', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.platforms = data.data!;
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
