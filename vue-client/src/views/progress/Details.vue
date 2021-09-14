<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Review</h4>
            <hr />

            <dl class="row">
                <dt class = "col-sm-2">
                    User
                </dt>
                <dd class = "col-sm-10">
                    {{progress.appUserName}}
                </dd>
                <dt class = "col-sm-2">
                    Game
                </dt>
                <dd class = "col-sm-10">
                    {{progress.gameName}}
                </dd>
                <dt class = "col-sm-2">
                    Compatibility
                </dt>
                <dd class = "col-sm-10">
                    {{progress.compatibilityTypeType}}
                </dd>
                <dt class = "col-sm-2">
                    Time (hours)
                </dt>
                <dd class = "col-sm-10">
                    {{progress.time}}
                </dd>
                <dt class = "col-sm-2">
                    Score
                </dt>
                <dd class = "col-sm-10">
                    {{progress.score}}
                </dd>
                <dt class = "col-sm-2">
                    Review
                </dt>
                <dd class = "col-sm-10">
                    {{progress.review}}
                </dd>
                <dt class = "col-sm-2">
                    Created at
                </dt>
                <dd class = "col-sm-10">
                    {{progress.createdAt}}
                </dd>
            </dl>
        </div>
        <div v-if="!loading">
            <router-link v-if="role === 'Admin'" :to="'/progress/edit/' + progress.id">Edit</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link v-if="role === 'Admin'" :to="'/progress/delete/' + progress.id">Delete</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link to="/progresses">Back to List</router-link>
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
import IProgress from '@/domain/IProgress'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class ProgressDetails extends Vue {
    id!: string;
    private progress!: IProgress;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        if (this.userId === undefined) await this.$router.push('/');
        const service = new BaseService<IProgress>('v1/progresses', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.progress = data.data!;
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
