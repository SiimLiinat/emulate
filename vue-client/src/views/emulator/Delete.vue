<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Emulator</h4>
            <hr />

            <dl class="row">
                <dt class = "col-sm-2">
                    Platform
                </dt>
                <dd class = "col-sm-10">
                    {{emulator.platformName}}
                </dd>
                <dt class = "col-sm-2">
                    Name
                </dt>
                <dd class = "col-sm-10">
                    {{emulator.name}}
                </dd>
                <dt class = "col-sm-2">
                    Url
                </dt>
                <dd class = "col-sm-10">
                    {{emulator.url}}
                </dd>
                <dt class = "col-sm-2">
                    Picture
                </dt>
                <dd class = "col-sm-10">
                    {{emulator.picture}}
                </dd>
            </dl>
            <div>
                <input @click="deleteEmulator" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/emulator/edit/' + emulator.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/emulator/details/' + emulator.id">Details</router-link>
            <span> | </span>
            <router-link to="/emulators">Back to List</router-link>
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
import IEmulator from '@/domain/IEmulator'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class EmulatorDelete extends Vue {
    id!: string;
    private emulator!: IEmulator;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IEmulator>('v1/emulators', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.emulator = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteEmulator(): Promise<void> {
        const service = new BaseService<IEmulator>('v1/emulators', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/emulators')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
