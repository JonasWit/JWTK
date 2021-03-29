const initState = () => ({
  relatedUsers: [],
  legalAppSummary: null,
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
  updateLegalAppSummary(state, {summary}) {
    state.legalAppSummary = summary;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  getRelatedUsers({commit}) {
    return this.$axios.$get("/api/legal-app-admin/related-users")
      .then((relatedUsers) => {
        relatedUsers.forEach(x => {
          if (x.lastLogin) {
            x.lastLogin = new Date(x.lastLogin);
          }
        });
        commit('updateRelatedUsersList', {relatedUsers});
      })
      .catch(() => {
      });
  },
  getSummary({commit}) {
    return this.$axios.$get("/api/legal-app-admin/legal-app-summary")
      .then((summary) => {
        console.log('legal-app-summary', summary);
        commit('updateLegalAppSummary', {summary});
      })
      .catch(() => {
      });
  },
};
