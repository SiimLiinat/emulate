<template>
    <body v-if="!loading">
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit media</h5>
                        <div class="form-signin">
                            <div class="form-group">
                                <label class="control-label" for="gameId">Game</label>
                                <select v-model="media.gameId" class="form-control" id="gameId" name="gameId">
                                    <option v-for="game of games" v-bind:key="game.id" v-bind:value="game.id">
                                        {{ game.name }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label class="control-label" for="mediaTypeId">Media type</label>
                                <select v-model="media.mediaTypeId" class="form-control" id="mediaTypeId" name="mediaTypeId">
                                    <option v-for="mediaType of mediaTypes" v-bind:key="mediaType.id" v-bind:value="mediaType.id">
                                        {{ mediaType.type }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-label-group">
                                <input v-model="media.url" type="text" id="inputUrl" class="form-control" placeholder="Url" required autofocus>
                                <label for="inputUrl">Url</label>
                            </div>
                            <button @click="editMedia" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
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
import { Options, Vue } from 'vue-class-component'
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IMedia from '@/domain/IMedia';
import IMediaType from '@/domain/IMediaType'
import IGame from '@/domain/IGame'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class MediaEdit extends Vue {
    id!: string;
    private loading: boolean = true;

    media!: IMedia;
    mediaTypes: IMediaType[] = [];
    games: IGame[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        if (this.userId === undefined) await this.$router.push('/');
        const service = new BaseService<IMedia>('v1/medias', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.media = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
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

    async editMedia(): Promise<void> {
        const service = new BaseService<IMedia>('v1/medias', this.token || undefined);
        await service.put(this.id, this.media).then((data) => {
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
