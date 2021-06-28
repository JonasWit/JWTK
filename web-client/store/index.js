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
  async nuxtServerInit({dispatch, commit}, context) {
    await dispatch("auth/initialize");

    // if (context.req.headers.cookie && context.req.headers.cookie.includes('custom-color-theme')) {
    //   let reqTheme = getCookieFromRequest('custom-color-theme', context.req.headers.cookie);
    //   if (reqTheme) {
    //     if (reqTheme === 'dark') {
    //     }
    //     if (reqTheme === 'light') {
    //     }
    //     //commit('auth/setTheme', reqTheme);
    //   }
    // }
  },
};
