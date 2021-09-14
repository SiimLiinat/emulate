<template>
    <div class="card card-body mt-5">
        <h1>Game Genres</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/game-genre/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Game
                    </th>
                    <th>
                        Genre
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="gameGenre of gameGenres" v-bind:key="gameGenre">
                    <td>
                        {{gameGenre.gameName}}
                    </td>
                    <td>
                        {{gameGenre.genreType}}
                    </td>
                    <td>
                        <router-link :to="'/game-genre/edit/' + gameGenre.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/game-genre/details/' + gameGenre.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/game-genre/delete/' + gameGenre.id">Delete</router-link>
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
import IGameGenre from '@/domain/IGameGenre';

export default class GameGenreIndex extends Vue {
    private gameGenres: IGameGenre[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGameGenre>('v1/gameGenres', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.gameGenres = data.data!;
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
