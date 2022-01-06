import { createStore } from 'vuex'
import {NavModel} from "@/models/general/portalDisplayModel";
import {NAV_TYPES} from "@/models/enums";

export default createStore({
  state: {
    user: null,
    navType: new NavModel(NAV_TYPES.MAIN)
  },
  mutations: {
  },
  actions: {
  },
  modules: {
  }
})
