<template>
    <body v-if="!loading">
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit platform</h5>
                        <div class="form-signin">
                            <div class="form-group">
                                <label class="control-label" for="platformId">PlatformId</label>
                                <select v-model="emulator.platformId" class="form-control" id="platformId" name="platformId">
                                    <option v-for="platform of platforms" v-bind:key="platform.id" v-bind:value="platform.id">
                                        {{ platform.name }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-label-group">
                                <input v-model="emulator.name" type="text" id="inputName" class="form-control" placeholder="Name" required>
                                <label for="inputName">Name</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="emulator.url" type="text" id="inputUrl" class="form-control" placeholder="Url" required>
                                <label for="inputUrl">Url</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="emulator.picture" type="text" id="inputPicture" class="form-control" placeholder="Picture" required>
                                <label for="inputPicture">Picture</label>
                            </div>
                            <button @click="editEmulator" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
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
import { Options, Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IPlatform from '@/domain/IPlatform';
import IEmulator from '@/domain/IEmulator';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class EmulatorEdit extends Vue {
    id!: string;
    private loading: boolean = true;

    emulator!: IEmulator;
    platforms: IPlatform[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IEmulator>('v1/emulators', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.emulator = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
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

    async editEmulator(): Promise<void> {
        const service = new BaseService<IEmulator>('v1/emulators', this.token || undefined);
        await service.put(this.id, this.emulator).then((data) => {
            if (data.statusCode >= 200 && data.statusCode < 300) {
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
