<template>
    <body>
    <div v-if="!loading" class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit configuration</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="configuration.configurationString" type="text" id="inputType" class="form-control" placeholder="Config" required autofocus>
                                <label for="inputType">Config</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="configuration.creationDt" type="text" id="inputDescription" class="form-control" placeholder="Creation time" required>
                                <label for="inputDescription">Creation time</label>
                            </div>
                            <button @click="editConfiguration" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Edit</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/configurations">Back to List</router-link>
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
import IConfiguration from '@/domain/IConfiguration';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class ConfigurationEdit extends Vue {
    id!: string;
    private configuration!: IConfiguration;
    private loading: boolean = true;

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IConfiguration>('v1/configurations', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.configuration = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        if (this.role !== 'Admin' && this.configuration.appUserId !== this.userId) await this.$router.push('/');
        this.loading = false;
    }

    async editConfiguration(): Promise<void> {
        const service = new BaseService<IConfiguration>('v1/configurations', this.token || undefined);
        await service.put(this.id, this.configuration).then((data) => {
            if (data.ok) {
                this.$router.push('/configurations')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
