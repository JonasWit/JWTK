const initState = () => ({
  clientForAction: null,
  contactForAction: null,


  //Contact-details and add-email dialogs
  contactDetailsFromFetch: [],
  emailsList: [],
  phoneNumbersList: [],
  addressesList: [],

  //Financials records
  FinancialRecordForAction: null,
  financialRecordsFromFetch: []
});

export const state = initState;

export const getters = {

  //Contact-details and add-email dialogs
  emailsList(state) {
    return state.contactDetailsFromFetch.emails;
  },

  phoneNumbersList(state) {
    return state.contactDetailsFromFetch.phoneNumbers;
  },

  addressesList(state) {
    return state.contactDetailsFromFetch.physicalAddresses;
  }

};

export const mutations = {
  setClientForAction(state, client) {
    console.warn('mutation done', client);
    state.clientForAction = client;
  },

  setContactForAction(state, contact) {
    console.warn('mutation done for contact', contact);
  },
//Financials records
  setFinancialRecordForAction(state, financialRecord) {
    state.FinancialRecordForAction = financialRecord;
  },
  //Contact-details and add-email dialogs
  updateContactDetailsList(state, {contactDetailsFromFetch}) {
    console.warn('mutation done for updateContactDetailsList', contactDetailsFromFetch);
    state.contactDetailsFromFetch = contactDetailsFromFetch;

  },
//Financials records
  updateFinancialRecordsFromFetch(state, {financialRecordsFromFetch}) {
    console.warn('mutation done for updateFinancialRecordsFromFetch', financialRecordsFromFetch);
    state.financialRecordsFromFetch = financialRecordsFromFetch;
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
  },
//Financials records
  getFinancialRecordsFromFetch({commit}, {clientId, query}) {
    return this.$axios.$get(`/api/legal-app-clients-finance/client/${clientId}/finance-records${query}`)
      .then((financialRecordsFromFetch) => {
        console.warn('Action from store: getFinancialRecordsFromFetch', financialRecordsFromFetch);
        commit('updateFinancialRecordsFromFetch', {financialRecordsFromFetch});

      }).catch((e) => {
        console.warn('error in getFinancialRecordsFromFetch', e);
      });
  },

};
