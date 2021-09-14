<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <div>
                <h4>Configuration</h4>
                <hr />
                <dl class="row">
                    <dt class = "col-sm-2">
                        Configuration
                    </dt>
                    <dd class = "col-sm-10">
                        {{configuration.configurationString}}
                    </dd>
                </dl>
                <dl class="row">
                    <dt class = "col-sm-2">
                        Created at
                    </dt>
                    <dd class = "col-sm-10">
                        {{configuration.creationDt}}
                    </dd>
                </dl>
            </div>
        </div>
        <div v-if="!loading">
            <router-link v-if="role === 'Admin'" :to="'/configuration/edit/' + configuration.id">Edit</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link v-if="role === 'Admin'" :to="'/configuration/delete/' + configuration.id">Delete</router-link>
            <span v-if="role === 'Admin'"> | </span>
            <router-link to="/configurations">Back to List</router-link>
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
import IConfiguration from '@/domain/IConfiguration';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class ConfigurationDetails extends Vue {
    id!: string;
    private configuration!: IConfiguration;
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
        const service = new BaseService<IConfiguration>('v1/configurations', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.configuration = data.data!;
            }
        });
        if (this.role !== 'Admin' && this.configuration.appUserId !== this.userId) await this.$router.push('/');
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
