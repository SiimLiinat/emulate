<template>
    <body>
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create configuration</h5>
                            <div class="form-signin">
                                <div class="form-label-group">
                                    <input v-model="configurationString" type="text" id="inputType" class="form-control" placeholder="Config" required autofocus>
                                    <label for="inputType">Config</label>
                                </div>
                                <button @click="createConfiguration" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/configurations">Back to List</router-link>
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
import IConfigurationAdd from '@/domain/IConfigurationAdd';

export default class ConfigurationCreate extends Vue {
    configurationString!: string;

    get token(): string | undefined {
        return store.state.token;
    }

    get id(): string {
        return store.state.id ?? "";
    }

    async createConfiguration(): Promise<void> {
        if (this.id === "") await this.$router.push('/');
        const configuration: IConfigurationAdd = { appUserId: this.id, configurationString: this.configurationString };
        const service = new BaseService<IConfigurationAdd>('v1/configurations', this.token || undefined);
        await service.post(configuration).then((data) => {
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
