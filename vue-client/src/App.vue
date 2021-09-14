<template>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container">
                <router-link class="navbar-brand" to="/">emulate</router-link>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <router-link class="nav-link text-dark" to="/games/list">Compatibility</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link text-dark" to="/emulators/list">Emulators</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link text-dark" to="/about">About</router-link>
                        </li>
                        <li v-if="role === 'Admin'" class="nav-item dropdown">
                            <a class="nav-link text-dark dropdown-toggle" href="#" id="navbarDropdown" role="button"
                               data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                Admin
                            </a>
                            <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <router-link class="nav-link text-dark" to="/companies">Companies</router-link>
                                <router-link class="nav-link text-dark" to="/compatibilities">Compatibilities</router-link>
                                <router-link class="nav-link text-dark" to="/compatibility-types">Compatibility Types</router-link>
                                <router-link class="nav-link text-dark" to="/configurations">Configurations</router-link>
                                <router-link class="nav-link text-dark" to="/emulators">Emulators</router-link>
                                <router-link class="nav-link text-dark" to="/games">Game</router-link>
                                <router-link class="nav-link text-dark" to="/game-genres">Game Genres</router-link>
                                <router-link class="nav-link text-dark" to="/game-on-platforms">Games On Platforms</router-link>
                                <router-link class="nav-link text-dark" to="/genres">Genres</router-link>
                                <router-link class="nav-link text-dark" to="/medias">Media</router-link>
                                <router-link class="nav-link text-dark" to="/media-types">Media Types</router-link>
                                <router-link class="nav-link text-dark" to="/platforms">Platforms</router-link>
                                <router-link class="nav-link text-dark" to="/platform-types">PlatformTypes</router-link>
                                <router-link class="nav-link text-dark" to="/progresses">Progresses</router-link>
                                <div class="dropdown-divider"></div>
                                <router-link class="nav-link text-dark" to="/roles">Roles</router-link>
                                <router-link class="nav-link text-dark" to="/user-role/create">User Role Create</router-link>
                                <router-link class="nav-link text-dark" to="/user-role/delete">User Role Delete</router-link>
                                <router-link class="nav-link text-dark" to="/users">Users</router-link>
                            </div>
                        </li>
                    </ul>

                    <ul v-if="token && user" class="navbar-nav">
                        <nav>
                            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbar-list-4" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                                <span class="navbar-toggler-icon"></span>
                            </button>
                            <div class="collapse navbar-collapse" id="navbar-list-4">
                                <ul class="navbar-nav">
                                    <li class="nav-item dropdown">
                                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <img v-if="user !== undefined && user.profilePicture !== null" v-bind:src="user.profilePicture"
                                                 width="40" height="40" class="rounded-circle" alt="">
                                            <img v-else class="img-thumbnail w-40" width="40" height="40" src="../public/default_profile_pic.webp" alt="">
                                        </a>
                                        <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                            <router-link class="dropdown-item" :to="'/profile/' + id">Profile</router-link>
                                            <router-link class="dropdown-item" to="/login" @click="logOut">Logout</router-link>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </nav>
                    </ul>
                    <ul class="navbar-nav">
                        <li v-if="token === undefined" class="nav-item">
                            <router-link class="nav-link text-dark" to="/register">Register</router-link>
                        </li>
                        <li v-if="token === undefined" class="nav-item">
                            <router-link class="nav-link text-dark" to="/login">Login</router-link>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    <div class="container">
        <main role="main" class="pb-3">
            <router-view/>
        </main>
    </div>

    <footer v-bind:style="[ this.$route.path !== '/' ? { background: '#FFFFFF'} : {}]" class="footer text-muted panel-custom panel-footer font-weight-bold">
        <div class="container">
            &copy; 2021 - Vue-client | <router-link to="/about">Privacy</router-link> | en-GB
        </div>
    </footer>
</template>
<script lang="ts">
import { Vue } from 'vue-class-component'
import store from "@/store/index";
import IAppUser from '@/domain/IAppUser'
import { BaseService } from '@/services/base-service'
export default class App extends Vue {
    private user: IAppUser | undefined;
    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    async updated(): Promise<void> {
        const token = localStorage.getItem('jwt');
        if (token !== null && token.length > 20) await store.dispatch('signIn', token);
        if (this.id === undefined || this.user) {
            return;
        }
        const userService = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await userService.get(this.id!).then((data) => {
            if (data.ok) {
                this.user = data.data!;
            } else {
                this.$router.push({ name: 'Login' })
            }
        });
        this.$forceUpdate();
    }

    logOut(): void {
        store.commit('logOut');
        this.user = undefined;
        this.$router.push('/');
    }
}
</script>
<style>

@import "views/identity/identity.css";

.panel-footer.panel-custom {
    color: #000000;
}

#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
}

html, #app {
    background: linear-gradient(to right, #0062E6, #33AEFF);
    -webkit-background-size: cover;
    -moz-background-size: cover;
    -o-background-size: cover;
    background-size: cover;
}

#nav {
    padding: 30px;
}

#nav a {
    font-weight: bold;
    color: #2c3e50;
}

#nav a.router-link-exact-active {
    color: #42b983;
}

a.navbar-brand {
    white-space: normal;
    text-align: center;
    word-break: break-all;
}

/* Provide sufficient contrast against white background */
a {
    color: #0366d6;
}

.btn-primary {
    color: #fff;
    background-color: #1b6ec2;
    border-color: #1861ac;
}

.nav-pills .nav-link.active, .nav-pills .show > .nav-link {
    color: #fff;
    background-color: #1b6ec2;
    border-color: #1861ac;
}

/* Sticky footer styles
-------------------------------------------------- */
html {
    font-size: 14px;
}
@media (min-width: 768px) {
    html {
        font-size: 16px;
    }
}

.border-top {
    border-top: 1px solid #e5e5e5;
}
.border-bottom {
    border-bottom: 1px solid #e5e5e5;
}

.box-shadow {
    box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
}

button.accept-policy {
    font-size: 1rem;
    line-height: inherit;
}

/* Sticky footer styles
-------------------------------------------------- */
html {
    position: relative;
    min-height: 100%;
}

body {
    /* Margin bottom by footer height */
    margin-bottom: 60px;
}
.footer {
    position: absolute;
    bottom: 0;
    width: 100%;
    white-space: nowrap;
    line-height: 60px; /* Vertically center the text there */
}

</style>
