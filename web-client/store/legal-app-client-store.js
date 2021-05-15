const initState = () => ({
  clientForAction: null,
});

export const state = initState;

export const getters = {};

export const mutations = {
  setClientForAction(state, client) {
    console.warn('mutation done', client)
    state.clientForAction = client
  }
};

export const actions = {};
