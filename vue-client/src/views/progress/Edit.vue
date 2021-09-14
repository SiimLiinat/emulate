<template>
    <body v-if="!loading">
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit review</h5>
                        <div class="form-signin">
                            <div class="form-group">
                                <label class="control-label" for="gameId">Game</label>
                                <select v-model="progress.gameId" class="form-control" id="gameId" name="gameId">
                                    <option v-for="game of games" v-bind:key="game.id" v-bind:value="game.id">
                                        {{ game.name }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label class="control-label" for="compatibilityTypeId">Compatibility</label>
                                <select v-model="progress.compatibilityTypeId" class="form-control" id="compatibilityTypeId" name="compatibilityTypeId">
                                    <option v-for="compatibilityType of compatibilityTypes" v-bind:key="compatibilityType.id" v-bind:value="compatibilityType.id">
                                        {{ compatibilityType.type }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label class="control-label" for="configurationId">Configuration</label>
                                <select v-model="progress.configurationId" class="form-control" id="configurationId" name="configurationId">
                                    <option v-for="configuration of configurations" v-bind:key="configuration.id" v-bind:value="configuration.id">
                                        {{ configuration.configurationString }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-label-group">
                                <input v-model="progress.time" type="number" id="inputTime" class="form-control" required>
                                <label for="inputTime">Time (hours)</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="progress.score" type="number" id="inputScore" class="form-control" required>
                                <label for="inputScore">Score (1-10)</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="progress.review" type="text" id="inputReview" class="form-control" placeholder="Review">
                                <label for="inputReview">Review</label>
                            </div>
                            <button @click="editProgress" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/progresses">Back to List</router-link>
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
import ICompatibilityType from '@/domain/ICompatibilityType';
import IConfiguration from '@/domain/IConfiguration';
import IGame from '@/domain/IGame';
import IProgress from '@/domain/IProgress';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class ProgressEdit extends Vue {
    id!: string;
    private loading: boolean = true;

    progress!: IProgress;

    compatibilityTypes: ICompatibilityType[] = [];
    configurations: IConfiguration[] = [];
    games: IGame[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        if (this.userId === undefined) await this.$router.push('/');
        const service = new BaseService<IProgress>('v1/progresses', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.progress = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        const configurationService = new BaseService<IConfiguration>('v1/configurations', this.token || undefined);
        configurationService.getAll().then((data) => {
            if (data.ok) {
                // TODO Filter in database
                this.configurations = data.data!.filter(c => c.appUserId === this.userId);
            } else {
                console.log(data)
            }
        });
        const compatibilityTypeService = new BaseService<ICompatibilityType>('v1/compatibilityTypes', this.token || undefined);
        compatibilityTypeService.getAll().then((data) => {
            if (data.ok) {
                this.compatibilityTypes = data.data!;
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

    async editProgress(): Promise<void> {
        const service = new BaseService<IProgress>('v1/progresses', this.token || undefined);
        await service.put(this.id, this.progress).then((data) => {
            if (data.ok) {
                this.$router.push('/progresses')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
