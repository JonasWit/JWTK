const initState = () => ({
  relatedUsers: [],
  clients: []
});

export const state = initState;

export const getters = {
  normalUsers(state) {
    return state.relatedUsers
      .filter(x => x.role === "Client");
  },
  clients(state) {
    return state.clients;
  }
};

export const mutations = {
  updateRelatedUsersList(state, {relatedUsers}) {
    state.relatedUsers = relatedUsers;
  },
  updateClients(state, {clients}) {
    state.clients = clients;
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
  getClients({commit}) {
    return this.$axios.$get("/api/legal-app-clients/admin-clients-flat")
      .then((clients) => {
        commit('updateClients', {clients});
      })
      .catch(() => {
      });
  },
};
