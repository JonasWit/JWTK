import * as fs from "fs";
import path from 'path';
import colors from 'vuetify/es5/util/colors';

const config = {
  // Global page headers: https://go.nuxtjs.dev/config-head
  publicRuntimeConfig: {
    auth: {
      loginPath: process.env.LOGIN_PATH,
      changePassPath: process.env.PASSWORD_CHANGE_PATH,
      logoutPath: process.env.LOGOUT_PATH,
      deletePath: process.env.DELETE_PATH
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
    '~/plugins/notifier',
    '~/plugins/bus-client.js'
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
      path: '~/components/legal-app',
      prefix: 'legalapp'
    },
    {
      path: '~/components/legal-app/clients',
      prefix: 'legalapp'
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
          primary: colors.blue.darken2,
          accent: colors.grey.darken3,
          secondary: colors.amber.darken3,

          info: colors.teal.lighten1,
          warning: colors.amber.base,
          error: colors.deepOrange.accent4,
          success: colors.green.accent3,

        },
        light: {
          primary: '#1976D2',
          secondary: colors.teal.lighten3,
          accent: colors.teal.accent4,

          error: '#FF5252',
          info: '#2196F3',
          success: '#4CAF50',
          warning: '#FFC107',

        }
      },
    }
  },

  loading: {
    color: 'crimson',
    height: '5px'
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
