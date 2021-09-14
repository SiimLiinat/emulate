<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Game</h4>
            <hr />
            <dl class="row">
                <dt class = "col-sm-2">
                    Name
                </dt>
                <dd class = "col-sm-10">
                    {{game.name}}
                </dd>
                <dt class = "col-sm-2">
                    Developer
                </dt>
                <dd class = "col-sm-10">
                    {{game.devCompanyName}}
                </dd>
                <dt class = "col-sm-2">
                    Publisher
                </dt>
                <dd class = "col-sm-10">
                    {{game.pubCompanyName}}
                </dd>
                <dt class = "col-sm-2">
                    Release Date
                </dt>
                <dd class = "col-sm-10">
                    {{game.releaseDate}}
                </dd>
                <dt class = "col-sm-2">
                    Compatibility
                </dt>
                <dd class = "col-sm-10">
                    {{game.compatibilityType}}
                </dd>
                <dt class = "col-sm-2">
                    Compatibility Percentage
                </dt>
                <dd class = "col-sm-10">
                    {{game.compatibilityPercentage}}
                </dd>
            </dl>
            <div>
                <input @click="deleteGame" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/game/edit/' + game.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/game/details/' + game.id">Details</router-link>
            <span> | </span>
            <router-link to="/games">Back to List</router-link>
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
import IGame from '@/domain/IGame'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class GameDelete extends Vue {
    id!: string;
    private game!: IGame;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGame>('v1/games', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.game = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteGame(): Promise<void> {
        const service = new BaseService<IGame>('v1/games', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/games')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
