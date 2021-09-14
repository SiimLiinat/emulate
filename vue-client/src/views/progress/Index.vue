<template>
    <div class="card card-body mt-5">
        <h1>Reviews</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/progress/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        User
                    </th>
                    <th>
                        Game
                    </th>
                    <th>
                        Review
                    </th>
                    <th>
                        Time (h)
                    </th>
                    <th>
                        Score
                    </th>
                    <th>
                        Comp type
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="progress of progresses" v-bind:key="progress">
                    <td>
                        {{progress.appUserName}}
                    </td>
                    <td>
                        {{progress.gameName}}
                    </td>
                    <td>
                        {{progress.review}}
                    </td>
                    <td>
                        {{progress.time}}
                    </td>
                    <td>
                        {{progress.score}}
                    </td>
                    <td>
                        {{progress.compatibilityTypeType}}
                    </td>
                    <td>
                        <router-link v-if="id" :to="'/progress/edit/' + progress.id">Edit</router-link>
                        <span v-if="id"> | </span>
                        <router-link :to="'/progress/details/' + progress.id">Details</router-link>
                        <span v-if="id"> | </span>
                        <router-link v-if="id" :to="'/progress/delete/' + progress.id">Delete</router-link>
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
import IProgress from '@/domain/IProgress'

export default class ProgressIndex extends Vue {
    private progresses: IProgress[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IProgress>('v1/progresses', this.token || undefined);
        await service.getAll({ userId: this.role !== 'Admin' ? this.id : null }).then((data) => {
            if (data.ok) {
                this.progresses = data.data!;
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
