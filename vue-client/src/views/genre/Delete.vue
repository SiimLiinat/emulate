<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <div>
                <h4>Genre</h4>
                <hr />
                <dl class="row">
                    <dt class = "col-sm-2">
                        Type
                    </dt>
                    <dd class = "col-sm-10">
                        {{genre.type}}
                    </dd>
                </dl>
                <div>
                    <input @click="deleteGenre" type="submit" value="Delete" class="btn btn-danger" />
                </div>
            </div>
        </div>
        <router-link to="/genres">Back to List</router-link>
        <div v-if="loading">
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
import IGenre from '@/domain/IGenre'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class GenreDelete extends Vue {
    id!: string;
    private genre!: IGenre;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGenre>('v1/genres', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.genre = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteGenre(): Promise<void> {
        const service = new BaseService<IGenre>('v1/genres', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/genres')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
