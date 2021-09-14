<template>
    <div class="card card-body mt-5">
        <h1>Delete</h1>
        <div class="align-items-center">
            <div v-if="!loading">
                <h4>Company</h4>
                <hr />
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
                <div>
                    <input @click="deleteCompany()" type="submit" value="Delete" class="btn btn-danger" />
                </div>
            </div>
            <div v-else>
                <div class="spinner-border text-primary" role="status">
                    <span class="sr-only">Loading...</span>
                </div>
            </div>
            <div>
                <router-link to="/companies">Back to List</router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import ICompany from '@/domain/ICompany';
import { BaseService } from '@/services/base-service';
import store from '@/store';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class CompanyDelete extends Vue {
    id!: string;
    private company!: ICompany;
    private loading: boolean = true;

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<ICompany>('v1/companies', this.token || undefined);
        await service.get(this.id).then((data) => {
            this.company = data.data!;
        });
        this.company.developedCount = this.company.developedCount || 0;
        this.company.publishedCount = this.company.publishedCount || 0;
        this.loading = false;
    }

    async deleteCompany(): Promise<void> {
        const service = new BaseService<ICompany>('https://localhost:5001/api/v1/companies', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/companies')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
