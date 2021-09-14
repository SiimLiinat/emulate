<template>
    <div class="card card-body mt-5">
        <h1>Platform Types</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/platform-type/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Type
                    </th>
                    <th>
                        Platform Count
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="platformType of platformTypes" v-bind:key="platformType">
                    <td>
                        {{platformType.type}}
                    </td>
                    <td>
                        {{platformType.platformCount}}
                    </td>
                    <td>
                        <router-link :to="'/platform-type/edit/' + platformType.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/platform-type/details/' + platformType.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/platform-type/delete/' + platformType.id">Delete</router-link>
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
import IPlatformType from '@/domain/IPlatformType';
import { BaseService } from '@/services/base-service';

export default class PlatformTypeIndex extends Vue {
    private platformTypes: IPlatformType[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IPlatformType>('v1/platformTypes', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.platformTypes = data.data!;
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
