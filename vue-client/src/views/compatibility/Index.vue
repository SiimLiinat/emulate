<template>
    <div class="card card-body mt-5">
        <h1>Compatibilities</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/compatibility/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Game
                    </th>
                    <th>
                        Emulator
                    </th>
                    <th>
                        Compatibility
                    </th>
                    <th>
                        Date
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="compatibility of compatibilities" v-bind:key="compatibility">
                    <td>
                        {{compatibility.gameName}}
                    </td>
                    <td>
                        {{compatibility.emulatorName}}
                    </td>
                    <td>
                        {{compatibility.compatibilityTypeType}}
                    </td>
                    <td>
                        {{compatibility.date}}
                    </td>
                    <td>
                        <router-link v-if="role === 'Admin'" :to="'/compatibility/edit/' + compatibility.id">Edit</router-link>
                        <span v-if="role === 'Admin'" > | </span>
                        <router-link :to="'/compatibility/details/' + compatibility.id">Details</router-link>
                        <span v-if="role === 'Admin'" > | </span>
                        <router-link v-if="role === 'Admin'" :to="'/compatibility/delete/' + compatibility.id">Delete</router-link>
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
import ICompatibility from '@/domain/ICompatibility';

export default class CompatibilityIndex extends Vue {
    private compatibilities: ICompatibility[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<ICompatibility>('v1/compatibilities', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.compatibilities = data.data!;
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
