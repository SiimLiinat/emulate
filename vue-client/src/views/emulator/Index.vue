<template>
    <div class="card card-body mt-5">
        <h1>Emulators</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/emulator/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Platform
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Url
                    </th>
                    <th>
                        Picture
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="emulator of emulators" v-bind:key="emulator">
                    <td>
                        {{emulator.platformName}}
                    </td>
                    <td>
                        {{emulator.name}}
                    </td>
                    <td>
                        {{emulator.url}}
                    </td>
                    <td>
                        {{emulator.picture}}
                    </td>
                    <td>
                        <router-link :to="'/emulator/edit/' + emulator.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/emulator/details/' + emulator.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/emulator/delete/' + emulator.id">Delete</router-link>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
        <div v-else>
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IEmulator from '@/domain/IEmulator'

export default class EmulatorIndex extends Vue {
    private emulators: IEmulator[] = [];
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
        await service.getAll().then((data) => {
            if (data.ok) {
                this.emulators = data.data!;
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
