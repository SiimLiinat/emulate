<template>
    <body v-if="!loading">
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit platform</h5>
                        <div class="form-signin">
                            <div class="form-group">
                                <label class="control-label" for="companyId">CompanyId</label>
                                <select v-model="platform.companyId" class="form-control" id="companyId" name="companyId">
                                    <option v-for="company of companies" v-bind:key="company.id" v-bind:value="company.id">
                                        {{ company.name }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label class="control-label" for="platformTypeId">PlatformTypeId</label>
                                <select v-model="platform.platformTypeId" class="form-control" id="platformTypeId" name="platformTypeId">
                                    <option v-for="platformType of platformTypes" v-bind:key="platformType.id" v-bind:value="platformType.id">
                                        {{ platformType.type }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-label-group">
                                <input v-model="platform.name" type="text" id="inputName" class="form-control" placeholder="Name" required>
                                <label for="inputName">Name</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="platform.code" type="text" id="inputCode" class="form-control" placeholder="Code" required>
                                <label for="inputCode">Code</label>
                            </div>
                            <button @click="editPlatform" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/platforms">Back to List</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </body>
    <div v-else>
        <div class="spinner-border text-light" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import store from '@/store';
import { BaseService } from '@/services/base-service';
import ICompany from '@/domain/ICompany'
import IPlatformType from '@/domain/IPlatformType'
import IPlatform from '@/domain/IPlatform'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class PlatformEdit extends Vue {
    id!: string;
    private loading: boolean = true;

    platform!: IPlatform;
    platformTypes: IPlatformType[] = [];
    companies: ICompany[] = [];

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IPlatform>('v1/platforms', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.platform = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        const platformTypeService = new BaseService<IPlatformType>('v1/platformtypes', this.token || undefined);
        platformTypeService.getAll().then((data) => {
            if (data.ok) {
                this.platformTypes = data.data!;
            } else {
                console.log(data)
            }
        });
        const companyService = new BaseService<ICompany>('v1/companies', this.token || undefined);
        companyService.getAll().then((data) => {
            if (data.ok) {
                this.companies = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async editPlatform(): Promise<void> {
        const service = new BaseService<IPlatform>('v1/platforms', this.token || undefined);
        await service.put(this.id, this.platform).then((data) => {
            if (data.ok) {
                this.$router.push('/platforms')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
