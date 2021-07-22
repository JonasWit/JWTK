import {formatDateToMonth} from "@/data/date-extensions";
import {groupByKey} from "@/data/functions";

const initState = () => ({})
export const state = initState;
export const getters = {}
export const mutations = {}
export const actions = {
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


}
