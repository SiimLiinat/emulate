<template>
    <body>
    <div v-if="!loading" class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit genre</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="genre.type" type="text" id="inputType" class="form-control" placeholder="Type" required autofocus>
                                <label for="inputType">Type</label>
                            </div>
                            <button @click="editGenre" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Edit</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/genres">Back to List</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div v-else>
        <div class="spinner-border text-primary" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
    </body>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IGenre from '@/domain/IGenre';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class GenreEdit extends Vue {
    id!: string;
    private genre!: IGenre;
    private loading: boolean = true;

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGenre>('v1/genres', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.genre = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        this.loading = false;
    }

    async editGenre(): Promise<void> {
        const service = new BaseService<IGenre>('v1/genres', this.token || undefined);
        await service.put(this.id, this.genre).then((data) => {
            if (data.ok) {
                this.$router.push('/genres')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
