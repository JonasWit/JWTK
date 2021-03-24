const initState = () => ({
  relatedUsers: [],
});

export const state = initState;

export const getters = {
  relatedUsers(state) {
    return state.relatedUsers;
  },
  normalUsers(state) {
    return state.relatedUsers
      .filter(x => x.role === "Client");
  },
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
    return this.$axios.$get("/api/legal-app-admin/related-users")
      .then((relatedUsers) => {
        console.log('relatedUsers', relatedUsers);
        commit('updateRelatedUsersList', {relatedUsers});
      })
      .catch(() => {
      });
  },
};
