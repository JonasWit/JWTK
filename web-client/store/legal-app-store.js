const initState = () => ({
  clientsList: [],
});

export const state = initState;

export const getters = {
  clientsList(state) {
    return state.clientsList;
  }
};

export const mutations = {
  updateClientsList(state, {list}) {
    state.clientsList = list;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  getClientsList({commit}) {
    return this.$axios.$get("/api/portal-admin/user-admin/users")
      .then((list) => {
        commit('updateClientsList', {list});
      })
      .catch(() => {
      });
  },
};
