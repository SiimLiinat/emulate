<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <div>
                <h4>Media Type</h4>
                <hr />
                <dl class="row">
                    <dt class = "col-sm-2">
                        Type
                    </dt>
                    <dd class = "col-sm-10">
                        {{mediaType.type}}
                    </dd>
                </dl>
                <dl class="row">
                    <dt class = "col-sm-2">
                        Description
                    </dt>
                    <dd class = "col-sm-10">
                        {{mediaType.description}}
                    </dd>
                </dl>
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/media-type/edit/' + mediaType.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/media-type/delete/' + mediaType.id">Delete</router-link>
            <span> | </span>
            <router-link to="/media-types">Back to List</router-link>
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
import IMediaType from '@/domain/IMediaType'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class MediaTypeDetails extends Vue {
    id!: string;
    private mediaType!: IMediaType;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IMediaType>('v1/mediaTypes', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.mediaType = data.data!;
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
