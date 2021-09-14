<template>
    <div class="card card-body mt-5">
        <h1>Media Types</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/media-type/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Type
                    </th>
                    <th>
                        Description
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="mediaType of mediaTypes" v-bind:key="mediaType">
                    <td>
                        {{mediaType.type}}
                    </td>
                    <td>
                        {{mediaType.description}}
                    </td>
                    <td>
                        <router-link :to="'/media-type/edit/' + mediaType.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/media-type/details/' + mediaType.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/media-type/delete/' + mediaType.id">Delete</router-link>
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
import IMediaType from '@/domain/IMediaType'

export default class MediaTypeIndex extends Vue {
    private mediaTypes: IMediaType[] = [];
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
        await service.getAll().then((data) => {
            if (data.ok) {
                this.mediaTypes = data.data!;
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
