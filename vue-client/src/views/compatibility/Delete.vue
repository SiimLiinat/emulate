<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Compatibility</h4>
            <hr />

            <dl class="row">
                <dt class = "col-sm-2">
                    Compatibility
                </dt>
                <dd class = "col-sm-10">
                    {{compatibility.compatibilityTypeType}}
                </dd>
                <dt class = "col-sm-2">
                    Emulator
                </dt>
                <dd class = "col-sm-10">
                    {{compatibility.emulatorName}}
                </dd>
                <dt class = "col-sm-2">
                    Game
                </dt>
                <dd class = "col-sm-10">
                    {{compatibility.gameName}}
                </dd>
                <dt class = "col-sm-2">
                    Date
                </dt>
                <dd class = "col-sm-10">
                    {{compatibility.date}}
                </dd>
            </dl>
            <div>
                <input @click="deleteCompatibility" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/compatibility/edit/' + compatibility.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/compatibility/details/' + compatibility.id">Details</router-link>
            <span> | </span>
            <router-link to="/compatibilities">Back to List</router-link>
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
import ICompatibility from '@/domain/ICompatibility'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class CompatibilityDelete extends Vue {
    id!: string;
    private compatibility!: ICompatibility;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<ICompatibility>('v1/compatibilities', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.compatibility = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteCompatibility(): Promise<void> {
        const service = new BaseService<ICompatibility>('v1/compatibilities', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/compatibilities')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
