import {createRouter, createWebHistory} from 'vue-router'
import Home from '../views/Home.vue'
import About from "@/views/About";
import Privacy from "@/views/Privacy";
import Contact from "@/views/Contact";
import Profile from "@/views/Profile";
import Login from "@/views/auth/Login";
import Register from "@/views/auth/Register";
import store from "@/store";
import Solutions from "@/views/Solutions";
import ChangePassword from "@/views/auth/ChangePassword";
import Logout from "@/views/auth/Logout";
import ToS from "@/views/ToS";
import DeleteAccount from "@/views/auth/DeleteAccount";
import Menus from "@/views/apps/gastronomy/Menus";
import Dishes from "@/views/apps/gastronomy/Dishes";
import Ingredients from "@/views/apps/gastronomy/Ingredients";
import ControlPanel from "@/views/apps/gastronomy/ControlPanel";
import Sources from "@/views/Sources";
import GastronomyCalculator from "@/views/apps/gastronomy/GastronomyCalculator";
import ResetPassword from "@/views/auth/ResetPassword";
import ForgotPassword from "@/views/auth/ForgotPassword";

//route guards
const requireAuth = (to, from, next) => {
    let auth = store.getters['auth/isAuthorized'];
    if (!auth) {
        next({name: 'Login'})
    } else {
        next()
    }
}

//custom actions
const scrollUp = (to, from, next) => {
    next()
    window.scrollTo(0, 0)
    store.commit('hideNavBar')
}

