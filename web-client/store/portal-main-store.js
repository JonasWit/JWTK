const initState = () => ({
  theme: ""
});

export const state = initState;

export const getters = {
  currentTheme(state) {
    if (state.theme) {
      return state.theme;
    }
    return "dark";
  },
};

export const mutations = {
  reset(state) {
    Object.assign(state, initState());
  },
  setTheme(state, theme) {
    state.theme = theme;
  },
};

export const actions = {
  setTheme({dispatch}, reqTheme) {
    if (reqTheme) {
      commit('setTheme', {reqTheme});
      console.error('TEST', test);
      console.error('COOKIES', context.req.headers.cookie);
    }
  }
};
