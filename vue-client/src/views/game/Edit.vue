<template>
    <body v-if="!loading">
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Create game</h5>
                        <div class="form-signin">
                            <div class="form-group">
                                <label class="control-label" for="devCompanyId">CompanyId</label>
                                <select required v-model="game.devCompanyId" class="form-control" id="devCompanyId" name="companyId">
                                    <option v-for="company of companies" v-bind:key="company.id" v-bind:value="company.id">
                                        {{ company.name }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label class="control-label" for="pubCompanyId">CompanyId</label>
                                <select required v-model="game.pubCompanyId" class="form-control" id="pubCompanyId" name="companyId">
                                    <option v-for="company of companies" v-bind:key="company.id" v-bind:value="company.id">
                                        {{ company.name }}
                                    </option>
                                </select>
                            </div>
                            <div class="form-label-group">
                                <input v-model="game.name" type="text" id="inputName" class="form-control" placeholder="Name" required>
                                <label for="inputName">Name</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="game.releaseDate" type="text" id="inputReleaseDate" class="form-control" placeholder="Release date" required>
                                <label for="inputReleaseDate">Release date</label>
                            </div>
                            <button @click="editGame" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/games">Back to List</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </body>
    <div v-else>
        <div class="spinner-border text-light" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import store from '@/store';
import { BaseService } from '@/services/base-service';
import ICompany from '@/domain/ICompany';
import IGame from '@/domain/IGame'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class GameEdit extends Vue {
    id!: string;
    private loading: boolean = true;

    game!: IGame;
    companies: ICompany[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IGame>('v1/games', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.game = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        const companyService = new BaseService<ICompany>('v1/companies', this.token || undefined);
        companyService.getAll().then((data) => {
            if (data.ok) {
                this.companies = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async editGame(): Promise<void> {
        const service = new BaseService<IGame>('v1/games', this.token || undefined);
        await service.put(this.id, this.game).then((data) => {
            if (data.statusCode >= 200 && data.statusCode < 300) {
                this.$router.push('/games')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
