<template>
    <div class="card card-body mt-5">
        <h1>Games</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/game/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Developer
                    </th>
                    <th>
                        Publisher
                    </th>
                    <th>
                        Release Date
                    </th>
                    <th>
                        Compatibility
                    </th>
                    <th>
                        Percentage
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="game of games" v-bind:key="game">
                    <td>
                        {{game.name}}
                    </td>
                    <td>
                        {{game.devCompanyName}}
                    </td>
                    <td>
                        {{game.pubCompanyName}}
                    </td>
                    <td>
                        {{game.releaseDate}}
                    </td>
                    <td>
                        {{game.compatibilityType}}
                    </td>
                    <td>
                        {{game.compatibilityPercentage}}
                    </td>
                    <td>
                        <router-link :to="'/game/edit/' + game.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/game/details/' + game.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/game/delete/' + game.id">Delete</router-link>
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
import IGame from '@/domain/IGame'

export default class GameIndex extends Vue {
    private games: IGame[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGame>('v1/games', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.games = data.data!;
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
