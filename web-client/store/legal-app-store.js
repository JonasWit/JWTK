const initState = () => ({
  clientSearchItems: [],
  clientList: [],
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

// Clients list - create autocomplete
  clientItems(state) {
    return []
      .concat(state.clientSearchItems.map(x => searchItemFactory(x.name, x.id)));
  },
};

export const mutations = {
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
    try {
      let clientSearchItems = await this.$axios.$get("/api/legal-app-clients/clients/basic-list");
      commit('updateClientSearchItems', {clientSearchItems});
    } catch (error) {
      console.error(error);
    }


  },


};
