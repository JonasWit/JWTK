import {createStore} from 'vuex'
import {NavModel} from "@/models/general/portalDisplayModel";
import {NAV_TYPES} from "@/models/enums";
import {readGateAPIAddress, setLocalStoreItem} from "@/appControl/controlFunctions";
import snack from "@/store/snack";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";

export default createStore({
    state: {
        user: null,
        navType: new NavModel(NAV_TYPES.MAIN),
        apiGateBaseAddress: readGateAPIAddress(),
        idToken: null
    },
    getters: {
        getUser(){
     
     
            
        }
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
        setIdToken({state}, token) {
            console.warn("Saving token: ", token)
            setLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN, token)
            state.idToken = token
            
            
            
            
        },
    },
    modules: {
        snack
    }
})
