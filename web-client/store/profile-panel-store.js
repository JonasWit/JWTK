const initState = () => ({
  relatedUsers: [],
  clients: [],
});

export const state = initState;

export const getters = {

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
  updateSelectedUser(state, user) {
    state.selectedUser = user;
  }
};

export const actions = {};
