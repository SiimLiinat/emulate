<template>
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Create role</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="name" type="text" id="inputType" class="form-control" placeholder="Name" required autofocus>
                                <label for="inputType">Name</label>
                            </div>
                            <button @click="createRole" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/roles">Back to List</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IAppRoleAdd from '@/domain/IAppRoleAdd';

export default class RoleCreate extends Vue {
    name!: string;

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    mounted(): void {
        if (this.role !== 'Admin') this.$router.push('/');
    }

    async createRole(): Promise<void> {
        const role: IAppRoleAdd = { name: this.name };
        const service = new BaseService<IAppRoleAdd>('v1/appRoles', this.token || undefined);
        await service.post(role).then((data) => {
            if (data.ok) {
                this.$router.push('/roles')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
