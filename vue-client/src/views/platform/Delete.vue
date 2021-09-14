<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Platform</h4>
            <hr />

            <dl class="row">
                <dt class = "col-sm-2">
                    Company
                </dt>
                <dd class = "col-sm-10">
                    {{platform.companyName}}
                </dd>
                <dt class = "col-sm-2">
                    PlatformType
                </dt>
                <dd class = "col-sm-10">
                    {{platform.platformTypeType}}
                </dd>
                <dt class = "col-sm-2">
                    Name
                </dt>
                <dd class = "col-sm-10">
                    {{platform.name}}
                </dd>
                <dt class = "col-sm-2">
                    Code
                </dt>
                <dd class = "col-sm-10">
                    {{platform.code}}
                </dd>
            </dl>
            <div>
                <input @click="deletePlatform" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/platform/edit/' + platform.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/platform/details/' + platform.id">Details</router-link>
            <span> | </span>
            <router-link to="/platforms">Back to List</router-link>
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
import IPlatform from '@/domain/IPlatform';
import store from '@/store';
import { BaseService } from '@/services/base-service';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class PlatformDelete extends Vue {
    id!: string;
    private platform!: IPlatform;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IPlatform>('v1/platforms', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.platform = data.data!;
            }
        });
        this.loading = false;
    }

    async deletePlatform(): Promise<void> {
        const service = new BaseService<IPlatform>('v1/platforms', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/platforms')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
