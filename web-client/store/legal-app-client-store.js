import {amountNet, groupByKey, rateNet, vatAmount, vatRate} from "@/data/functions";
import {formatDateToMonth} from "@/data/date-extensions";

const initState = () => ({
  //Clients
  clientForAction: null,
  contactForAction: null,
  clientDataFromFetch: [],

  //Contact-details and add-email dialogs
  contactDetailsFromFetch: [],
  emailsList: [],
  phoneNumbersList: [],
  addressesList: [],


  //Financials records
  financialRecordsFromFetch: [],
  billingDataFromFetch: [],
  fullWorkRecordsList: [],

  //Notes list for cases
  notesListFromFetch: [],
  clientNotesList: [],

  //Accesses
  allowedUsersList: [],
  eligibleUsersList: [],

  //Cases
  clientCaseDetails: null,
  groupedCases: [],
  allowedUsersForCase: [],
  eligibleUsersForCase: [],
  notesListForCases: [],
  deadlines: []


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
        return 1;
      } else if (b.eventDate > a.eventDate) {
        return -1;
      }
      return 0;
    });
    return sortedItems;
  },
  //Billing data
  billingDataList(state) {
    return state.billingDataFromFetch;
  },
  //Clients List
  clientData(state) {
    return state.clientDataFromFetch;
  },

  //Access allowed users
  allowedUsers(state) {
    return state.allowedUsersList
  },
  eligibleUsers(state) {
    return state.eligibleUsersList
  },
};

