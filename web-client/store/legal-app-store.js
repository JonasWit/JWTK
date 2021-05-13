const initState = () => ({
  clientsList: [],
  clientSearchItems: [],
});

// Clients list - create autocomplete
const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export const state = initState;

export const getters = {
  clientsList(state) {
    return state.clientsList;
  },

// Clients list - create autocomplete
  clientItems(state) {
    return []
      .concat(state.clientSearchItems.map(x => searchItemFactory(x.name, x.id)));
  },
};

export const mutations = {
  updateClientsList(state, {list}) {
    state.clientsList = list;
  },
// Clients list - create autocomplete
  updateClientSearchItems(state, {clientSearchItems}) {
    state.clientSearchItems = clientSearchItems;
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
  // Clients list - gets basic list of all clients
  async fetchClients({commit}) {
    let clientSearchItems = await this.$axios.$get("/api/legal-app-clients/clients/basic-list");
    commit('updateClientSearchItems', {clientSearchItems});

  },
};
