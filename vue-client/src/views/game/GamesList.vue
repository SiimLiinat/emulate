<template>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/material-design-iconic-font/2.2.0/css/material-design-iconic-font.min.css"  integrity="sha256-3sPp8BkKUE7QyPSl6VfBByBroQbKxKG7tsusY2mhbVY=" crossorigin="anonymous" />

    <div class="card">
    <div class="container">
        <div class="row">
            <div class="col-lg-10 mx-auto mb-4">
                <div class="section-title text-center ">
                    <h3 class="top-c-sep mt-5">Compatibility List</h3>
                    <hr class="border-dark">
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-10 mx-auto">
                <div class="career-search mb-60">
                        <div class="row">
                            <div class="col-md-6 col-lg-3 my-3">
                                <div class="input-group position-relative">
                                    <input @change="filterData" v-model="searchWords" type="text" class="form-control" placeholder="Enter Your Keywords"
                                           id="keywords">
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 my-3">
                                <div class="select-container">
                                    <select @change="filterData" v-model="compatibilityType" class="form-control custom-select" id="compatibilityTypeId"
                                            name="compatibilityTypeId">
                                        <option v-bind:value="undefined" selected>Select compatibility</option>
                                        <option v-for="compatibilityType of compatibilityTypes" v-bind:key="compatibilityType.id"
                                                v-bind:value="compatibilityType">
                                            {{ compatibilityType.type }}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 my-3">
                                <div class="select-container">
                                    <select @change="filterData" v-model="platform" class="form-control custom-select" id="platformId" name="compatibilityTypeId">
                                        <option v-bind:value="undefined" selected>Select platform</option>
                                        <option  v-for="platform of platforms" v-bind:key="platform" v-bind:value="platform.code">
                                            {{ platform.name }}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 my-3">
                                <div class="select-container">
                                    <select @change="filterData" v-model="genre" class="form-control custom-select" id="genreId" name="compatibilityTypeId">
                                        <option v-bind:value="undefined" selected>Select genre</option>
                                        <option  v-for="genre of genres" v-bind:key="genre" v-bind:value="genre.type">
                                            {{ genre.type }}
                                        </option>
                                    </select>
                                </div>
                            </div>
                        </div>

                    <div class="filter-result">
                        <p class="mb-30 ff-montserrat text-left">Total Programs : {{ filteredData.length }}</p>

                        <div v-for="game in filteredData" v-bind:key="game" role="button"
                             class="job-box d-md-flex align-items-center justify-content-between mb-3 media position-relative">
                            <div class="job-left d-md-flex align-items-center flex-wrap">
                                <div class="img-holder mr-md-4 mb-md-0 mb-4 mx-auto mx-md-0 d-md-none d-lg-flex">
                                    <img v-if="game.coverArt" class="img-fluid" v-bind:src="game.coverArt">
                                    <span v-else>{{ game.name.toUpperCase().charAt(0) }}</span>
                                </div>
                                <div class="job-content">
                                    <h5 class="text-center text-md-left">{{ game.name }}</h5>
                                    <ul class="d-md-flex flex-wrap text-capitalize ff-open-sans">
                                        <li class="mr-md-4">
                                            <i class="fas fa-building"></i> {{ game.devCompanyName }}
                                        </li>
                                        <li class="mr-md-4">
                                            <i class="fas fa-gamepad"></i> {{ game.platforms.join(", ") || n/a }}
                                        </li>
                                        <li class="mr-md-4 align-items-center">
                                            <i class="fas fa-table"></i> {{ game.genres.join(", ") || n/a}}
                                        </li>
                                        <span v-bind:class="getBadge(game.compatibilityRank)" class="badge">{{ game.compatibilityType || 'n/a' }}</span>
                                    </ul>
                                </div>
                            </div>
                            <router-link :to="'/program/' + game.id" class="stretched-link"></router-link>
                        </div>
                    </div>
                    <div v-if="loading">
                        <div class="spinner-border text-primary" role="status">
                            <span class="sr-only">Loading...</span>
                        </div>
                    </div>
                </div>

                <!-- START Pagination -->
