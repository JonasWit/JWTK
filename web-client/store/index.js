const initState = () => ({
  name: ""
});

export const state = initState;

export const mutations = {
  reset(state) {
    Object.assign(state, initState());
  },

  SET_META(state, name) {
    console.warn(name, 'SET_META')
    state.name = name
  }

};

export const actions = {
  async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize");
  },
};
