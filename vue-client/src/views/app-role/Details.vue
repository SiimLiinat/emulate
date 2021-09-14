<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <div>
                <h4>Role</h4>
                <hr />
                <dl class="row">
                    <dt class = "col-sm-2">
                        Name
                    </dt>
                    <dd class = "col-sm-10">
                        {{appRole.name}}
                    </dd>
                </dl>
            </div>
        </div>
        <div v-if="!loading">
            <router-link v-if="role === 'Admin'" :to="'/role/edit/' + appRole.id">Edit</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link v-if="role === 'Admin'" :to="'/role/delete/' + appRole.id">Delete</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link to="/roles">Back to List</router-link>
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
import IAppRole from '@/domain/IAppRole';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class RoleDetails extends Vue {
    id!: string;
    private appRole!: IAppRole;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IAppRole>('v1/appRoles', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.appRole = data.data!;
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
