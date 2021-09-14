<template>
    <div class="card">
        <div v-if="!loading" class="container">
            <div class="main-body">
                <div class="row gutters-sm">
                    <div class="col-md-8">
                        <div class="card mb-3 mt-3">
                            <!-- Carousel wrapper -->
                            <div
                                id="carouselBasicExample"
                                class="carousel slide carousel-fade"
                                data-mdb-ride="carousel"
                            >
                                <!-- Indicators -->
                                <div class="carousel-indicators">
                                    <div v-if="medias.length > 0" >
                                        <button v-for="(media, index) in medias" v-bind:key="media"
                                                :class="{'active': index === 0}"
                                                :aria-current="index === 0"
                                                type="button"
                                                data-mdb-target="#carouselBasicExample"
                                                v-bind:data-mdb-slide-to="index"
                                                v-bind:aria-label="'Slide ' + (index + 1)"
                                        ></button>
                                    </div>
                                    <div v-else>
                                        <button type="button"
                                                data-mdb-target="#carouselBasicExample"
                                                data-mdb-slide-to="0"
                                                class="active"
                                                aria-current="true"
                                                aria-label="Slide 1">
                                        </button>
                                    </div>
                                </div>

                                <!-- Inner -->
                                <div class="carousel-inner">
                                    <!-- Single item -->
                                    <div v-if="medias.length > 0" >
                                        <div v-for="(media, index) in medias" v-bind:key="media" :class="{'active': index === 0}" class="carousel-item">
                                            <img
                                                v-bind:src="media.url"
                                                class="d-block w-100"
                                                alt="..."
                                            />
                                            <div class="carousel-caption d-none d-md-block">
                                                <h5>{{ media.userName }}</h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div v-else>
                                        <div class="carousel-item active">
                                            <img
                                                src="https://i.stack.imgur.com/6M513.png"
                                                class="d-block w-100"
                                                alt="..."
                                            />
                                        </div>
                                    </div>
                                </div>
                                <!-- Inner -->

                                <!-- Controls -->
                                <button
                                    class="carousel-control-prev"
                                    type="button"
                                    data-mdb-target="#carouselBasicExample"
                                    data-mdb-slide="prev"
                                >
                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span class="visually-hidden">Previous</span>
                                </button>
                                <button
                                    class="carousel-control-next"
                                    type="button"
                                    data-mdb-target="#carouselBasicExample"
                                    data-mdb-slide="next"
                                >
                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span class="visually-hidden">Next</span>
                                </button>
                            </div>
                            <!-- Carousel wrapper -->
                        </div>

                        <div class="row gutters-sm">
                            <div class="col-sm-12 mb-3">
                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="mb-3">Official compatibilities</h4>
                                        <div v-for="compatibility of compatibilities" v-bind:key="compatibility">
                                            <h6 class="font-weight-bold">{{ compatibility.emulatorName }} - {{ compatibility.platformName }}
                                                <span v-bind:class="'text-' + getColor(compatibility.compatibilityTypeRank)"
                                                      class="ml-4">{{ compatibility.compatibilityTypeType }}</span>
                                                <span class="ml-4">{{ new Date(compatibility.date).toLocaleDateString()}}</span></h6>
                                            <div class="progress mb-3" style="height: 5px">
                                                <div v-bind:class="'bg-' + getColor(compatibility.compatibilityTypeRank)" class="progress-bar" role="progressbar"
                                                     v-bind:style="getProgressBarStyle(compatibility.compatibilityTypeRank)"
                                                     aria-valuenow="80" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="d-flex flex-column align-items-center text-center">
                                    <div id="img-holder" class="mr-md-4 mb-md-0 mb-4 mx-auto mx-md-0 d-md-none d-lg-flex">
                                        <img v-if="game.coverArt" class="img-fluid" v-bind:src="game.coverArt">
                                        <img v-else class="img-fluid" src="https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png">
                                    </div>
                                    <div class="mt-3">
                                        <h3 class="font-weight-bold">{{ game.name }}</h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card mt-3">
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                    <h6 class="mb-0 font-weight-bold">Publisher</h6>
                                    <span class="text-dark">{{ game.pubCompanyName }}</span>
                                </li>
                                <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                    <h6 class="mb-0 font-weight-bold">Developer</h6>
                                    <span class="text-dark">{{ game.devCompanyName }}</span>
                                </li>
                                <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                    <h6 class="mb-0 font-weight-bold">Release Date</h6>
                                    <span class="text-dark">{{ new Date(game.releaseDate).toLocaleDateString() }}</span>
                                </li>
                                <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                    <h6 class="mb-0 font-weight-bold">Genres</h6>
                                    <span class="text-dark">{{ game.genres.join(', ') }}</span>
                                </li>
                                <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                    <h6 class="mb-0 font-weight-bold">Platforms</h6>
                                    <span class="text-dark">{{ game.platforms.join(', ') }}</span>
                                </li>
                                <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                    <h6 class="mb-0 font-weight-bold">Public compatibility</h6>
                                    <span v-bind:class="'text-' + getColor(game.compatibilityRank)">{{ game.compatibilityType || 'Nothing' }} - {{ game.compatibilityPercentage + '%' || n/a}}</span>
                                </li>
                                <li v-if="compatibilities.length > 0" class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                    <h6 class="mb-0 font-weight-bold">Official compatibility</h6>
                                    <span v-bind:class="'text-' + getColor(compatibilities[0].compatibilityTypeRank || 0)">{{ compatibilities[0].compatibilityTypeType }}</span>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="row gutters-sm">
                        <div class="col-sm-12 mb-3">
                            <div class="card">
                                <div class="card-body">
                                    <div class="container">
                                        <div class="col-md-12">
                                            <div class="bg-white rounded shadow-sm p-4 mb-4">
                                                <h5 class="mb-0 mb-5">{{ reviews.length }} Review(s)</h5>
                                                <p class="text-black mb-4 mt-2">Average rating: {{ (reviews.reduce((a, b) => a.valueOf() + b.score, 0) / reviews.length || 0).toPrecision(2) }} out of 10</p>
                                                <div v-if="userId">
                                                    <button @click="modal = !modal" type="button" class="btn-outline-primary btn-sm">Post Review</button>
                                                    <progress-create v-show="modal" :set-game-id="game.id" :return-link="'/program/' + game.id"></progress-create>
                                                </div>
                                                <div v-else>
                                                    <router-link to="/login" class="btn">Log in to post review</router-link>
                                                </div>
                                            </div>
                                            <div v-for="review in reviews" v-bind:key="review" class="bg-white rounded shadow-sm p-4 mb-4 restaurant-detailed-ratings-and-reviews">
                                                <div class="reviews-members pt-4 pb-4">
                                                    <div class="media">
                                                        <a href="#"><img width="100" alt="Generic placeholder image" v-bind:src="review.appUserProfile || 'https://t3.ftcdn.net/jpg/03/46/83/96/360_F_346839683_6nAPzbhpSkIpb8pmAwufkC7c5eD7wYws.jpg'" class="mr-3 img-thumbnail rounded-pill"></a>
                                                        <div class="media-body">
                                                            <div class="reviews-members-header text-left ml-3">
                                                                <span class="float-right">
                                                                    <h6 v-bind:class="'text-' + getColor(review.compatibilityTypeRank)" v-if="review.compatibilityTypeType"> {{ review.compatibilityTypeType }}</h6>
                                                                    <h6 class="ml-2"> {{ review.score }}/10</h6>
                                                                </span>
                                                                <h6 class="mb-1 font-weight-bold">{{ review.appUserName }}</h6>
                                                                <h6 class="mb-1">Time played: {{ review.time }} hours</h6>
                                                                <h6 class="mb-1">Config: {{ review.configurationString || ""}}</h6>
                                                                <p class="text-gray">Date: {{ new Date(review.editedAt || review.createdAt).toLocaleDateString() }}</p>
                                                            </div>
                                                            <div v-if="review.review" class="text-justify mx-3">
                                                                <p class="justify-content-center">{{ review.review }}</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-else>
            <div class="spinner-border text-white" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import { BaseService } from '@/services/base-service'
