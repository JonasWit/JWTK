import {amountGross, amountNet, groupByKey, handleError, rateNet, vatAmount, vatRate} from "@/data/functions";
import {formatDateToMonth} from "@/data/date-extensions";
import {getClientsBasicList, getContacts} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {getAllDeadlinesFromTo, getRemindersFromTo} from "@/data/endpoints/legal-app/legal-app-reminders-endpoints";

const initState = () => ({
  //Clients
  clientDataFromFetch: [],
  basicClientsListFromFetch: [],

  //Contact-details and add-email dialogs
  contactDetailsFromFetch: null,
  contactItemsFromFetch: [],

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
  fullListOfCases: [],
  allowedUsersForCase: [],
  eligibleUsersForCase: [],
  notesListForCases: [],
  deadlines: [],

  //Reminders

  newEvents: []


});

export const state = initState;

export const getters = {
  //Cases related
  getCasesGroups(state) {
    if (state.fullListOfCases) {
      return [...new Set(state.fullListOfCases.map(item => item.group))];
    }
    return [];
  },
  //Basic Clients List
  basicClientsInfo(state) {
    return state.basicClientsListFromFetch;
  },
  //Financials records
  workRecordsList(state) {
    return state.financialRecordsFromFetch;
  },
  // calculatedFinancialRecords(state) {
  //   return state.financialRecordsFromFetch.map(x => ({
  //     ...x,
  //     invoiceVatAmount: vatAmount(x.amount, x.vat),
  //     invoiceDecimalVat: vatRate(x.vat),
  //     invoiceRateNet: rateNet(x.rate, x.vat),
  //     invoiceAmountNet: amountNet(x.amount, x.vat),
  //   }));
  // },
  sortedFinancialRecords(state) {
    return [...state.financialRecordsFromFetch].sort((a, b) => {
      if (a.eventDate > b.eventDate) {
        return 1;
      } else if (b.eventDate > a.eventDate) {
        return -1;
      }
      return 0;
    });
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
    return state.allowedUsersList;
  },
  eligibleUsers(state) {
    return state.eligibleUsersList;
  },
  notificationsCount(state) {
    return state.newEvents.length;
  }
};

export const mutations = {
  //CLIENTS

  updateBasicClientsListFromFetch(state, {basicClientsListFromFetch}) {
    state.basicClientsListFromFetch = basicClientsListFromFetch;
  },

  //CONTACT ITEMS
  updateContactItemsList(state, {contactItemsFromFetch}) {
    state.contactItemsFromFetch = contactItemsFromFetch;
  },

  //Contact-details and add-email dialogs
  updateContactDetailsList(state, {contactDetailsFromFetch}) {
    state.contactDetailsFromFetch = contactDetailsFromFetch;
  },
//Financials records
  updateFinancialRecordsFromFetch(state, {data}) {
    console.warn('mutation done for updateFinancialRecordsFromFetch', data);
    state.financialRecordsFromFetch = data;
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
    state.clientDataFromFetch = clientDataFromFetch;
  },

  //Notes list for cases
  updateNotesListForCases(state, {notesListForCases}) {
    state.notesListForCases = notesListForCases;
  },
  updateNotesTitlesListFromFetch(state, {clientNotesList}) {
    state.clientNotesList = clientNotesList;
  },
  //CLIENT ACCESS - GET ALLOWED & ELIGIBLE USERS
  updateAllowedUsersList(state, {allowedUsersList}) {
    state.allowedUsersList = allowedUsersList;
  },
  updateEligibleUsersList(state, {eligibleUsersList}) {
    state.eligibleUsersList = eligibleUsersList;
  },

  //CASES
  updateClientCaseDetails(state, {clientCaseDetails}) {
    state.clientCaseDetails = clientCaseDetails;
  },

  updateGroupedCases(state, {groupedCases}) {
    state.groupedCases = groupedCases;
  },

  updateFullListOfCases(state, {fullListOfCases}) {
    state.fullListOfCases = fullListOfCases;
  },

  updateAllowedUsersForCase(state, {allowedUsersForCase}) {
    state.allowedUsersForCase = allowedUsersForCase;
  },
  updateEligibleUsersForCase(state, {eligibleUsersForCase}) {
    state.eligibleUsersForCase = eligibleUsersForCase;
  },

  //CASE DEADLINES LIST
  updateCaseDeadlinesList(state, {deadlines}) {
    state.deadlines = deadlines;
  },

  updateNewEventsForNotifications(state, {newEvents}) {
    state.newEvents = newEvents;
  }
};

