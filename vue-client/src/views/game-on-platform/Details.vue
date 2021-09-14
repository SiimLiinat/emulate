<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Game on platform</h4>
            <hr />

            <dl class="row">
                <dt class = "col-sm-2">
                    Game
                </dt>
                <dd class = "col-sm-10">
                    {{gameOnPlatform.gameName}}
                </dd>
                <dt class = "col-sm-2">
                    Platform
                </dt>
                <dd class = "col-sm-10">
                    {{gameOnPlatform.platformName}}
                </dd>
                <dt class = "col-sm-2">
                    Release date
                </dt>
                <dd class = "col-sm-10">
                    {{gameOnPlatform.releaseDate}}
                </dd>
            </dl>
        </div>
        <div v-if="!loading">
            <router-link :to="'/game-on-platform/edit/' + gameOnPlatform.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/game-on-platform/delete/' + gameOnPlatform.id">Delete</router-link>
            <span> | </span>
            <router-link to="/game-on-platforms">Back to List</router-link>
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
import IGameOnPlatform from '@/domain/IGameOnPlatform'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class GameOnPlatformDetails extends Vue {
    id!: string;
    private gameOnPlatform!: IGameOnPlatform;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGameOnPlatform>('v1/gameOnPlatforms', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.gameOnPlatform = data.data!;
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
