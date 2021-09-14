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
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/user/edit/' + user.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/user/delete/' + user.id">Delete</router-link>
            <span> | </span>
            <router-link to="/users">Back to List</router-link>
        </div>
        <div v-else>
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
export default class UserDetails extends Vue {
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
}
</script>

<style scoped>

</style>
