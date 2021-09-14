import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../views/Home.vue';
import Login from '@/views/identity/Login.vue';
import Register from '@/views/identity/Register.vue';
import CompanyIndex from '@/views/company/Index.vue';
import CompanyCreate from '@/views/company/Create.vue';
import CompanyDetails from '@/views/company/Details.vue';
import CompanyEdit from '@/views/company/Edit.vue';
import CompanyDelete from '@/views/company/Delete.vue';
import PlatformIndex from '@/views/platform/Index.vue';
import PlatformCreate from '@/views/platform/Create.vue';
import PlatformDetails from '@/views/platform/Details.vue';
import PlatformEdit from '@/views/platform/Edit.vue';
import PlatformDelete from '@/views/platform/Delete.vue';
import PlatformTypeIndex from '@/views/platform-type/Index.vue';
import PlatformTypeCreate from '@/views/platform-type/Create.vue';
import PlatformTypeDetails from '@/views/platform-type/Details.vue';
import PlatformTypeEdit from '@/views/platform-type/Edit.vue';
import PlatformTypeDelete from '@/views/platform-type/Delete.vue';
import MediaTypeIndex from '@/views/media-type/Index.vue';
import MediaTypeEdit from '@/views/media-type/Edit.vue';
import MediaTypeDelete from '@/views/media-type/Delete.vue';
import MediaTypeDetails from '@/views/media-type/Details.vue';
import MediaTypeCreate from '@/views/media-type/Create.vue';
import CompatibilityTypeIndex from '@/views/compatibility-type/Index.vue';
import CompatibilityTypeDelete from '@/views/compatibility-type/Delete.vue';
import CompatibilityTypeEdit from '@/views/compatibility-type/Edit.vue';
import CompatibilityTypeDetails from '@/views/compatibility-type/Details.vue';
import CompatibilityTypeCreate from '@/views/compatibility-type/Create.vue';
import EmulatorIndex from '@/views/emulator/Index.vue';
import EmulatorDetails from '@/views/emulator/Details.vue';
import EmulatorCreate from '@/views/emulator/Create.vue';
import EmulatorEdit from '@/views/emulator/Edit.vue';
import EmulatorDelete from '@/views/emulator/Delete.vue';
import GenreIndex from '@/views/genre/Index.vue';
import GenreCreate from '@/views/genre/Create.vue';
import GenreDetails from '@/views/genre/Details.vue';
import GenreEdit from '@/views/genre/Edit.vue';
import GenreDelete from '@/views/genre/Delete.vue';
import GameIndex from '@/views/game/Index.vue';
import GameCreate from '@/views/game/Create.vue';
import GameDetails from '@/views/game/Details.vue';
import GameEdit from '@/views/game/Edit.vue';
import GameDelete from '@/views/game/Delete.vue';
import GameGenreIndex from '@/views/game-genre/Index.vue';
import GameGenreCreate from '@/views/game-genre/Create.vue';
import GameGenreDetails from '@/views/game-genre/Details.vue';
import GameGenreDelete from '@/views/game-genre/Delete.vue';
import GameOnPlatformIndex from '@/views/game-on-platform/Index.vue';
import GameOnPlatformCreate from '@/views/game-on-platform/Create.vue';
import GameOnPlatformEdit from '@/views/game-on-platform/Edit.vue';
import GameOnPlatformDetails from '@/views/game-on-platform/Details.vue';
import GameOnPlatformDelete from '@/views/game-on-platform/Delete.vue';
import CompatibilityIndex from '@/views/compatibility/Index.vue';
import CompatibilityCreate from '@/views/compatibility/Create.vue';
import CompatibilityDetails from '@/views/compatibility/Details.vue';
import CompatibilityEdit from '@/views/compatibility/Edit.vue';
import CompatibilityDelete from '@/views/compatibility/Delete.vue';
import ConfigurationIndex from '@/views/configuration/Index.vue';
import ConfigurationDetails from '@/views/configuration/Details.vue';
import ConfigurationCreate from '@/views/configuration/Create.vue';
import ConfigurationDelete from '@/views/configuration/Delete.vue';
import ConfigurationEdit from '@/views/configuration/Edit.vue';
import MediaIndex from '@/views/media/Index.vue';
import MediaCreate from '@/views/media/Create.vue';
import MediaDetails from '@/views/media/Details.vue';
import MediaEdit from '@/views/media/Edit.vue';
import MediaDelete from '@/views/media/Delete.vue';
import RoleIndex from '@/views/app-role/Index.vue';
import RoleCreate from '@/views/app-role/Create.vue';
import RoleDetails from '@/views/app-role/Details.vue';
import RoleEdit from '@/views/app-role/Edit.vue';
import RoleDelete from '@/views/app-role/Delete.vue';
import UserRoleCreate from '@/views/app-user-role/Create.vue';
import UserRoleDelete from '@/views/app-user-role/Delete.vue';
import UserIndex from '@/views/app-user/Index.vue';
import UserDetails from '@/views/app-user/Details.vue';
import UserDelete from '@/views/app-user/Delete.vue';
import UserEdit from '@/views/app-user/Edit.vue';
import ProgressIndex from '@/views/progress/Index.vue'
import ProgressCreate from '@/views/progress/Create.vue'
import ProgressDetails from '@/views/progress/Details.vue'
import ProgressDelete from '@/views/progress/Delete.vue'
import ProgressEdit from '@/views/progress/Edit.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'Home',
        component: Home
    },
    {
        path: '/register',
        name: 'Register',
        component: Register
    },
    {
        path: '/login',
        name: 'Login',
        component: Login
    },
    // companies
    { path: '/companies', name: 'companies', component: CompanyIndex },
    { path: '/company/create', name: 'company-create', component: CompanyCreate },
    { path: '/company/details/:id', name: 'company-details', component: CompanyDetails, props: true },
    { path: '/company/edit/:id', name: 'company-edit', component: CompanyEdit, props: true },
    { path: '/company/delete/:id', name: 'company-delete', component: CompanyDelete, props: true },
    // compatibilities
    { path: '/compatibilities', name: 'compatibilities', component: CompatibilityIndex },
    { path: '/compatibility/create', name: 'compatibility-create', component: CompatibilityCreate },
    { path: '/compatibility/details/:id', name: 'compatibility-details', component: CompatibilityDetails, props: true },
    { path: '/compatibility/edit/:id', name: 'compatibility-edit', component: CompatibilityEdit, props: true },
    { path: '/compatibility/delete/:id', name: 'compatibility-delete', component: CompatibilityDelete, props: true },
    // compatibility-types
    { path: '/compatibility-types', name: 'compatibility-types', component: CompatibilityTypeIndex },
    { path: '/compatibility-type/create', name: 'compatibility-type-create', component: CompatibilityTypeCreate },
    { path: '/compatibility-type/details/:id', name: 'compatibility-type-details', component: CompatibilityTypeDetails, props: true },
    { path: '/compatibility-type/edit/:id', name: 'compatibility-type-edit', component: CompatibilityTypeEdit, props: true },
    { path: '/compatibility-type/delete/:id', name: 'compatibility-type-delete', component: CompatibilityTypeDelete, props: true },
    // configurations
    { path: '/configurations', name: 'configurations', component: ConfigurationIndex },
    { path: '/configuration/create', name: 'configuration-create', component: ConfigurationCreate },
    { path: '/configuration/details/:id', name: 'configuration-details', component: ConfigurationDetails, props: true },
    { path: '/configuration/edit/:id', name: 'configuration-edit', component: ConfigurationEdit, props: true },
    { path: '/configuration/delete/:id', name: 'configuration-delete', component: ConfigurationDelete, props: true },
    // emulators
    { path: '/emulators', name: 'emulators', component: EmulatorIndex },
    { path: '/emulator/create', name: 'emulator-create', component: EmulatorCreate },
    { path: '/emulator/details/:id', name: 'emulator-details', component: EmulatorDetails, props: true },
    { path: '/emulator/edit/:id', name: 'emulator-edit', component: EmulatorEdit, props: true },
    { path: '/emulator/delete/:id', name: 'emulator-delete', component: EmulatorDelete, props: true },
    // games
    { path: '/games', name: 'games', component: GameIndex },
    { path: '/game/create', name: 'game-create', component: GameCreate },
    { path: '/game/details/:id', name: 'game-details', component: GameDetails, props: true },
    { path: '/game/edit/:id', name: 'game-edit', component: GameEdit, props: true },
    { path: '/game/delete/:id', name: 'game-delete', component: GameDelete, props: true },
    // game-genres
    { path: '/game-genres', name: 'game-genres', component: GameGenreIndex },
    { path: '/game-genre/create', name: 'game-genre-create', component: GameGenreCreate },
    { path: '/game-genre/details/:id', name: 'game-genre-details', component: GameGenreDetails, props: true },
    { path: '/game-genre/delete/:id', name: 'game-genre-delete', component: GameGenreDelete, props: true },
    // game-on-platforms
    { path: '/game-on-platforms', name: 'game-on-platforms', component: GameOnPlatformIndex },
    { path: '/game-on-platform/create', name: 'game-on-platform-create', component: GameOnPlatformCreate },
    { path: '/game-on-platform/details/:id', name: 'game-on-platform-details', component: GameOnPlatformDetails, props: true },
    { path: '/game-on-platform/edit/:id', name: 'game-on-platform-edit', component: GameOnPlatformEdit, props: true },
    { path: '/game-on-platform/delete/:id', name: 'game-on-platform-delete', component: GameOnPlatformDelete, props: true },
    // genres
    { path: '/genres', name: 'genres', component: GenreIndex },
    { path: '/genre/create', name: 'genre-create', component: GenreCreate },
    { path: '/genre/details/:id', name: 'genre-details', component: GenreDetails, props: true },
    { path: '/genre/edit/:id', name: 'genre-edit', component: GenreEdit, props: true },
    { path: '/genre/delete/:id', name: 'genre-delete', component: GenreDelete, props: true },
    // media
    { path: '/medias', name: 'medias', component: MediaIndex },
    { path: '/media/create', name: 'media-create', component: MediaCreate },
    { path: '/media/details/:id', name: 'media-details', component: MediaDetails, props: true },
    { path: '/media/edit/:id', name: 'media-edit', component: MediaEdit, props: true },
    { path: '/media/delete/:id', name: 'media-delete', component: MediaDelete, props: true },
    // media-types
    { path: '/media-types', name: 'media-types', component: MediaTypeIndex },
    { path: '/media-type/create', name: 'media-type-create', component: MediaTypeCreate },
    { path: '/media-type/details/:id', name: 'media-type-details', component: MediaTypeDetails, props: true },
    { path: '/media-type/edit/:id', name: 'media-type-edit', component: MediaTypeEdit, props: true },
    { path: '/media-type/delete/:id', name: 'media-type-delete', component: MediaTypeDelete, props: true },
    // platforms
    { path: '/platforms', name: 'platforms', component: PlatformIndex },
    { path: '/platform/create', name: 'platform-create', component: PlatformCreate },
    { path: '/platform/details/:id', name: 'platform-details', component: PlatformDetails, props: true },
    { path: '/platform/edit/:id', name: 'platform-edit', component: PlatformEdit, props: true },
    { path: '/platform/delete/:id', name: 'platform-delete', component: PlatformDelete, props: true },
    // platform-types
    { path: '/platform-types', name: 'platform-types', component: PlatformTypeIndex },
    { path: '/platform-type/create', name: 'platform-type-create', component: PlatformTypeCreate },
    { path: '/platform-type/details/:id', name: 'platform-type-details', component: PlatformTypeDetails, props: true },
    { path: '/platform-type/edit/:id', name: 'platform-type-edit', component: PlatformTypeEdit, props: true },
    { path: '/platform-type/delete/:id', name: 'platform-type-delete', component: PlatformTypeDelete, props: true },
    // progresses
    { path: '/progresses', name: 'progresses', component: ProgressIndex },
    { path: '/progress/create', name: 'progress-create', component: ProgressCreate },
    { path: '/progress/details/:id', name: 'progress-details', component: ProgressDetails, props: true },
    { path: '/progress/edit/:id', name: 'progress-edit', component: ProgressEdit, props: true },
    { path: '/progress/delete/:id', name: 'progress-delete', component: ProgressDelete, props: true },
    // roles
    { path: '/roles', name: 'roles', component: RoleIndex },
    { path: '/role/create', name: 'role-create', component: RoleCreate },
    { path: '/role/details/:id', name: 'role-details', component: RoleDetails, props: true },
    { path: '/role/edit/:id', name: 'role-edit', component: RoleEdit, props: true },
    { path: '/role/delete/:id', name: 'role-delete', component: RoleDelete, props: true },
    // user-roles
    { path: '/user-role/create', name: 'user-role-create', component: UserRoleCreate },
    { path: '/user-role/delete', name: 'user-role-delete', component: UserRoleDelete },
    // users
    { path: '/users', name: 'users', component: UserIndex },
    { path: '/user/details/:id', name: 'user-details', component: UserDetails, props: true },
    { path: '/user/edit/:id', name: 'user-edit', component: UserEdit, props: true },
    { path: '/user/delete/:id', name: 'user-delete', component: UserDelete, props: true },
    {
        path: '/emulators/list/',
        name: 'EmulatorList',
        component: () => import(/* webpackChunkName: "about" */ '../views/emulator/EmulatorList.vue')
    },
    {
        path: '/games/list/',
        name: 'GameList',
        component: () => import(/* webpackChunkName: "about" */ '../views/game/GamesList.vue')
    },
    {
        path: '/program/:id',
        name: 'GameView',
        props: true,
        component: () => import(/* webpackChunkName: "about" */ '../views/game/GameView.vue')
    },
    {
        path: '/profile/:id',
        name: 'Profile',
        props: true,
        component: () => import(/* webpackChunkName: "about" */ '../views/profile/Overview.vue')
    },
    {
        path: '/about',
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router
