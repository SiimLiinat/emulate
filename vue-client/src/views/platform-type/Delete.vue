<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <div>
                <h4>Platform Type</h4>
                <hr />
                <dl class="row">
                    <dt class = "col-sm-2">
                        Type
                    </dt>
                    <dd class = "col-sm-10">
                        {{platformType.type}}
                    </dd>
                </dl>
                <dl class="row">
                    <dt class = "col-sm-2">
                        Platforms
                    </dt>
                    <dd class = "col-sm-10">
                        {{platformType.platformCount}}
                    </dd>
                </dl>
                <div>
                    <input @click="deletePlatformType" type="submit" value="Delete" class="btn btn-danger" />
                </div>
            </div>
        </div>
        <router-link to="/platform-types">Back to List</router-link>
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
import IPlatformType from '@/domain/IPlatformType';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class PlatformTypeDelete extends Vue {
    id!: string;
    private platformType!: IPlatformType;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IPlatformType>('v1/platformTypes', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.platformType = data.data!;
            }
        });
        this.loading = false;
    }

    async deletePlatformType(): Promise<void> {
        const service = new BaseService<IPlatformType>('v1/platformTypes', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/platform-types')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
