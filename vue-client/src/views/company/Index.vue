<template>
    <div class="card card-body mt-5">
        <h1>Companies</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/company/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}" >
                <thead>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Published Games
                    </th>
                    <th>
                        Developed Games
                    </th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="company of companies" v-bind:key="company">
                    <td>
                        {{company.name}}
                    </td>
                    <td>
                        {{company.publishedCount}}
                    </td>
                    <td>
                        {{company.developedCount}}
                    </td>
                    <td>
                        <router-link :to="'/company/edit/' + company.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/company/details/' + company.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/company/delete/' + company.id">Delete</router-link>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="mt-3" v-if="loading">
        <div class="spinner-border text-light" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import store from '@/store';
import ICompany from '@/domain/ICompany';
import { BaseService } from '@/services/base-service';
@Options({
})

export default class CompanyIndex extends Vue {
    // noinspection JSMismatchedCollectionQueryUpdate
    private companies: ICompany[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<ICompany>('v1/companies', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.companies = data.data!;
            } else {
                console.log(data.errorMessage)
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
