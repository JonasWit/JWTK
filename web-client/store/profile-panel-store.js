﻿const initState = () => ({
  relatedUsers: [],
  legalAppSummary: null,
  clients: []
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
  updateClientsList(state, {clients}) {
    state.clients = clients;
  },
  updateLegalAppSummary(state, {summary}) {
    state.legalAppSummary = summary;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  async getClients({commit}) {
    let response = await this.$axios.$get("/api/legal-app-clients/admin/flat");

    let clients = response.map(client => ({...client, key: `client-${client.id}`}));
    clients.forEach(client => {
      client.displayText = `${client.name.substring(0, this.displayTextSize)}...`;
      client.cases = client.cases.map(c => ({
        ...c, key: `case-${c.id}`,
        displayText: `${c.name.substring(0, this.displayTextSize)}...`
      }));
    });
    commit('updateClientsList', {clients});
  },
  async getRelatedUsers({commit}) {
    let relatedUsers = await this.$axios.$get("/api/legal-app-admin/related-users");

    relatedUsers.forEach(x => {
      if (x.lastLogin) {
        x.lastLogin = new Date(x.lastLogin);
      }
    });
    commit('updateRelatedUsersList', {relatedUsers});
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