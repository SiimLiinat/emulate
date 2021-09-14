<template>
    <div class="card card-body mt-5">
        <h1>Configuration</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/configuration/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Configuration
                    </th>
                    <th>
                        Created at
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="configuration of configurations" v-bind:key="configuration">
                    <td>
                        {{configuration.configurationString}}
                    </td>
                    <td>
                        {{configuration.creationDt}}
                    </td>
                    <td>
                        <router-link v-if="id" :to="'/configuration/edit/' + configuration.id">Edit</router-link>
                        <span v-if="id"> | </span>
                        <router-link :to="'/configuration/details/' + configuration.id">Details</router-link>
                        <span v-if="id" > | </span>
                        <router-link v-if="id" :to="'/configuration/delete/' + configuration.id">Delete</router-link>
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
import { Options, Vue } from 'vue-class-component'
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IConfiguration from '@/domain/IConfiguration';

export default class ConfigurationIndex extends Vue {
    private configurations: IConfiguration[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IConfiguration>('v1/configurations', this.token || undefined);
        await service.getAll({ userId: this.role !== 'Admin' ? store.state.id : null }).then((data) => {
            if (data.ok) {
                this.configurations = data.data!;
            } else {
                console.log(data)
            }
        });
        if (this.id === undefined) await this.$router.push('/');
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
