<template>
    <body>
    <div v-if="!loading" class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit role</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="role.name" type="text" id="inputName" class="form-control" placeholder="Name" required autofocus>
                                <label for="inputName">Type</label>
                            </div>
                            <button @click="editRole" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Edit</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/roles">Back to List</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div v-else>
        <div class="spinner-border text-primary" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
    </body>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IAppRole from '@/domain/IAppRole'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class RoleEdit extends Vue {
    id!: string;
    private role!: IAppRole;
    private loading: boolean = true;

    get userRole(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.userRole !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IAppRole>('v1/appRoles', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.role = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        this.loading = false;
    }

    async editRole(): Promise<void> {
        const service = new BaseService<IAppRole>('v1/appRoles', this.token || undefined);
        await service.put(this.id, this.role).then((data) => {
            if (data.ok) {
                this.$router.push('/roles')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
