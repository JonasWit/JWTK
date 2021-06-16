import {amountNet, rateNet, vatAmount, vatRate} from "@/data/functions";

const initState = () => ({
  clientForAction: null,
  contactForAction: null,


  //Contact-details and add-email dialogs
  contactDetailsFromFetch: [],
  emailsList: [],
  phoneNumbersList: [],
  addressesList: [],

  //Financials records

  financialRecordsFromFetch: [],
  billingDataFromFetch: []
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
  },
//Financials records
  workRecordsList(state) {
    return state.financialRecordsFromFetch;
  },

  sortedFinancialRecords(state) {
    const sortedItems = [...state.financialRecordsFromFetch].sort((a, b) => {
      if (a.eventDate > b.eventDate) {
        return 1
      } else if (b.eventDate > a.eventDate) {
        return -1
      }
      return 0
    })

    return sortedItems

  },

  //Billing data
  billingDataList(state) {
    return state.billingDataFromFetch;
  }

};

export const mutations = {
  setClientForAction(state, client) {
    console.warn('mutation done for setClientForAction', client);
    state.clientForAction = client;
  },

  setContactForAction(state, contact) {
    console.warn('mutation done for setContactForAction', contact);
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

  },

  reset(state) {
    Object.assign(state, initState());
  },


  //Billing data
  updateBillingDataFromFetch(state, {billingDataFromFetch}) {
    console.warn('mutation done for updateBillingDataFromFetch', billingDataFromFetch);
    state.billingDataFromFetch = billingDataFromFetch;

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
    return this.$axios.$get(`/api/legal-app-clients-work/client/${clientId}/work-records${query}`)
      .then((financialRecordsFromFetch) => {

        financialRecordsFromFetch.forEach(x => {
          x.invoiceVatAmount = vatAmount(x.amount, x.vat);
          x.invoiceDecimalVat = vatRate(x.vat);
          x.invoiceRateNet = rateNet(x.rate, x.vat);
          x.invoiceAmountNet = amountNet(x.amount, x.vat);
        })
        console.warn('Action from store: getFinancialRecordsFromFetch', financialRecordsFromFetch);
        commit('updateFinancialRecordsFromFetch', {financialRecordsFromFetch});

      }).catch((e) => {
        console.warn('error in getFinancialRecordsFromFetch', e);
      });
  },


  //Billing data

  async getBillingDataFromFetch({commit}) {
    try {
      let billingDataFromFetch = await this.$axios.$get('/api/legal-app-billing/list');
      commit('updateBillingDataFromFetch', {billingDataFromFetch});
      console.warn('Action from store = getBillingDataFromFetch', billingDataFromFetch)
    } catch (e) {
      console.warn('Action from store = getBillingDataFromFetch API error', e)

    }


  }

};
