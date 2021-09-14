<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <div>
                <h4>User</h4>
                <hr />
                <dl class="row">
                    <dt class = "col-sm-2">
                        Email
                    </dt>
                    <dd class = "col-sm-10">
                        {{user.email}}
                    </dd>
                    <dt class = "col-sm-2">
                        Username
                    </dt>
                    <dd class = "col-sm-10">
                        {{user.userName}}
                    </dd>
                    <dt class = "col-sm-2">
                        Locked out
                    </dt>
                    <dd class = "col-sm-10">
                        {{user.lockoutEnd || "false"}}
                    </dd>
                </dl>
                <div>
                    <input @click="deleteUser" type="submit" value="Delete" class="btn btn-danger" />
                </div>
            </div>
        </div>
        <router-link to="/users">Back to List</router-link>
        <div v-if="loading">
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IAppUser from '@/domain/IAppUser'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class UserDelete extends Vue {
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
            }
        });
        this.loading = false;
    }

    async deleteUser(): Promise<void> {
        const service = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await service.delete(this.id).then((data) => {
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
