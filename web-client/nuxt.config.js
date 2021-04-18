import colors from 'vuetify/es5/util/colors';
import * as fs from "fs";
import path from 'path';

const config = {
  // Global page headers: https://go.nuxtjs.dev/config-head
  publicRuntimeConfig: {
    auth: {
      loginPath: process.env.LOGIN_PATH,
      changePassPath: process.env.PASSWORD_CHANGE_PATH,
      logoutPath: process.env.LOGOUT_PATH,
    },
    axios: {
      baseURL: process.env.BROWSER_SIDE_URL,
      https: true
    },
  },
  privateRuntimeConfig: {
    axios: {
      baseURL: 'http://localhost:5000',
      https: false
    },
  },
  head: {
    titleTemplate: '%s - Systemy Wspomagania Pracy',
    title: 'SWP',
    htmlAttrs: {
      lang: 'en'
    },
    meta: [
      {charset: 'utf-8'},
      {name: 'viewport', content: 'width=device-width, initial-scale=1'},
      {hid: 'description', name: 'description', content: ''}
    ],
    link: [
      {rel: 'icon', type: 'image/x-icon', href: '/favicon.ico'}
    ]
  },

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
    '@/assets/css/main.scss'
  ],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
    '~/plugins/axios',
    '~/plugins/notifier'
  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: [
    {
      path: '~/components/',
    },
    {
      path: '~/components/auth/',
    },
    {
      path: '~/components/home-page/',
    },
    {
      path: '~/components/user-profile',
      prefix: 'prof'
    },
    {
      path: '~/components/user-profile/legal-app-access-details',
      prefix: 'prof'
    },
    {
      path: '~/components/portal-admin/',
      prefix: 'padmin'
    },
    {
      path: '~/components/portal-admin/access-keys',
      prefix: 'padmin'
    },
    {
      path: '~/components/portal-admin/users-management',
      prefix: 'padmin'
    },
    {
      path: '~/components/portal-admin/log-manager',
      prefix: 'padmin'
    },

  ],

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/vuetify
    '@nuxtjs/vuetify',
    '@nuxtjs/dotenv'
  ],

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    // https://go.nuxtjs.dev/axios
    '@nuxtjs/axios',
  ],

  // Vuetify module configuration: https://go.nuxtjs.dev/config-vuetify
  vuetify: {
    theme: {
      options: {
        customProperties: true
      },
      dark: true,
      themes: {
        dark: {

          primary: '#B41946',
          secondary: '#6F8AB7',
          accent: '#06D6A0',
          anchor: '#ececea',

          error: '#F24333',
          info: '#3E8989',
          success: '#2CDA9D',
          warning: '#FFC107',

        },
        light: {

          primary: '#B41946',
          secondary: '#6F8AB7',
          accent: '#06D6A0',
          anchor: '#000210',

          error: '#F24333',
          info: '#3E8989',
          success: '#4B8F8C',
          warning: '#FFC107',
        }
      },
    }
  },

  build: {
    extend(config, ctx) {
    },
    terser: {
      terserOptions: {
        compress: {
          drop_console: true
        }
      }
    }
  }
};

if (process.env.NODE_ENV === 'development') {
  config.server = {
    https: {
      key: fs.readFileSync(path.relative(__dirname, 'server.key')),
      cert: fs.readFileSync(path.relative(__dirname, 'server.cert')),
    }
  };
}

export default config;
