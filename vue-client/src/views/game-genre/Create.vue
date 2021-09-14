<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Add genre to game</h5>
                            <div class="form-signin">
                                <div class="form-group">
                                    <label class="control-label" for="gameId">Game</label>
                                    <select v-model="gameId" class="form-control" id="gameId" name="gameId">
                                        <option v-for="game of games" v-bind:key="game.id" v-bind:value="game.id">
                                            {{ game.name }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="genreId">Genre</label>
                                    <select v-model="genreId" class="form-control" id="genreId" name="genreId">
                                        <option v-for="genre of genres" v-bind:key="genre.id" v-bind:value="genre.id">
                                            {{ genre.type }}
                                        </option>
                                    </select>
                                </div>
                                <button @click="createGameGenre" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/game-genres">Back to List</router-link>
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
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IGameGenreAdd from '@/domain/IGameGenreAdd'
import IGenre from '@/domain/IGenre'
import IGame from '@/domain/IGame'

export default class GameGenreCreate extends Vue {
    gameId!: string;
    genreId!: string;

    private loading: boolean = true;

    games: IGame[] = [];
    genres: IGenre[] = [];

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const gameService = new BaseService<IGame>('v1/games', this.token || undefined);
        gameService.getAll().then((data) => {
            if (data.ok) {
                this.games = data.data!;
            } else {
                console.log(data)
            }
        });
        const genreService = new BaseService<IGenre>('v1/genres', this.token || undefined);
        genreService.getAll().then((data) => {
            if (data.ok) {
                this.genres = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async createGameGenre(): Promise<void> {
        const gameGenre: IGameGenreAdd = { gameId: this.gameId, genreId: this.genreId };
        const service = new BaseService<IGameGenreAdd>('v1/gameGenres', this.token || undefined);
        await service.post(gameGenre).then((data) => {
            if (data.ok) {
                this.$router.push('/game-genres')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