export const actions = {
  //Get Basic list of clients

  async getBasicListOfClients({commit}) {
    try {
      let basicClientsListFromFetch = await this.$axios.$get(getClientsBasicList());
      commit('updateBasicClientsListFromFetch', {basicClientsListFromFetch});
      console.log('Lista klientow ze stora', basicClientsListFromFetch);
    } catch (error) {
      handleError(error);
    }
  },

  //Contact-details and add-email dialogs
  async getContactDetailsFromFetch({commit}, {clientId, contactId}) {
    try {
      let contactDetailsFromFetch = await this.$axios.$get(`/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}`);
      commit('updateContactDetailsList', {contactDetailsFromFetch});
    } catch (error) {
      handleError(error);
    }

  },
//Financials records
  async getFinancialRecordsFromFetch({commit}, {clientId, query}) {
    try {
      const data = await this.$axios.$get(`/api/legal-app-clients-work/client/${clientId}/work-records${query}`);
      data.sort((a, b) => {
        const dateA = new Date(a.created);
        const dateB = new Date(b.created);
        return dateB - dateA;
      });
      data.forEach(x => {
        x.invoiceAmountGross = amountGross(x.hours, x.minutes, x.rate);
        x.invoiceVatAmount = vatAmount(x.invoiceAmountGross, x.vat);
        x.invoiceDecimalVat = vatRate(x.vat);
        x.invoiceRateNet = rateNet(x.rate, x.vat);
        x.invoiceAmountNet = amountNet(x.amount, x.vat);

      });
      console.warn('Action from store: getFinancialRecordsFromFetch', data);
      commit('updateFinancialRecordsFromFetch', {data});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
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
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  //Notes list for cases
  async getNotesListForCases({commit}, {caseId}) {
    try {
      let response = await this.$axios.$get(`/api/legal-app-cases-notes/case/${caseId}/list`);
      response.sort((a, b) => {
        const dateA = new Date(a.created);
        const dateB = new Date(b.created);
        return dateB - dateA;
      });
      response.forEach(x => {
        x.caseCreatedDate = formatDateToMonth(x.created);
      });
      const notesListForCases = groupByKey(response, 'caseCreatedDate');
      commit('updateNotesListForCases', {notesListForCases});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  //CLIENT Notes - list of titles

  async getClientsNotes({commit}, clientId) {
    try {
      let response = await this.$axios.$get(`/api/legal-app-clients-notes/client/${clientId}/notes/titles-list`);
      response.sort((a, b) => {
        const dateA = new Date(a.created);
        const dateB = new Date(b.created);
        return dateB - dateA;
      });
      response.forEach(x => {
        x.caseCreatedDate = formatDateToMonth(x.created);
      });
      const clientNotesList = groupByKey(response, 'caseCreatedDate');
      // const clientNotesList = response;
      commit('updateNotesTitlesListFromFetch', {clientNotesList});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }

  },
  // CLIENT ACCESS - GET ALLOWED & ELIGIBLE USERS

  async getAllowedUsers({commit}, {clientId}) {
    try {
      let allowedUsersList = await this.$axios.$get(`/api/legal-app-client-access/client/${clientId}/allowed-users`);
      commit('updateAllowedUsersList', {allowedUsersList});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  async getEligibleUsersList({commit}, {clientId}) {
    try {
      let eligibleUsersList = await this.$axios.$get(`/api/legal-app-client-access/client/${clientId}/eligible-users`);
      commit('updateEligibleUsersList', {eligibleUsersList});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  //CASES
  async getCaseDetails({commit}, {caseId}) {
    try {
      let clientCaseDetails = await this.$axios.$get(`/api/legal-app-cases/case/${caseId}`);
      commit('updateClientCaseDetails', {clientCaseDetails});
      console.warn('case details - action from store:', clientCaseDetails);
    } catch (error) {
      handleError(error);
    }
  },

  async getFullListOfCases({commit}, {clientId}) {
    try {
      let fullListOfCases = await this.$axios.$get(`/api/legal-app-cases/client/${clientId}/cases`);
      fullListOfCases.sort((a, b) => {
        const dateA = new Date(a.created);
        const dateB = new Date(b.created);
        return dateB - dateA;
      });
      commit('updateFullListOfCases', {fullListOfCases});
    } catch (error) {
      handleError(error);
    }
  },

  // CASE ACCESS - GET ALLOWED & ELIGIBLE USERS

  async getAllowedUsersForCase({commit}, {caseId}) {
    try {
      let allowedUsersForCase = await this.$axios.$get(`/api/legal-app-case-access/case/${caseId}/allowed-users`);
      commit('updateAllowedUsersForCase', {allowedUsersForCase});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  async getEligibleUsersForCase({commit}, {caseId}) {
    try {
      let eligibleUsersForCase = await this.$axios.$get(`/api/legal-app-case-access/case/${caseId}/eligible-users`);
      commit('updateEligibleUsersForCase', {eligibleUsersForCase});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  //CASE DEADLINES
  async getCaseDeadlines({commit}, {caseId, query}) {
    try {
      let deadlines = await this.$axios.$get(`/api/legal-app-cases/deadlines/case/${caseId}/list${query}`);
      deadlines.sort((a, b) => {
        const dateA = new Date(a.deadline);
        const dateB = new Date(b.deadline);
        return dateA - dateB;
      });
      commit('updateCaseDeadlinesList', {deadlines});
    } catch (e) {
      console.error('error', e);
    }
  },
  //CONTACT DETAILS

  async getContactsList({commit}, {clientId}) {
    try {
      let contactItemsFromFetch = await this.$axios.$get(getContacts(clientId));
      contactItemsFromFetch.sort((a, b) => {
        let contactA = new Date(a.created);
        let contactB = new Date(b.created);
        return contactB - contactA;
      });
      commit('updateContactItemsList', {contactItemsFromFetch});
      this.contactList = this.contactItemsFromFetch;

    } catch (error) {
      console.error('creating contact error', error);
      this.$notifier.showErrorMessage(error.response.data);
    }
  },

  async getEventsForNotifications({commit}, {dates}) {
    try {
      let deadlines = await this.$axios.$get(getAllDeadlinesFromTo(dates));
      let remindersList = await this.$axios.$get(getRemindersFromTo(dates));
      let newEvents = [];
      remindersList.forEach(x => {
        newEvents.push({
          name: x.name,
          details: x.message,
          deadline: x.start,
          category: x.reminderCategory,
          id: x.id,
          case: 'none',
          client: 'none',
          type: 'event',

        });
      });
      deadlines.forEach(x => {
        newEvents.push({
          name: x.case.name,
          details: x.message,
          deadline: x.deadline,
          category: 4,
          id: x.id,
          case: x.case.id,
          client: x.case.legalAppClientId,
          type: 'deadline',
        });
      });
      let ordering = {}, // map for efficient lookup of sortIndex
        sortOrder = ['deadline', 'event'];
      for (let i = 0; i < sortOrder.length; i++)
        ordering[sortOrder[i]] = i;
      newEvents.sort(function (a, b) {
        return (ordering[a.type] - ordering[b.type]) || a.deadline.localeCompare(b.deadline);
      });

      commit('updateNewEventsForNotifications', {newEvents});
    } catch (e) {
      console.log('error', e);
    }
  },

};
