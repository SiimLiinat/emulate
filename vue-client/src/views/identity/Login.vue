<template>
    <body>
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Sign In</h5>
                            <div class="form-signin">
                                <div class="form-label-group">
                                    <input v-model="username" type="text" id="inputUsername" class="form-control" placeholder="Username" required autofocus>
                                    <label for="inputUsername">Username</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="password" type="password" id="inputPassword" class="form-control" placeholder="Password" required>
                                    <label for="inputPassword">Password</label>
                                </div>

                                <div class="custom-control custom-checkbox mb-3">
                                    <input type="checkbox" class="custom-control-input" id="customCheck1">
                                    <label class="custom-control-label" for="customCheck1">Remember password</label>
                                </div>
                                <button @click="loginSubmit" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Sign in</button>
                                <div v-if="errors" class="text-center">
                                    <br>
                                    <span class="alert-danger alert text-right">{{ errors }}</span>
                                    <br>
                                </div>
                                <hr class="my-4">
                                <button class="btn btn-lg btn-google btn-block text-uppercase" type="submit"><i class="fab fa-google mr-2"></i> Sign in with Google</button>
                                <button class="btn btn-lg btn-facebook btn-block text-uppercase" type="submit"><i class="fab fa-facebook-f mr-2"></i> Sign in with Facebook</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import store from '@/store/index'

@Options({
    components: {},
    props: {}
})

export default class Login extends Vue {
    username: string = "";
    password: string = "";

    errors: string | null = null;

    get id(): string | undefined {
        return store.state.id;
    }

    mounted(): void {
        if (this.id) this.$router.push('/');
    }

    async loginSubmit (): Promise<void> {
        await store.dispatch('logIn', {
            username: this.username,
            password: this.password
        })
        if (store.state.token) await this.$router.push('/');
        else this.errors = "Something went wrong. Please try again.";
    }
}
</script>

<style scoped src="./identity.css">
</style>
