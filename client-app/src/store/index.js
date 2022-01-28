import {createStore} from 'vuex'
import {NavModel} from "@/models/general/portalDisplayModel";
import {NAV_TYPES} from "@/models/enums";
import {readGateAPIAddress} from "@/appControl/controlFunctions";
import snack from "@/store/snack";
import auth from "@/store/auth";

export default createStore({
    state: {
        navType: new NavModel(NAV_TYPES.MAIN),
        apiGateBaseAddress: readGateAPIAddress(),
    },
    getters: {

    },
    mutations: {
        setApiGateBaseAddress(state, {address}) {
            state.legalAppTooltips = address;
        },
    },
    actions: {
        readStatusOfLegalAppTooltips({commit}) {
            commit('setLegalAppTooltipsOption', {option: true});
        },
    },
    modules: {
        snack,
        auth
    }
})
