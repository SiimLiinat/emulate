<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create review</h5>
                            <div class="form-signin">
                                <div v-show="!setGameId" class="form-group">
                                    <label class="control-label" for="gameId">Game</label>
                                    <select v-model="gameId" class="form-control" id="gameId" name="gameId">
                                        <option v-for="game of games" v-bind:key="game.id" v-bind:value="game.id">
                                            {{ game.name }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="compatibilityTypeId">Compatibility</label>
                                    <select v-model="compatibilityTypeId" class="form-control" id="compatibilityTypeId" name="compatibilityTypeId">
                                        <option v-for="compatibilityType of compatibilityTypes" v-bind:key="compatibilityType.id" v-bind:value="compatibilityType.id">
                                            {{ compatibilityType.type }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="configurationId">Configuration</label>
                                    <select v-model="configurationId" class="form-control" id="configurationId" name="configurationId">
                                        <option v-for="configuration of configurations" v-bind:key="configuration.id" v-bind:value="configuration.id">
                                            {{ configuration.configurationString }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="time" type="number" id="inputTime" class="form-control" required>
                                    <label for="inputTime">Time (hours)</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="score" type="number" id="inputScore" class="form-control" required>
                                    <label for="inputScore">Score (1-10)</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="review" type="text" id="inputReview" class="form-control" placeholder="Review">
                                    <label for="inputReview">Review</label>
                                </div>
                                <button @click="createProgress" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div v-if="!setGameId">
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
import IProgressAdd from '@/domain/IProgressAdd';
import IConfiguration from '@/domain/IConfiguration';
import ICompatibilityType from '@/domain/ICompatibilityType';
import IGame from '@/domain/IGame';
@Options({
    components: {},
    props: {
        setGameId: String,
        returnLink: String,
    }
})
export default class ProgressCreate extends Vue {
    setGameId?: string;
    returnLink?: string;

    gameId!: string;
    configurationId: string | undefined;
    compatibilityTypeId: string | undefined;
    public: boolean = true;
    time!: number;
    score!: number;
    review: string | undefined;

    private loading: boolean = true;

    compatibilityTypes: ICompatibilityType[] = [];
    configurations: IConfiguration[] = [];
    games: IGame[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        if (store.state.id === undefined) await this.$router.push('/login');
        if (this.setGameId) this.gameId = this.setGameId;
        const configurationService = new BaseService<IConfiguration>('v1/configurations', this.token || undefined);
        configurationService.getAll().then((data) => {
            if (data.ok) {
                // TODO Filter in database
                this.configurations = data.data!.filter(c => c.appUserId === this.id);
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

    async createProgress(): Promise<void> {
        const progress: IProgressAdd = {
            appUserId: this.id!,
            gameId: this.gameId,
            configurationId: this.configurationId,
            compatibilityTypeId: this.compatibilityTypeId,
            public: true,
            time: this.time,
            score: this.score % 10,
            review: this.review
        };
        const service = new BaseService<IProgressAdd>('v1/progresses', this.token || undefined);
        await service.post(progress).then((data) => {
            if (data.ok) {
                if (this.returnLink) this.$router.push(this.returnLink);
                else this.$router.push('/progresses');
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
