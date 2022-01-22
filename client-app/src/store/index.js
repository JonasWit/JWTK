import {createStore} from 'vuex'
import {NavModel} from "@/models/general/portalDisplayModel";
import {NAV_TYPES} from "@/models/enums";
import {getCookie, readGateAPIAddress} from "@/appControl/controlFunctions";
import {COOKIES_NAMES} from "@/enums/portalEnums";
import snack from "@/store/snack";

export default createStore({
    state: {
        user: null,
        navType: new NavModel(NAV_TYPES.MAIN),
        idToken: getCookie(COOKIES_NAMES.token_id),
        apiGateBaseAddress: readGateAPIAddress()
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
        snack
    }
})
