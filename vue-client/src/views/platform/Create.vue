<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create platform</h5>
                            <div class="form-signin">
                                <div class="form-group">
                                    <label class="control-label" for="companyId">CompanyId</label>
                                    <select v-model="companyId" class="form-control" id="companyId" name="companyId">
                                        <option v-for="company of companies" v-bind:key="company.id" v-bind:value="company.id">
                                            {{ company.name }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="platformTypeId">PlatformTypeId</label>
                                    <select v-model="platformTypeId" class="form-control" id="platformTypeId" name="platformTypeId">
                                        <option v-for="platformType of platformTypes" v-bind:key="platformType.id" v-bind:value="platformType.id">
                                            {{ platformType.type }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="name" type="text" id="inputName" class="form-control" placeholder="Name" required>
                                    <label for="inputName">Name</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="code" type="text" id="inputCode" class="form-control" placeholder="Code" required>
                                    <label for="inputCode">Code</label>
                                </div>
                                <button @click="createPlatform" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
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
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IPlatformAdd from '@/domain/IPlatformAdd';
import ICompany from '@/domain/ICompany'
import IPlatformType from '@/domain/IPlatformType'

export default class PlatformCreate extends Vue {
    companyId: string = "";
    platformTypeId: string = "";
    name: string = "";
    code: string = "";

    private loading: boolean = true;

    platformTypes: IPlatformType[] = [];
    companies: ICompany[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
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

    async createPlatform(): Promise<void> {
        const platform: IPlatformAdd = { companyId: this.companyId, platformTypeId: this.platformTypeId, name: this.name, code: this.code };
        const service = new BaseService<IPlatformAdd>('v1/platforms', this.token || undefined);
        await service.post(platform).then((data) => {
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
