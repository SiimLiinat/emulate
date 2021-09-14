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
                                    <select v-model="gameOnPlatform.gameId" class="form-control" id="gameId" name="gameId">
                                        <option v-for="game of games" v-bind:key="game.id" v-bind:value="game.id">
                                            {{ game.name }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="platformId">Platform</label>
                                    <select v-model="gameOnPlatform.platformId" class="form-control" id="platformId" name="platformId">
                                        <option v-for="platform of platforms" v-bind:key="platform.id" v-bind:value="platform.id">
                                            {{ platform.name }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="gameOnPlatform.releaseDate" type="text" id="releaseDate" class="form-control" placeholder="Release Date" required>
                                    <label for="releaseDate">Release Date</label>
                                </div>
                                <button @click="editGameOnPlatform" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Edit</button>
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
import { Options, Vue } from 'vue-class-component'
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IGame from '@/domain/IGame';
import IPlatform from '@/domain/IPlatform';
import IGameOnPlatformAdd from '@/domain/IGameOnPlatformAdd'
import IGameOnPlatform from '@/domain/IGameOnPlatform'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class GameOnPlatformEdit extends Vue {
    id!: string;
    private gameOnPlatform!: IGameOnPlatform;

    private loading: boolean = true;

    games: IGame[] = [];
    platforms: IPlatform[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGameOnPlatform>('v1/gameOnPlatforms', this.token || undefined);
        service.get(this.id).then((data) => {
            if (data.ok) {
                this.gameOnPlatform = data.data!;
            } else {
                console.log(data)
            }
        });
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

    async editGameOnPlatform(): Promise<void> {
        const service = new BaseService<IGameOnPlatformAdd>('v1/gameOnPlatforms', this.token || undefined);
        await service.put(this.id, this.gameOnPlatform).then((data) => {
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
