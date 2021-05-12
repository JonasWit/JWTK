const initState = () => ({
  clientSearchItems: [],
});

const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export const state = initState;

export const getters = {
  clientItems(state) {
    return []
      .concat(state.clientSearchItems.map(x => searchItemFactory(x.name, x.id)));
  },
};

export const actions = {
  async fetchClients({commit}) {
    let clientSearchItems = await this.$axios.$get("/api/legal-app-clients/clients/basic-list");
    commit('updateClientSearchItems', {clientSearchItems});

  }
};

export const mutations = {
  updateClientSearchItems(state, {clientSearchItems}) {
    state.clientSearchItems = clientSearchItems;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};
