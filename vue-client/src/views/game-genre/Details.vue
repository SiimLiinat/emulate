<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Game Genre</h4>
            <hr />

            <dl class="row">
                <dt class = "col-sm-2">
                    Game
                </dt>
                <dd class = "col-sm-10">
                    {{gameGenre.gameName}}
                </dd>
                <dt class = "col-sm-2">
                    Genre
                </dt>
                <dd class = "col-sm-10">
                    {{gameGenre.genreType}}
                </dd>
            </dl>
        </div>
        <div v-if="!loading">
            <router-link :to="'/game-genre/edit/' + gameGenre.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/game-genre/delete/' + gameGenre.id">Delete</router-link>
            <span> | </span>
            <router-link to="/game-genres">Back to List</router-link>
        </div>
        <div v-else>
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IGameGenre from '@/domain/IGameGenre'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class GameGenreDetails extends Vue {
    id!: string;
    private gameGenre!: IGameGenre;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGameGenre>('v1/gameGenres', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.gameGenre = data.data!;
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
