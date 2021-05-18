const initState = () => ({
  clientForAction: null,
  contactForAction: null,
});

export const state = initState;

export const getters = {};

export const mutations = {
  setClientForAction(state, client) {
    console.warn('mutation done', client)
    state.clientForAction = client
  },

  setContactForAction(state, contact) {
    console.warn('mutation done for contact', contact)
  }
};

export const actions = {};
