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
                <div>
                    <input @click="deleteCompatibilityType" type="submit" value="Delete" class="btn btn-danger" />
                </div>
            </div>
        </div>
        <router-link to="/compatibility-types">Back to List</router-link>
        <div v-if="loading">
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
export default class CompatibilityTypeDelete extends Vue {
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

    async deleteCompatibilityType(): Promise<void> {
        const service = new BaseService<ICompatibilityType>('v1/compatibilityTypes', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/compatibility-types')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