<!--                <nav aria-label="Page navigation">-->
<!--                    <ul class="pagination pagination-reset justify-content-center">-->
<!--                        <li class="page-item disabled">-->
<!--                            <a class="page-link" href="#" tabindex="-1" aria-disabled="true">-->
<!--                                <i class="zmdi zmdi-long-arrow-left"></i>-->
<!--                            </a>-->
<!--                        </li>-->
<!--                        <li class="page-item"><a class="page-link" href="#">1</a></li>-->
<!--                        <li class="page-item d-none d-md-inline-block"><a class="page-link" href="#">2</a></li>-->
<!--                        <li class="page-item d-none d-md-inline-block"><a class="page-link" href="#">3</a></li>-->
<!--                        <li class="page-item"><a class="page-link" href="#">...</a></li>-->
<!--                        <li class="page-item"><a class="page-link" href="#">8</a></li>-->
<!--                        <li class="page-item">-->
<!--                            <a class="page-link" href="#">-->
<!--                                <i class="zmdi zmdi-long-arrow-right"></i>-->
<!--                            </a>-->
<!--                        </li>-->
<!--                    </ul>-->
<!--                </nav>-->
            </div>
        </div>
    </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component'
import IGame from '@/domain/IGame'
import store from '@/store'
import { BaseService } from '@/services/base-service'
import ICompatibilityType from '@/domain/ICompatibilityType'
import IPlatform from '@/domain/IPlatform'
import IGenre from '@/domain/IGenre'

export default class GamesList extends Vue {
    private games: IGame[] = [];
    private filteredData: IGame[] = [];
    private loading: boolean = true;

    private compatibilityTypes: ICompatibilityType[] = [];
    private genres: IGenre[] = [];
    private platforms: IPlatform[] = [];

    private searchWords: string | undefined;
    private compatibilityType: ICompatibilityType | undefined = undefined;
    private platform: string | undefined = undefined;
    private genre: string | undefined = undefined;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    getBadge(rank: number | null): string {
        if (rank === 0 || rank === null) {
            return 'badge-dark'
        } else if (rank === 1) {
            return 'badge-danger'
        } else if (rank === 2) {
            return 'badge-warning'
        } else if (rank === 3) {
            return 'badge-success'
        } else if (rank >= 4) {
            return 'badge-primary'
        } else return "";
    }

    filterData(): void {
        this.filteredData = this.games;
        if (this.searchWords !== undefined && this.searchWords !== "") {
            const searchWords = this.searchWords.toUpperCase().split(" ");
            this.filteredData = this.filteredData.filter(g => this.ContainsAny(g.name.toUpperCase(), searchWords));
        }
        if (this.genre !== undefined) this.filteredData = this.filteredData.filter(g => g.genres.includes(this.genre!));
        if (this.compatibilityType !== undefined) this.filteredData = this.filteredData.filter(g => g.compatibilityRank === this.compatibilityType?.rating);
        if (this.platform !== undefined) this.filteredData = this.filteredData.filter(g => g.platforms.includes(this.platform!));
        console.log(this.compatibilityType)
    }

