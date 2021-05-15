const initState = () => ({
  deletedClient: null,
});

export const state = initState;

export const getters = {};

export const mutations = {
  setDeletedClient(state, client) {
    console.warn('mutation done', client)
    state.deletedClient = client
  }
};

export const actions = {};
