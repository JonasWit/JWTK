import {getCookieFromRequest} from "@/data/cookie-handlers";

const initState = () => ({
  name: "",
});

export const state = initState;

export const getters = {};

export const mutations = {
  reset(state) {
    Object.assign(state, initState());
  },
  SET_META(state, name) {
    console.warn(name, 'SET_META');
    state.name = name;
  },

};

export const actions = {
  async nuxtServerInit({dispatch}, context) {
    await dispatch("auth/initialize");

    if (context.req.headers.cookie) {
      let reqTheme = getCookieFromRequest('custom-color-theme', context.req.headers.cookie);
      if (reqTheme) {
        // if (reqTheme === 'dark') {
        //   this.$vuetify.theme.dark = true;
        // }
        // if (reqTheme === 'light') {
        //   this.$vuetify.theme.dark = false;
        // }
      }
    }
  },
};
