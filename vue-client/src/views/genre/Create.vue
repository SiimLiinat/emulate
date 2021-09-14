<template>
    <body>
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Create genre</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="type" type="text" id="inputType" class="form-control" placeholder="Type" required autofocus>
                                <label for="inputType">Type</label>
                            </div>
                            <button @click="createGenre" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/genres">Back to List</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </body>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IGenreAdd from '@/domain/IGenreAdd';

export default class GenreCreate extends Vue {
    type: string = "";

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    mounted(): void {
        if (this.role !== 'Admin') this.$router.push('/');
    }

    async createGenre(): Promise<void> {
        const genre: IGenreAdd = { type: this.type };
        const service = new BaseService<IGenreAdd>('v1/genres', this.token || undefined);
        await service.post(genre).then((data) => {
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
