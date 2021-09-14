<template>
    <div v-if="!loading" class="container">
        <div class="main-body">
            <div class="row gutters-sm">
                <div class="col-md-4 mb-3">
                    <div class="card">
                        <div class="card-body">
                            <div v-once class="d-flex flex-column align-items-center text-center">
                                <img v-bind:src="user.profilePicture !== null ? user.profilePicture
                                : require('../../../public/default_profile_pic.webp')" alt="" class="rounded-circle" width="150">
                                <div class="mt-3">
                                    <h4>{{ user.userName }}</h4>
                                    <button class="btn btn-outline-primary">Message</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card mt-3">
                        <ul class="list-group list-group-flush">
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
                            <hr>
                            <div v-if="id === userId" class="row">
                                <div class="col-sm-6 mb-3">
                                    <router-link class="btn btn-primary text-white" to="/media/create">Add image</router-link>
                                </div>
                                <div class="col-sm-6 mb-3">
                                    <router-link class="btn btn-secondary text-white" to="/medias">See images</router-link>
                                </div>
                            </div>

                        </ul>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="card mb-3">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-3">
                                    <h6 class="mb-0">User Name</h6>
                                </div>
                                <div class="col-sm-3 text-primary float-left">
                                    {{ user.userName }}
                                </div>
                            </div>
                            <hr>
                            <div v-if="userId === id" class="row mb-3">
                                <div class="col-sm-3">
                                    <h6 class="mb-0 align-items-center my-2">Email</h6>
                                </div>
                                <div class="col-sm-9 text-primary">
                                    <input v-on:change="emailChange = true" v-model="user.email" type="text" class="form-control">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <h6 class="mb-0 align-items-center my-2">Profile Picture</h6>
                                </div>
                                <div class="col-sm-9 text-primary">
                                    <input :disabled="userId !== id"  v-on:change="profilePictureChange = true" v-model="user.profilePicture" type="text" class="form-control">
                                </div>
                            </div>
                            <hr>
                            <button v-if="userId === id" @click="editProfile" :disabled="!emailChange && !profilePictureChange"
                                    class="btn btn-primary">Save changes</button>
                        </div>
                    </div>

                    <div class="row gutters-sm">
                        <div class="col-sm-6 mb-3">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="align-items-center mb-3">Configs</h4>
                                    <hr>
                                    <div v-if="configs.length === 0"><h2>Empty</h2><hr></div>
                                    <div v-for="configuration of configs" v-bind:key="configuration">
                                        <h6>{{ configuration.configurationString }}</h6>
                                        <hr>
                                    </div>
                                    <div v-if="userId === id">
                                        <div class="btn btn-primary mr-2">
                                            <router-link class="text-white" to="/configuration/create">Add config</router-link>
                                        </div>
                                        <div class="btn btn-secondary">
                                            <router-link class="text-white" to="/configurations">See configs</router-link>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 mb-3">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="align-items-center mb-3">Reviews</h4>
                                    <hr>
                                    <div v-if="progresses.length === 0"><h2>Empty</h2></div>
                                    <div v-for="progress of progresses" v-bind:key="progress">
                                        <h6>{{ progress.gameName }}
                                            <span v-if="progress.score"> - {{ progress.score}}</span>
                                        </h6>
                                        <hr>
                                    </div>
                                    <div v-if="userId === id" class="btn btn-primary">
                                        <router-link class="text-white" to="/progresses">See reviews</router-link>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import { BaseService } from '@/services/base-service'
import IAppUser from '@/domain/IAppUser'
import IConfiguration from '@/domain/IConfiguration'
import IProgress from '@/domain/IProgress'
import store from '@/store'
import IMedia from '@/domain/IMedia'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class Overview extends Vue {
    id!: string;
    private loading: boolean = true;
    private user!: IAppUser;
    private emailChange: boolean = false;
    private profilePictureChange: boolean = false;

    configs: IConfiguration[] = [];
    medias: IMedia[] = [];
    progresses: IProgress[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        const userService = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await userService.get(this.id!).then((data) => {
            if (data.ok) {
                this.user = data.data!;
            } else {
                this.$router.push({ name: 'Login' })
            }
        });
        const configService = new BaseService<IConfiguration>('v1/configurations', this.token || undefined);
        await configService.getAll({ userId: this.id }).then((data) => {
            if (data.ok) {
                this.configs = data.data!.slice(0, 5);
            }
        });
        const mediaService = new BaseService<IMedia>('v1/medias', this.token || undefined);
        await mediaService.getAll({ userId: this.id }).then((data) => {
            if (data.ok) {
                this.medias = data.data!.slice(0, 5);
            }
        });
        const progressService = new BaseService<IProgress>('v1/progresses', this.token || undefined);
        await progressService.getAll({ userId: this.id }).then((data) => {
            if (data.ok) {
                this.progresses = data.data!.slice(0, 5);
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

    async editProfile(): Promise<void> {
        const service = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await service.put(this.id!, this.user).then((data) => {
            if (data.ok) {
                this.$forceUpdate();
                this.$router.push('/profile/' + this.id);
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../../../node_modules/mdb-ui-kit/css/mdb.min.css">

body{
    margin-top:20px;
    color: #1a202c;
    text-align: left;
    background-color: #e2e8f0;
}
.main-body {
    padding: 15px;
}
.card {
    box-shadow: 0 1px 3px 0 rgba(0,0,0,.1), 0 1px 2px 0 rgba(0,0,0,.06);
}

.card {
    position: relative;
    display: flex;
    flex-direction: column;
    min-width: 0;
    word-wrap: break-word;
    background-color: #fff;
    background-clip: border-box;
    border: 0 solid rgba(0,0,0,.125);
    border-radius: .25rem;
}

.card-body {
    flex: 1 1 auto;
    min-height: 1px;
    padding: 1rem;
}

.gutters-sm {
    margin-right: -8px;
    margin-left: -8px;
}

.gutters-sm, .gutters-sm {
    padding-right: 8px;
    padding-left: 8px;
}
.mb-3 {
    margin-bottom: 1rem!important;
}

</style>
