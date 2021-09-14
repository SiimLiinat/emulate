<template>
    <div class="card card-body mt-5">
        <h1>Media</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/media/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Media type
                    </th>
                    <th>
                        Game
                    </th>
                    <th>
                        User
                    </th>
                    <th>
                        Url
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="media of medias" v-bind:key="media">
                    <td>
                        {{media.mediaTypeType}}
                    </td>
                    <td>
                        {{media.gameId}}
                    </td>
                    <td>
                        {{media.appUserId}}
                    </td>
                    <td>
                        {{media.url}}
                    </td>
                    <td>
                        <router-link :to="'/media/edit/' + media.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/media/details/' + media.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/media/delete/' + media.id">Delete</router-link>
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
import IMedia from '@/domain/IMedia'

export default class MediaIndex extends Vue {
    private medias: IMedia[] = [];
    private loading: boolean = true;

    get id(): string | undefined {
        return store.state.id;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IMedia>('v1/medias', this.token || undefined);
        await service.getAll({ userId: this.role !== 'Admin' ? store.state.id : null }).then((data) => {
            if (data.ok) {
                this.medias = data.data!;
            } else {
                console.log(data)
            }
        });
        if (this.id === undefined) await this.$router.push('/');
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
