<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Add platform to game</h5>
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
                                    <label class="control-label" for="platformId">Platform</label>
                                    <select v-model="platformId" class="form-control" id="platformId" name="platformId">
                                        <option v-for="platform of platforms" v-bind:key="platform.id" v-bind:value="platform.id">
                                            {{ platform.name }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="releaseDate" type="text" id="releaseDate" class="form-control" placeholder="Release Date" required>
                                    <label for="releaseDate">Release Date</label>
                                </div>
                                <button @click="createGameOnPlatform" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/game-on-platforms">Back to List</router-link>
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
import IGame from '@/domain/IGame';
import IPlatform from '@/domain/IPlatform';
import IGameOnPlatformAdd from '@/domain/IGameOnPlatformAdd'

export default class GameOnPlatformCreate extends Vue {
    gameId!: string;
    platformId!: string;
    releaseDate!: string;

    private loading: boolean = true;

    games: IGame[] = [];
    platforms: IPlatform[] = [];

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
        const platformService = new BaseService<IPlatform>('v1/platforms', this.token || undefined);
        platformService.getAll().then((data) => {
            if (data.ok) {
                this.platforms = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async createGameOnPlatform(): Promise<void> {
        const gameOnPlatform: IGameOnPlatformAdd = { gameId: this.gameId, platformId: this.platformId, releaseDate: this.releaseDate };
        const service = new BaseService<IGameOnPlatformAdd>('v1/gameOnPlatforms', this.token || undefined);
        await service.post(gameOnPlatform).then((data) => {
            if (data.ok) {
                this.$router.push('/game-on-platforms')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
