<template>
    <body>
    <div v-if="!loading" class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit user</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="user.email" type="email" id="inputEmail" class="form-control" placeholder="Email" required autofocus>
                                <label for="inputEmail">Type</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="user.lockoutEnd" type="text" id="inputReleaseDate" class="form-control" placeholder="Locked out until" required>
                                <label for="inputReleaseDate">Locked out until</label>
                            </div>
                            <button @click="editUser" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Edit</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/users">Back to List</router-link>
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
import IAppUser from '@/domain/IAppUser';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class UserEdit extends Vue {
    id!: string;
    private user!: IAppUser;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.user = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        this.loading = false;
    }

    async editUser(): Promise<void> {
        const service = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await service.put(this.id, this.user).then((data) => {
            if (data.ok) {
                this.$router.push('/users')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
