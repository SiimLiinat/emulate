<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Add media</h5>
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
                                    <label class="control-label" for="mediaTypeId">Media type</label>
                                    <select v-model="mediaTypeId" class="form-control" id="mediaTypeId" name="mediaTypeId">
                                        <option v-for="mediaType of mediaTypes" v-bind:key="mediaType.id" v-bind:value="mediaType.id">
                                            {{ mediaType.type }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="url" type="text" id="inputUrl" class="form-control" placeholder="Url" required autofocus>
                                    <label for="inputUrl">Url</label>
                                </div>
                                <button @click="createMedia" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/medias">Back to List</router-link>
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
import IMediaType from '@/domain/IMediaType';
import IGame from '@/domain/IGame';
import IMediaAdd from '@/domain/IMediaAdd';

export default class MediaCreate extends Vue {
    gameId: string | undefined;
    mediaTypeId!: string;
    url!: string;

    private loading: boolean = true;

    games: IGame[] = [];
    mediaTypes: IMediaType[] = [];

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        if (this.id === undefined) await this.$router.push('/');
        const mediaTypeService = new BaseService<IMediaType>('v1/mediaTypes', this.token || undefined);
        mediaTypeService.getAll().then((data) => {
            if (data.ok) {
                this.mediaTypes = data.data!;
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
        this.loading = false;
    }

    async createMedia(): Promise<void> {
        const media: IMediaAdd = { appUserId: this.id, gameId: this.gameId, mediaTypeId: this.mediaTypeId, url: this.url };
        const service = new BaseService<IMediaAdd>('v1/medias', this.token || undefined);
        await service.post(media).then((data) => {
            if (data.ok) {
                this.$router.push('/medias')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
