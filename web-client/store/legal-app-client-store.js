const initState = () => ({
  clientForAction: null,
  contactForAction: null,

  //Contact-details and add-email dialogs
  contactDetailsFromFetch: [],
  emailsList: [],
  phoneNumbersList: [],
  addressesList: [],

});

export const state = initState;

export const getters = {

  //Contact-details and add-email dialogs
  emailsList(state) {
    return state.contactDetailsFromFetch.emails
  },

  phoneNumbersList(state) {
    return state.contactDetailsFromFetch.phoneNumbers
  },

  addressesList(state) {
    return state.contactDetailsFromFetch.physicalAddresses
  }

};

export const mutations = {
  setClientForAction(state, client) {
    console.warn('mutation done', client)
    state.clientForAction = client
  },

  setContactForAction(state, contact) {
    console.warn('mutation done for contact', contact)
  },

  //Contact-details and add-email dialogs
  updateContactDetailsList(state, {contactDetailsFromFetch}) {
    console.warn('mutation done for updateContactDetailsList', contactDetailsFromFetch)
    state.contactDetailsFromFetch = contactDetailsFromFetch

  }
};

export const actions = {
  //Contact-details and add-email dialogs
  getContactDetailsFromFetch({commit}, {clientId, contactId}) {
    return this.$axios.$get(`/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}`)
      .then((contactDetailsFromFetch) => {
        commit('updateContactDetailsList', {contactDetailsFromFetch});
      })
      .catch(() => {
      });
  }


};
