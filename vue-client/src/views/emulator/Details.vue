<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Platform</h4>
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
        </div>
        <div v-if="!loading">
            <router-link v-if="role === 'Admin'" :to="'/emulator/edit/' + emulator.id">Edit</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link v-if="role === 'Admin'" :to="'/emulator/delete/' + emulator.id">Delete</router-link>
            <span v-if="role === 'Admin'"> | </span>
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
export default class EmulatorDetails extends Vue {
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
}
</script>

<style scoped>

</style>
