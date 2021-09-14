<template>
    <div class="card card-body mt-5">
        <h1 class="mt-4">Company</h1>

        <div v-if="!loading" class="card card-body">
            <dl class="row">
                <dt class = "col-sm-2">
                    Name
                </dt>
                <dd class = "col-sm-10">
                    {{company.name}}
                </dd>
            </dl>
            <dl class="row">
                <dt class = "col-sm-2">
                    Published Games
                </dt>
                <dd class = "col-sm-10">
                    {{company.publishedCount}}
                </dd>
            </dl>
            <dl class="row">
                <dt class = "col-sm-2">
                    Developed Games
                </dt>
                <dd class = "col-sm-10">
                    {{company.developedCount}}
                </dd>
            </dl>
        </div>
        <div v-if="!loading">
            <router-link v-if="role === 'Admin'" :to="'/company/edit/' + company.id">Edit</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link v-if="role === 'Admin'" :to="'/company/delete/' + company.id">Delete</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link to="/companies">Back to List</router-link>
        </div>
        <div v-else>
            <div class="spinner-border text-light" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import store from '@/store';
import ICompany from '@/domain/ICompany';
import { BaseService } from '@/services/base-service';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class CompanyDetails extends Vue {
    id!: string;
    private company!: ICompany;
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
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.company = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
