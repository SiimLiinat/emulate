<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create emulator</h5>
                            <div class="form-signin">
                                <div class="form-group">
                                    <label class="control-label" for="platformId">PlatformId</label>
                                    <select v-model="platformId" class="form-control" id="platformId" name="platformId">
                                        <option v-for="platform of platforms" v-bind:key="platform.id" v-bind:value="platform.id">
                                            {{ platform.name }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="name" type="text" id="inputName" class="form-control" placeholder="Name" required>
                                    <label for="inputName">Name</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="url" type="text" id="inputUrl" class="form-control" placeholder="Url" required>
                                    <label for="inputUrl">Url</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="picture" type="text" id="inputPicture" class="form-control" placeholder="Picture" required>
                                    <label for="inputPicture">Picture</label>
                                </div>
                                <button @click="createEmulator" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/emulators">Back to List</router-link>
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
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IPlatform from '@/domain/IPlatform';
import IEmulatorAdd from '@/domain/IEmulatorAdd';

export default class EmulatorCreate extends Vue {
    platformId: string = "";
    name: string = "";
    url: string = "";
    picture!: string;

    private loading: boolean = true;

    platforms: IPlatform[] = [];

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const platformService = new BaseService<IPlatform>('v1/platforms', this.token || undefined);
        platformService.getAll().then((data) => {
            if (data.ok) {
                this.platforms = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async createEmulator(): Promise<void> {
        const emulator: IEmulatorAdd = { platformId: this.platformId, name: this.name, url: this.url, picture: this.picture };
        const service = new BaseService<IEmulatorAdd>('v1/emulators', this.token || undefined);
        await service.post(emulator).then((data) => {
            if (data.ok) {
                this.$router.push('/emulators')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
