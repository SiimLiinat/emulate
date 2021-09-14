<template>
    <body>
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create company</h5>
                            <div class="form-signin">
                                <div class="form-label-group">
                                    <input v-model="name" type="text" id="inputName" class="form-control" placeholder="Name" required autofocus>
                                    <label for="inputName">Name</label>
                                </div>
                                <button @click="createCompany" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/companies">Back to List</router-link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import { ICompanyAdd } from '@/domain/ICompanyAdd';

export default class CompanyCreate extends Vue {
    name: string = "";

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    mounted(): void {
        if (this.role !== 'Admin') this.$router.push('/');
    }

    async createCompany(): Promise<void> {
        const company: ICompanyAdd = { name: this.name }
        const service = new BaseService<ICompanyAdd>('v1/companies', this.token || undefined);
        await service.post(company).then((data) => {
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
