<template>

    <body>
    <div v-if="!loading" class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit compatibility type</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="compatibilityType.type" type="text" id="inputType" class="form-control" placeholder="Type" required autofocus>
                                <label for="inputType">Type</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="compatibilityType.description" type="text" id="inputDescription" class="form-control" placeholder="Description">
                                <label for="inputDescription">Description</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="compatibilityType.rating" type="number" id="inputRating" class="form-control" placeholder="Rating">
                                <label for="inputRating">Rating</label>
                            </div>
                            <button @click="editCompatibilityType" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Edit</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/compatibility-types">Back to List</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div v-else>
        <div class="spinner-border text-primary" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
    </body>
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
export default class CompatibilityTypeEdit extends Vue {
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
            } else {
                console.log(data.statusCode)
            }
        });
        this.loading = false;
    }

    async editCompatibilityType(): Promise<void> {
        const service = new BaseService<ICompatibilityType>('v1/compatibilityTypes', this.token || undefined);
        await service.put(this.id, this.compatibilityType).then((data) => {
            if (data.ok) {
                this.$router.push('/compatibility-types')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
