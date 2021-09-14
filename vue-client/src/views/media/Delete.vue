<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Media</h4>
            <hr />

            <dl class="row">
                <dt class = "col-sm-2">
                    Media type
                </dt>
                <dd class = "col-sm-10">
                    {{media.mediaTypeType}}
                </dd>
                <dt class = "col-sm-2">
                    Game
                </dt>
                <dd class = "col-sm-10">
                    {{media.gameId}}
                </dd>
                <dt class = "col-sm-2">
                    User
                </dt>
                <dd class = "col-sm-10">
                    {{media.appUserId}}
                </dd>
                <dt class = "col-sm-2">
                    Url
                </dt>
                <dd class = "col-sm-10">
                    {{media.url}}
                </dd>
            </dl>
            <div>
                <input @click="deleteMedia" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link v-if="role === 'Admin'" :to="'/media/edit/' + media.id">Edit</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link v-if="role === 'Admin'" :to="'/media/details/' + media.id">Details</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link to="/medias">Back to List</router-link>
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
import IMedia from '@/domain/IMedia';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class MediaDelete extends Vue {
    id!: string;
    private media!: IMedia;
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
        const service = new BaseService<IMedia>('v1/medias', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.media = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteMedia(): Promise<void> {
        const service = new BaseService<IMedia>('v1/medias', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/medias')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
