import {getCookieFromRequest} from "@/data/cookie-handlers";

const initState = () => ({
  name: "",
  theme: ""
});

export const state = initState;

export const mutations = {
  reset(state) {
    Object.assign(state, initState());
  },
  SET_META(state, name) {
    console.warn(name, 'SET_META');
    state.name = name;
  },
  setTheme(state, theme) {
    state.theme = theme;
  },
};

export const actions = {
  async nuxtServerInit({dispatch}, context) {
    await dispatch("auth/initialize");

    if (context.req.headers.cookie) {
      let test = getCookieFromRequest('custom-color-theme', context.req.headers.cookie);

      console.error('TEST', test);
      console.error('COOKIES', context.req.headers.cookie);
    }


  },
};
