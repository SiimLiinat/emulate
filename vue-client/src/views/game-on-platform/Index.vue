<template>
    <div class="card card-body mt-5">
        <h1>Games on platforms</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/game-on-platform/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Game
                    </th>
                    <th>
                        Platform
                    </th>
                    <th>
                        Release date
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="gameOnPlatform of gameOnPlatforms" v-bind:key="gameOnPlatform">
                    <td>
                        {{gameOnPlatform.gameName}}
                    </td>
                    <td>
                        {{gameOnPlatform.platformName}}
                    </td>
                    <td>
                        {{gameOnPlatform.releaseDate}}
                    </td>
                    <td>
                        <router-link :to="'/game-on-platform/edit/' + gameOnPlatform.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/game-on-platform/details/' + gameOnPlatform.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/game-on-platform/delete/' + gameOnPlatform.id">Delete</router-link>
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
import IGameOnPlatform from '@/domain/IGameOnPlatform'

export default class GameOnPlatformIndex extends Vue {
    private gameOnPlatforms: IGameOnPlatform[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGameOnPlatform>('v1/gameOnPlatforms', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.gameOnPlatforms = data.data!;
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
