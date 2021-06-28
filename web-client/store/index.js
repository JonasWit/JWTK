import {getCookieFromRequest} from "@/data/cookie-handlers";

const initState = () => ({
  name: ""
});

export const state = initState;

export const mutations = {
  reset(state) {
    Object.assign(state, initState());
  },

  SET_META(state, name) {
    console.warn(name, 'SET_META');
    state.name = name;
  }

};

export const actions = {
  async nuxtServerInit({dispatch}, context) {
    await dispatch("auth/initialize");


    //let accessToken = getCookieFromRequest('custom-color-theme', context.req.headers.cookie);

    console.error('server init action', context.req.headers.cookie);


  },
};
