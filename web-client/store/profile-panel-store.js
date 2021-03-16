const initState = () => ({
  relatedUsers: [],
});

export const state = initState;

export const getters = {
  normalUsers(state) {
    return state.relatedUsers
      .filter(x => x.role === "Client");
  }
};

export const mutations = {
  updateRelatedUsersList(state, {relatedUsers}) {
    state.relatedUsers = relatedUsers;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  getRelatedUsers({commit}) {
    return this.$axios.$get("/api/legal-app-statistics/related-users")
      .then((relatedUsers) => {
        commit('updateRelatedUsersList', {relatedUsers});
      })
      .catch(() => {
      });
  },
};