    ContainsAny(str: string, items: { [x: string]: any }): boolean {
        for (const i in items) {
            const item = items[i];
            if (str.indexOf(item) > -1) {
                return true;
            }
        }
        return false;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IGame>('v1/games', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.games = data.data!.sort((a, b) => a.name.localeCompare(b.name));
                this.filteredData = data.data!;
            }
        });
        const compTypeService = new BaseService<ICompatibilityType>('v1/compatibilityTypes', this.token || undefined);
        await compTypeService.getAll().then((data) => {
            if (data.ok) {
                this.compatibilityTypes = data.data!;
            }
        });
        const platformService = new BaseService<IPlatform>('v1/platforms', this.token || undefined);
        await platformService.getAll().then((data) => {
            if (data.ok) {
                this.platforms = data.data!;
            }
        });
        const genreService = new BaseService<IGenre>('v1/genres', this.token || undefined);
        await genreService.getAll().then((data) => {
            if (data.ok) {
                this.genres = data.data!;
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

.form-control {
    height: calc(2.2em + .75em + 2px);
}

option {
    color: black;
}

.media:hover {
    background-color: #2DA5FC !important;
    color: white;
}

body{
    background:#f5f5f5;
    margin-top:20px;}

/* ===== Career ===== */
.career-form {
    background-color: #2DA5FC;
    border-radius: 5px;
    padding: 0 16px;
}

.career-form .form-control {
    background-color: rgba(255, 255, 255, 0.2);
    border: 0;
    padding: 12px 15px;
    color: #fff;
}

.career-form .form-control::-webkit-input-placeholder {
    /* Chrome/Opera/Safari */
    color: #fff;
}

.career-form .form-control::-moz-placeholder {
    /* Firefox 19+ */
    color: #fff;
}

.career-form .form-control:-ms-input-placeholder {
    /* IE 10+ */
    color: #fff;
}

.career-form .form-control:-moz-placeholder {
    /* Firefox 18- */
    color: #fff;
}

.career-form .custom-select {
    background-color: rgba(255, 255, 255, 0.2);
    border: 0;
    padding: 12px 15px;
    color: #fff;
    width: 100%;
    border-radius: 5px;
    text-align: left;
    height: auto;
    background-image: none;
}

.career-form .custom-select:focus {
    -webkit-box-shadow: none;
    box-shadow: none;
}

.career-form .select-container {
    position: relative;
}

.career-form .select-container:before {
    position: absolute;
    right: 15px;
    top: calc(50% - 14px);
    font-size: 18px;
    color: #ffffff;
    content: '\F2F9';
    font-family: "Material-Design-Iconic-Font";
}

.filter-result .job-box {
    -webkit-box-shadow: 0 0 35px 0 rgba(130, 130, 130, 0.2);
    box-shadow: 0 0 35px 0 rgba(130, 130, 130, 0.2);
    border-radius: 10px;
    padding: 10px 35px;
}

ul {
    list-style: none;
}

.list-disk li {
    list-style: none;
    margin-bottom: 12px;
}

.list-disk li:last-child {
    margin-bottom: 0;
}

.job-box .img-holder {
    height: 65px;
    width: 65px;
    background-color: #4e63d7;
    background-image: -webkit-gradient(linear, left top, right top, from(rgba(78, 99, 215, 0.9)), to(#5a85dd));
    background-image: linear-gradient(to right, rgba(78, 99, 215, 0.9) 0%, #5a85dd 100%);
    font-family: "Open Sans", sans-serif;
    color: #fff;
    font-size: 22px;
    font-weight: 700;
    display: -webkit-box;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-pack: center;
    -ms-flex-pack: center;
    justify-content: center;
    -webkit-box-align: center;
    -ms-flex-align: center;
    align-items: center;
    border-radius: 65px;
}

.career-title {
    background-color: #4e63d7;
    color: #fff;
    padding: 15px;
    text-align: center;
    border-radius: 10px 10px 0 0;
    background-image: -webkit-gradient(linear, left top, right top, from(rgba(78, 99, 215, 0.9)), to(#2DA5FC));
    background-image: linear-gradient(to right, rgba(78, 99, 215, 0.9) 0%, #5a85dd 100%);
}

.job-overview {
    -webkit-box-shadow: 0 0 35px 0 rgba(130, 130, 130, 0.2);
    box-shadow: 0 0 35px 0 rgba(130, 130, 130, 0.2);
    border-radius: 10px;
}

@media (min-width: 992px) {
    .job-overview {
        position: -webkit-sticky;
        position: sticky;
        top: 70px;
    }
}

.job-overview .job-detail ul {
    margin-bottom: 28px;
}

.job-overview .job-detail ul li {
    opacity: 0.75;
    font-weight: 600;
    margin-bottom: 15px;
}

.job-overview .job-detail ul li i {
    font-size: 20px;
    position: relative;
    top: 1px;
}

.job-overview .overview-bottom,
.job-overview .overview-top {
    padding: 35px;
}

.job-content ul li {
    font-weight: 600;
    opacity: 0.75;
    border-bottom: 1px solid #ccc;
    padding: 10px 5px;
}

@media (min-width: 768px) {
    .job-content ul li {
        border-bottom: 0;
        padding: 0;
    }
}

.job-content ul li i {
    font-size: 20px;
    position: relative;
    top: 1px;
}
.mb-30 {
    margin-bottom: 30px;
}
</style>