import IGame from '@/domain/IGame'
import IMedia from '@/domain/IMedia'
import ICompatibility from '@/domain/ICompatibility'
import IProgress from '@/domain/IProgress'
import store from '@/store'
import ProgressCreate from '@/views/progress/Create.vue'
@Options({
    components: { ProgressCreate },
    props: {
        id: String,
    }
})
export default class GameView extends Vue {
    id!: string;
    private modal: boolean = false;
    private game!: IGame;
    private review?: IProgress;
    private compatibilities: ICompatibility[] = [];
    private medias: IMedia[] = [];
    private reviews: IProgress[] = [];
    private loading: boolean = true;

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IGame>('v1/games', undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.game = data.data!;
            }
        });
        const mediaService = new BaseService<IMedia>('v1/medias');
        await mediaService.getAll({ gameID: this.game.id }).then((data) => {
            if (data.ok) {
                this.medias = data.data!.filter(m => m.mediaTypeType !== 'Cover Art');
            }
        });
        const compatibilityService = new BaseService<ICompatibility>('v1/compatibilities');
        await compatibilityService.getAll({ gameID: this.game.id }).then((data) => {
            if (data.ok) {
                this.compatibilities = data.data!.sort((a, b) => a.compatibilityTypeRank > b.compatibilityTypeRank ? 1 : -1);
            }
        });
        const progressService = new BaseService<IProgress>('v1/progresses');
        await progressService.getAll({ gameID: this.game.id }).then((data) => {
            if (data.ok) {
                this.reviews = data.data!.sort((a, b) => a.time > b.time ? 1 : -1);
                this.review = this.reviews.find(r => r.appUserId === this.userId);
            }
        });
        this.loading = false;
        const script = document.createElement('script');
        script.setAttribute('id', 'mdbScript');
        script.setAttribute('src', 'https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/3.5.0/mdb.min.js');
        document.head.appendChild(script);
    }

    unmounted(): void {
        const script = document.getElementById('mdbScript');
        if (script) script.remove();
    }

    getColor(rank: number | null): string {
        if (rank === 0 || rank === null) {
            return 'dark'
        } else if (rank === 1) {
            return 'danger'
        } else if (rank === 2) {
            return 'warning'
        } else if (rank === 3) {
            return 'success'
        } else if (rank >= 4) {
            return 'primary'
        } else return "";
    }

    getProgressBarStyle(rating: number): string {
        rating = rating + 1 % 6;
        return "width: " + (20 * rating) + "%";
    }
}
</script>

<style scoped src="../../../node_modules/mdb-ui-kit/css/mdb.min.css">

</style>
