<template>
    <body v-if="!loading">
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit compatibility</h5>
                        <div class="form-signin">
                            <div class="form-group">
                                <label class="control-label" for="compatibilityTypeId">Compatibility</label>
                                <select v-model="compatibility.compatibilityTypeId" class="form-control" id="compatibilityTypeId" name="compatibilityTypeId">
                                    <option v-for="compatibilityType of compatibilityTypes" v-bind:key="compatibilityType.id" v-bind:value="compatibilityType.id">
                                        {{ compatibilityType.type }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label class="control-label" for="emulatorId">Emulator</label>
                                <select v-model="compatibility.emulatorId" class="form-control" id="emulatorId" name="emulatorId">
                                    <option v-for="emulator of emulators" v-bind:key="emulator.id" v-bind:value="emulator.id">
                                        {{ emulator.name }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label class="control-label" for="gameOnPlatformId">Game</label>
                                <select v-model="compatibility.gameOnPlatformId" class="form-control" id="gameOnPlatformId" name="gameOnPlatformId">
                                    <option v-for="gameOnPlatform of gameOnPlatforms" v-bind:key="gameOnPlatform.id" v-bind:value="gameOnPlatform.id">
                                        {{ gameOnPlatform.gameName + " on " + gameOnPlatform.platformName }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-label-group">
                                <input v-model="compatibility.date" type="text" id="inputDate" class="form-control" placeholder="Date" required>
                                <label for="inputDate">Date</label>
                            </div>
                            <button @click="editCompatibility" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/compatibilities">Back to List</router-link>
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
import { Options, Vue } from 'vue-class-component'
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IGameOnPlatform from '@/domain/IGameOnPlatform'
import IEmulator from '@/domain/IEmulator'
import ICompatibilityType from '@/domain/ICompatibilityType'
import ICompatibility from '@/domain/ICompatibility'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class CompatibilityEdit extends Vue {
    id!: string;
    private loading: boolean = true;

    compatibility!: ICompatibility;
    compatibilityTypes: ICompatibilityType[] = [];
    emulators: IEmulator[] = [];
    gameOnPlatforms: IGameOnPlatform[] = [];

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<ICompatibility>('v1/compatibilities', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.compatibility = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        const compatibilityTypeService = new BaseService<ICompatibilityType>('v1/compatibilityTypes', this.token || undefined);
        compatibilityTypeService.getAll().then((data) => {
            if (data.ok) {
                this.compatibilityTypes = data.data!;
            } else {
                console.log(data)
            }
        });
        const emulatorService = new BaseService<IEmulator>('v1/emulators', this.token || undefined);
        emulatorService.getAll().then((data) => {
            if (data.ok) {
                this.emulators = data.data!;
            } else {
                console.log(data)
            }
        });
        const gameOnPlatformService = new BaseService<IGameOnPlatform>('v1/gameOnPlatforms', this.token || undefined);
        gameOnPlatformService.getAll().then((data) => {
            if (data.ok) {
                this.gameOnPlatforms = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async editCompatibility(): Promise<void> {
        const service = new BaseService<ICompatibility>('v1/compatibilities', this.token || undefined);
        await service.put(this.id, this.compatibility).then((data) => {
            if (data.ok) {
                this.$router.push('/compatibilities')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
