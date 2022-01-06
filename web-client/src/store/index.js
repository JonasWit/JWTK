import { createStore } from 'vuex'
import {NavModel} from "@/models/general/portalDisplayModel";

export default createStore({
  state: {
    user: null,
    navType: new NavModel("main")
  },
  mutations: {
  },
  actions: {
  },
  modules: {
  }
})
