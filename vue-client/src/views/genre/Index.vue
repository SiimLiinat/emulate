<template>
    <div class="card card-body mt-5">
        <h1>Genres</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/genre/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Type
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="genre of genres" v-bind:key="genre">
                    <td>
                        {{genre.type}}
                    </td>
                    <td>
                        <router-link :to="'/genre/edit/' + genre.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/genre/details/' + genre.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/genre/delete/' + genre.id">Delete</router-link>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
        <div v-else>
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IGenre from '@/domain/IGenre';

export default class GenreIndex extends Vue {
    private genres: IGenre[] = [];
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
        await service.getAll().then((data) => {
            if (data.ok) {
                this.genres = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
