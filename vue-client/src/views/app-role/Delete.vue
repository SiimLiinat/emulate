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
                <div>
                    <input @click="deleteRole" type="submit" value="Delete" class="btn btn-danger" />
                </div>
            </div>
        </div>
        <router-link to="/roles">Back to List</router-link>
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
import IAppRole from '@/domain/IAppRole';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class RoleDelete extends Vue {
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

    async deleteRole(): Promise<void> {
        const service = new BaseService<IAppRole>('v1/appRoles', this.token || undefined);
        await service.delete(this.id).then((data) => {
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
