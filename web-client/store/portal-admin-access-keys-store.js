const initState = () => ({
  accessKeys: [],
});

export const state = initState;

export const getters = {
  accessKeys(state) {
    return state.accessKeys;
  }
};

export const mutations = {
  updateAccessKeysList(state, {keys}) {
    state.accessKeys = keys;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  async getAccessKeys({commit}) {
    try {
      let response = await this.$axios.$get("/api/portal-admin/key-admin/access-keys");
      console.warn('getAccessKeys from store - response', response);
      commit('updateAccessKeysList', {resposne});
    } catch (error) {

    }
  },
};
