import {createRouter, createWebHistory} from 'vue-router'
import Home from '../views/Home.vue'
import About from "@/views/About";
import Privacy from "@/views/Privacy";
import Contact from "@/views/Contact";
import Profile from "@/views/Profile";
import Gastronomy from "@/views/apps/Gastronomy";
import Login from "@/views/auth/Login";
import Register from "@/views/auth/Register";
import store from "@/store";
import Solutions from "@/views/Solutions";
import ChangePassword from "@/views/auth/ChangePassword";
import Logout from "@/views/auth/Logout";
import ToS from "@/views/ToS";

//route guards
const requireAuth = (to, from, next) => {
    let auth = store.getters['auth/isAuthorized'];
    if (!auth){
        next( { name: 'Login'} )
    } else {
        next()
    }
}

const routes = [
    {
        path: '/',
        name: 'Home',
        component: Home,
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
        path: '/auth/login',
        name: 'Login',
        component: Login,
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
        beforeEnter: requireAuth,
        meta: {
            title: 'Wyloguj',
        }
    },
    {
        path: '/auth/changepassword',
        name: 'ChangePassword',
        component: ChangePassword,
        beforeEnter: requireAuth,
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
        path: '/about',
        name: 'About',
        component: About,
        meta: {
            title: 'O Nas',
        }
    },
    {
        path: '/privacy',
        name: 'Privacy',
        component: Privacy,
        meta: {
            title: 'Polityka Prywatności',
        }
    },
    {
        path: '/contact',
        name: 'Contact',
        component: Contact,
        meta: {
            title: 'Kontakt',
        }
    },
    {
        path: '/profile',
        name: 'Profile',
        component: Profile,
        meta: {
            title: 'Profil',
        }
    },
    {
        path: '/tos',
        name: 'ToS',
        component: ToS,
        meta: {
            title: 'Regulamin',
        }
    },
    {
        path: '/apps/gastronomy',
        name: 'Gastronomy',
        component: Gastronomy,
        beforeEnter: requireAuth,
        meta: {
            title: 'Gastronomia',
        }
    },
    {
        path: '/solutions',
        name: 'Solutions',
        component: Solutions,
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
    mode: "history"
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
