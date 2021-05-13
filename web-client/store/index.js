const initState = () => ({});

export const state = initState;

export const mutations = {
  reset(state) {
    Object.assign(state, initState());
  }
};

export const actions = {
  async nuxtServerInit({dispatch}) {
    console.warn('vuex initialized');
    await dispatch("auth/initialize");
  },
};
