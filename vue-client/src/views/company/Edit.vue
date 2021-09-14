<template>
    <h1 class="text-white">Edit</h1>
    <div class="row" v-if="!loading">
        <div class="col-md-4"/>
        <div class="col-md-4 card card-body mt-3">
            <h4>Company</h4>
            <div class="form-group text-center">
                <label class="control-label" for="Name">Name</label>
                <input class="form-control" type="text" data-val="true" data-val-maxlength="The field Name must be a string or array with a maximum length of 100"
                       data-val-maxlength-max="100" data-val-required="The Name field is required." id="Name" maxlength="100" name="Name" v-model="company.name" />
                <span class="text-danger field-validation-valid" data-valmsg-for="Name" data-valmsg-replace="true"></span>
            </div>
            <div class="form-group">
                <input @click="editCompany()" type="submit" value="Save" class="btn btn-primary" />
            </div>
        </div>
        <div class="col-md-4"/>
    </div>
    <div v-else>
        <div class="spinner-border text-light" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
    <div class="mt-2">
        <router-link class="text-white" to="/companies">Back to List</router-link>
    </div>

</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import store from '@/store'
import ICompany from '@/domain/ICompany'
import { BaseService } from '@/services/base-service'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class CompanyEdit extends Vue {
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
            if (data.statusCode >= 200 && data.statusCode < 300) {
                this.company = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        this.loading = false;
    }

    async editCompany(): Promise<void> {
        const service = new BaseService<ICompany>('v1/companies', this.token || undefined);
        await service.put(this.id, this.company).then((data) => {
            if (data.ok) {
                this.$router.push('/companies')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