export const mutations = {

  setClientForAction(state, client) {
    console.warn('mutation done for setClientForAction', client);
    state.clientForAction = client;
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
  updateFullWorkRecordsList(state, {fullWorkRecordsList}) {
    console.warn('mutation done for updateFullWorkRecordsList', fullWorkRecordsList);
    state.fullWorkRecordsList = fullWorkRecordsList;
  },

  reset(state) {
    Object.assign(state, initState());
  },

  //Billing data
  updateBillingDataFromFetch(state, {billingDataFromFetch}) {
    console.warn('mutation done for updateBillingDataFromFetch', billingDataFromFetch);
    state.billingDataFromFetch = billingDataFromFetch;
  },

  //Client List
  updateClientDataFromFetch(state, {clientDataFromFetch}) {
    console.warn('mutation done for updateClientDataFromFetch', clientDataFromFetch);
    state.clientDataFromFetch = clientDataFromFetch;
  },

  //Notes list for cases
  updateNotesListForCases(state, {notesListForCases}) {
    state.notesListForCases = notesListForCases
  },
  updateNotesTitlesListFromFetch(state, {clientNotesList}) {
    state.clientNotesList = clientNotesList
  },
  //CLIENT ACCESS - GET ALLOWED & ELIGIBLE USERS
  updateAllowedUsersList(state, {allowedUsersList}) {
    state.allowedUsersList = allowedUsersList
  },
  updateEligibleUsersList(state, {eligibleUsersList}) {
    state.eligibleUsersList = eligibleUsersList
  },

  //CASES
  updateClientCaseDetails(state, {clientCaseDetails}) {
    state.clientCaseDetails = clientCaseDetails
  },

  updateGroupedCases(state, {groupedCases}) {
    state.groupedCases = groupedCases
  },
  updateAllowedUsersForCase(state, {allowedUsersForCase}) {
    state.allowedUsersForCase = allowedUsersForCase
  },
  updateEligibleUsersForCase(state, {eligibleUsersForCase}) {
    state.eligibleUsersForCase = eligibleUsersForCase
  },

  //CASE DEADLINES LIST
  updateCaseDeadlinesList(state, {deadlines}) {
    state.deadlines = deadlines
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
        });
        console.warn('Action from store: getFinancialRecordsFromFetch', financialRecordsFromFetch);
        commit('updateFinancialRecordsFromFetch', {financialRecordsFromFetch});

      }).catch((e) => {
        console.warn('error in getFinancialRecordsFromFetch', e);
      });
  },
  async getAllWorkRecordsOnFetch({commit}, {clientId}) {
    try {
      console.warn("Getting data from store - legal-app-client-store - getAllWorkRecordsOnFetch");
      let fullWorkRecordsList = await this.$axios.$get(`/api/legal-app-clients-work/client/${clientId}/work-records`);
      commit('updateFullWorkRecordsList', {fullWorkRecordsList});
    } catch (e) {
      console.warn('error in getAllWorkRecordsOnFetch', e);
    }
  },
  //Billing data
  async getBillingDataFromFetch({commit}) {
    try {
      let billingDataFromFetch = await this.$axios.$get('/api/legal-app-billing/list');
      commit('updateBillingDataFromFetch', {billingDataFromFetch});
      console.warn('Action from store = getBillingDataFromFetch', billingDataFromFetch);
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },
  //Clients list
  async getClientData({commit}, {clientId}) {
    try {
      let clientDataFromFetch = await this.$axios.$get(`/api/legal-app-clients/client/${clientId}`);
      commit('updateClientDataFromFetch', {clientDataFromFetch});
      console.warn('Action from store = clientBasicListFromFetch', clientDataFromFetch);
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  //Notes list for cases
  async getNotesListForCases({commit}, {caseId}) {
    try {
      let response = await this.$axios.$get(`/api/legal-app-cases-notes/case/${caseId}/list`)
      response.sort((a, b) => {
        const dateA = new Date(a.created)
        const dateB = new Date(b.created)
        return dateB - dateA
      });
      response.forEach(x => {
        x.caseCreatedDate = formatDateToMonth(x.created)
      });
      const notesListForCases = groupByKey(response, 'caseCreatedDate')
      commit('updateNotesListForCases', {notesListForCases});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  //CLIENT Notes - list of titles

  async getClientsNotes({commit}, clientId) {
    try {
      let response = await this.$axios.$get(`/api/legal-app-clients-notes/client/${clientId}/notes/titles-list`)
      response.sort((a, b) => {
        const dateA = new Date(a.created)
        const dateB = new Date(b.created)
        return dateB - dateA
      });
      response.forEach(x => {
        x.caseCreatedDate = formatDateToMonth(x.created)
      });
      const clientNotesList = groupByKey(response, 'caseCreatedDate')
      commit('updateNotesTitlesListFromFetch', {clientNotesList});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }

  },
  // CLIENT ACCESS - GET ALLOWED & ELIGIBLE USERS

  async getAllowedUsers({commit}, {clientId}) {
    try {
      let allowedUsersList = await this.$axios.$get(`/api/legal-app-client-access/client/${clientId}/allowed-users`)
      commit('updateAllowedUsersList', {allowedUsersList});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  async getEligibleUsersList({commit}, {clientId}) {
    try {
      let eligibleUsersList = await this.$axios.$get(`/api/legal-app-client-access/client/${clientId}/eligible-users`)
      commit('updateEligibleUsersList', {eligibleUsersList});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  //CASES
  async getCaseDetails({commit}, {caseId}) {
    try {
      let clientCaseDetails = await this.$axios.$get(`/api/legal-app-cases/case/${caseId}`)
      commit('updateClientCaseDetails', {clientCaseDetails});
      console.warn('case details:', clientCaseDetails)
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  async getListOfGroupedCases({commit}, {clientId}) {
    try {
      let response = await this.$axios.$get(`/api/legal-app-cases/client/${clientId}/cases`)
      response.sort((a, b) => {
        const dateA = new Date(a.created)
        const dateB = new Date(b.created)
        return dateB - dateA
      })
      const groupedCases = groupByKey(response, 'group')
      commit('updateGroupedCases', {groupedCases});
      console.warn('list of cases', groupedCases)
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },
  // CASE ACCESS - GET ALLOWED & ELIGIBLE USERS

  async getAllowedUsersForCase({commit}, {caseId}) {
    try {
      let allowedUsersForCase = await this.$axios.$get(`/api/legal-app-case-access/case/${caseId}/allowed-users`)
      commit('updateAllowedUsersForCase', {allowedUsersForCase});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  async getEligibleUsersForCase({commit}, {caseId}) {
    try {
      let eligibleUsersForCase = await this.$axios.$get(`/api/legal-app-case-access/case/${caseId}/eligible-users`)
      commit('updateEligibleUsersForCase', {eligibleUsersForCase});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  //CASE DEADLINES
  async getCaseDeadlines({commit}, {caseId, query}) {
    try {
      let deadlines = await this.$axios.$get(`/api/legal-app-cases/deadlines/case/${caseId}/list${query}`)
      deadlines.sort((a, b) => {
        const dateA = new Date(a.deadline)
        const dateB = new Date(b.deadline)
        return dateA - dateB
      })
      console.warn('deadlines', deadlines)
      commit('updateCaseDeadlinesList', {deadlines})

    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);

    }

  }

};
