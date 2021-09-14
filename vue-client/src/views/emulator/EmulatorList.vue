<template>
    <div class="container card py-3 mb-5">
        <div class="row text-center mb-3">
            <div class="col-lg-7 mx-auto">
                <h3 class="display-5 my-3">Emulators</h3>
                <hr class="border border-dark">
            </div>
        </div>
        <div v-for="emulator of emulators" v-bind:key="emulator" class="row mb-2">
            <div class="col-lg-8 mx-auto">
                <ul class="list-group shadow">
                    <li class="list-group-item">
                        <div class="media align-items-lg-center flex-column flex-lg-row p-3">
                            <div class="media-body order-2 order-lg-1 align-text-bottom">
                                <h5 class="mt-0 font-weight-bold mb-2">{{ emulator.name }}</h5>
                                <a v-bind:href="emulator.url" target="_blank" class="mb-0">{{ emulator.url }}</a>
                                <hr>
                                <div class="d-flex align-items-center justify-content-center mt-1">
                                    <h6 class="font-weight-bold my-2">Platform: {{ emulator.platformName }}</h6>
                                </div>
                                <div class="d-flex align-items-center justify-content-center mt-1">
                                    <h6 class="font-weight-bold my-2">Programs: {{ emulator.programCount }}</h6>
                                </div>
                            </div><img v-bind:src="emulator.picture" alt="Generic placeholder image" width="200" class="ml-lg-5 order-1 order-lg-2">
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import IEmulator from '@/domain/IEmulator';
import store from '@/store';
import { BaseService } from '@/services/base-service'

export default class EmulatorList extends Vue {
    private emulators: IEmulator[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IEmulator>('v1/emulators', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.emulators = data.data!.sort((a, b) => (a.name < b.name ? -1 : 1));
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