const routes = [
    {
        path: '/',
        name: 'Home',
        component: Home,
        beforeEnter: scrollUp,
        meta: {
            title: 'systemywp.pl',
            metaTags: [
                {
                    name: 'description',
                    content: 'Strona Główna'
                },
            ]
        }
    },
    {
        path: '/sources',
        name: 'Sources',
        component: Sources,
        beforeEnter: scrollUp,
        meta: {
            title: 'systemywp.pl',
            metaTags: [
                {
                    name: 'description',
                    content: 'Źródła'
                },
            ]
        }
    },
    {
        path: '/auth/login',
        name: 'Login',
        component: Login,
        beforeEnter: scrollUp,
        meta: {
            title: 'Logowanie',
            metaTags: [
                {
                    name: 'Strona Logowania',
                    content: 'Strona Logowania'
                },
            ]
        }
    },
    {
        path: '/auth/logout',
        name: 'Logout',
        component: Logout,
        beforeEnter: [requireAuth, scrollUp],
        meta: {
            title: 'Wyloguj',
        }
    },
    {
        path: '/auth/change-password',
        name: 'ChangePassword',
        component: ChangePassword,
        beforeEnter: [requireAuth, scrollUp],
        meta: {
            title: 'Zamiana Hasła',
            metaTags: [
                {
                    name: 'description',
                    content: 'Zmiana Hasła'
                },
            ]
        }
    },
    {
        path: '/auth/register',
        name: 'Register',
        component: Register,
        beforeEnter: scrollUp,
        meta: {
            title: 'Rejestracja',
            metaTags: [
                {
                    name: 'description',
                    content: 'Strona Rejestracji'
                },
            ]
        }
    },
    {
        path: '/auth/delete-account',
        name: 'DeleteAccount',
        component: DeleteAccount,
        beforeEnter: [requireAuth, scrollUp],
        meta: {
            title: 'Usunięcie Konta',
            metaTags: [
                {
                    name: 'description',
                    content: 'Usunięcie Konta'
                },
            ]
        }
    },
    {
        path: '/auth/forgot-password',
        name: 'ForgotPassword',
        component: ForgotPassword,
        beforeEnter: [scrollUp],
        meta: {
            title: 'Przypomnienie Hasła',
            metaTags: [
                {
                    name: 'description',
                    content: 'Przypomnienie Hasła'
                },
            ]
        }
    },
    {
        path: '/auth/reset-password',
        name: 'ResetPassword',
        component: ResetPassword,
        beforeEnter: [scrollUp],
        meta: {
            title: 'Reset Hasła',
            metaTags: [
                {
                    name: 'description',
                    content: 'Rest Hasła'
                },
            ]
        }
    },
    {
        path: '/about',
        name: 'About',
        component: About,
        beforeEnter: scrollUp,
        meta: {
            title: 'O Nas',
        }
    },
    {
        path: '/privacy',
        name: 'Privacy',
        component: Privacy,
        beforeEnter: scrollUp,
        meta: {
            title: 'Polityka Prywatności',
        }
    },
    {
        path: '/contact',
        name: 'Contact',
        component: Contact,
        beforeEnter: scrollUp,
        meta: {
            title: 'Kontakt',
        }
    },
    {
        path: '/profile',
        name: 'Profile',
        component: Profile,
        beforeEnter: scrollUp,
        meta: {
            title: 'Profil',
        }
    },
    {
        path: '/tos',
        name: 'ToS',
        component: ToS,
        beforeEnter: scrollUp,
        meta: {
            title: 'Regulamin',
        }
    },
    {
        path: '/apps/gastronomy/control-panel',
        name: 'GastronomyControlPanel',
        component: ControlPanel,
        beforeEnter: [requireAuth, scrollUp],
        meta: {
            title: 'Panel',
        }
    },
    {
        path: '/apps/gastronomy/menus',
        name: 'GastronomyMenus',
        component: Menus,
        beforeEnter: [requireAuth, scrollUp],
        meta: {
            title: 'Menu',
        }
    },
    {
        path: '/apps/gastronomy/dishes',
        name: 'GastronomyDishes',
        component: Dishes,
        beforeEnter: [requireAuth, scrollUp],
        meta: {
            title: 'Dania',
        }
    },
    {
        path: '/apps/gastronomy/calculator',
        name: 'GastronomyCalculator',
        component: GastronomyCalculator,
        beforeEnter: [requireAuth, scrollUp],
        meta: {
            title: 'Kalkulator',
        }
    },
    {
        path: '/apps/gastronomy/ingredients',
        name: 'GastronomyIngredients',
        component: Ingredients,
        beforeEnter: [requireAuth, scrollUp],
        meta: {
            title: 'Składniki',
        }
    },
    {
        path: '/solutions',
        name: 'Solutions',
        component: Solutions,
        beforeEnter: scrollUp,
        meta: {
            title: 'Rozwiązania dla Firm',
        }
    },
    // {
    //   path: '/about',
    //   name: 'About',
    //   // route level code-splitting
    //   // this generates a separate chunk (about.[hash].js) for this route
    //   // which is lazy-loaded when the route is visited.
    //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
    // }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
    mode: 'history',
    linkActiveClass: 'nav-active-link'
})

//Tab Title
router.beforeEach((to, from, next) => {
    const nearestWithTitle = to.matched.slice().reverse().find(r => r.meta && r.meta.title);
    const nearestWithMeta = to.matched.slice().reverse().find(r => r.meta && r.meta.metaTags);
    const previousNearestWithMeta = from.matched.slice().reverse().find(r => r.meta && r.meta.metaTags);

    if (nearestWithTitle) {
        document.title = nearestWithTitle.meta.title;
    } else if (previousNearestWithMeta) {
        document.title = previousNearestWithMeta.meta.title;
    }

    Array.from(document.querySelectorAll('[data-vue-router-controlled]')).map(el => el.parentNode.removeChild(el));

    if (!nearestWithMeta) return next();
    nearestWithMeta.meta.metaTags.map(tagDef => {
        const tag = document.createElement('meta');

        Object.keys(tagDef).forEach(key => {
            tag.setAttribute(key, tagDef[key]);
        });

        tag.setAttribute('data-vue-router-controlled', '');

        return tag;
    }).forEach(tag => document.head.appendChild(tag));

    next();
});

export default router
