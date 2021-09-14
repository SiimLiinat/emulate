<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <div>
                <h4>Compatibility Type</h4>
                <hr />
                <dl class="row">
                    <dt class = "col-sm-2">
                        Type
                    </dt>
                    <dd class = "col-sm-10">
                        {{compatibilityType.type}}
                    </dd>
                </dl>
                <dl class="row">
                    <dt class = "col-sm-2">
                        Description
                    </dt>
                    <dd class = "col-sm-10">
                        {{compatibilityType.description}}
                    </dd>
                </dl>
                <dl class="row">
                    <dt class = "col-sm-2">
                        Rating
                    </dt>
                    <dd class = "col-sm-10">
                        {{compatibilityType.rating}}
                    </dd>
                </dl>
            </div>
        </div>
        <div v-if="!loading">
            <router-link v-if="role === 'Admin'" :to="'/compatibility-type/edit/' + compatibilityType.id">Edit</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link v-if="role === 'Admin'" :to="'/compatibility-type/delete/' + compatibilityType.id">Delete</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link to="/compatibility-types">Back to List</router-link>
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
import ICompatibilityType from '@/domain/ICompatibilityType'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class CompatibilityTypeDetails extends Vue {
    id!: string;
    private compatibilityType!: ICompatibilityType;
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
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.compatibilityType = data.data!;
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
