<template>
    <div class="card card-body mt-5">
        <h1>Compatibility Types</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/compatibility-type/create">Create New</router-link>
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
                    <th>
                        Rating
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="compatibilityType of compatibilityTypes" v-bind:key="compatibilityType">
                    <td>
                        {{compatibilityType.type}}
                    </td>
                    <td>
                        {{compatibilityType.description}}
                    </td>
                    <td>
                        {{compatibilityType.rating}}
                    </td>
                    <td>
                        <router-link :to="'/compatibility-type/edit/' + compatibilityType.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/compatibility-type/details/' + compatibilityType.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/compatibility-type/delete/' + compatibilityType.id">Delete</router-link>
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
import ICompatibilityType from '@/domain/ICompatibilityType'

export default class CompatibilityTypeIndex extends Vue {
    private compatibilityTypes: ICompatibilityType[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<ICompatibilityType>('v1/compatibilityTypes', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.compatibilityTypes = data.data!;
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
