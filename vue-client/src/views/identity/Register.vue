<template>
    <body>
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Register</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="username" type="text" id="inputUsername" class="form-control" placeholder="Username" required autofocus>
                                <label for="inputUsername">Username</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="email" type="email" id="inputEmail" class="form-control" placeholder="Email address" required>
                                <label for="inputUsername">Email</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="password" type="password" id="inputPassword" class="form-control" placeholder="Password" required>
                                <label for="inputPassword">Password</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="confirmPassword" type="password" id="inputPasswordRepeat" class="form-control" placeholder="Password confirmation" required>
                                <label for="inputPasswordRepeat">Password confirmation</label>
                            </div>
                            <button :disabled="password.length < 6 || password !== confirmPassword" @click="registerSubmit()"
                                    class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Sign up</button>
                            <div v-if="errors" class="text-center">
                                <br>
                                <span class="alert-danger alert text-right">{{ errors }}</span>
                                <br>
                            </div>
                            <hr class="my-4">
                            <button class="btn btn-lg btn-google btn-block text-uppercase" type="submit"><i class="fab fa-google mr-2"></i> Sign up with Google</button>
                            <button class="btn btn-lg btn-facebook btn-block text-uppercase" type="submit"><i class="fab fa-facebook-f mr-2"></i> Sign up with Facebook</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </body>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import store from '@/store'

@Options({
    components: {},
    props: {}
})

export default class Register extends Vue {
    username: string = "";
    email: string = "";
    password: string = "";
    confirmPassword: string = "";

    errors: string | null = null;

    get id(): string | undefined {
        return store.state.id;
    }

    mounted(): void {
        if (this.id) this.$router.push('/');
    }

    async registerSubmit(): Promise<void> {
        await store.dispatch('register', {
            username: this.username,
            email: this.email,
            password: this.password,
            confirmPassword: this.confirmPassword
        });
        if (store.state.token) await this.$router.push('/');
        else this.errors = "Something went wrong. Please try again.";
    }
}
</script>

<style scoped src="./identity.css">
</style>
