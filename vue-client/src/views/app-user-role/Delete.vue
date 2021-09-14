<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Remove role from user</h5>
                            <div class="form-signin">
                                <div class="form-group">
                                    <label class="control-label" for="userId">User</label>
                                    <select v-model="userId" class="form-control" id="userId" name="userId">
                                        <option v-for="user of users" v-bind:key="user.id" v-bind:value="user.id">
                                            {{ user.userName }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="roleId">Role</label>
                                    <select v-model="roleId" class="form-control" id="roleId" name="roleId">
                                        <option v-for="role of roles" v-bind:key="role.id" v-bind:value="role.id">
                                            {{ role.name }}
                                        </option>
                                    </select>
                                </div>
                                <button @click="deleteAppUserRole" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Delete</button>
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
import { Options, Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IGameGenre from '@/domain/IGameGenre'
import IAppUserRole from '@/domain/IAppUserRole'
import IAppUser from '@/domain/IAppUser'
import IAppRole from '@/domain/IAppRole'
@Options({
    components: {},
    props: {
    }
})
export default class UserRoleDelete extends Vue {
    userId!: string;
    roleId!: string;
    private loading: boolean = true;

    users: IAppUser[] = [];
    roles: IAppRole[] = [];

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const userService = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        userService.getAll().then((data) => {
            if (data.ok) {
                this.users = data.data!;
            } else {
                console.log(data)
            }
        });
        const roleService = new BaseService<IAppRole>('v1/appRoles', this.token || undefined);
        roleService.getAll().then((data) => {
            if (data.ok) {
                this.roles = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async deleteAppUserRole(): Promise<void> {
        const service = new BaseService<IGameGenre>('v1/appUserRoles', this.token || undefined);
        const appUserRole: IAppUserRole = { userId: this.userId, roleId: this.roleId }
        await service.delete(appUserRole.toString()).then((data) => {
            if (data.ok) {
                this.$router.push('/')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
