import {setLocalStoreItem} from "@/appControl/controlFunctions";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";

export default {
    namespaced: true,
    state: {
        userToken: null,
        userObject: null,
    },
    getters: {
        isAuthorized(state){
            return !!state.userToken;
        },
        getUserObject(){
            
        }
    },
    mutations: {
        resetCredentials(state) {
            state.userToken = null
            state.userObject = null
        }
    },
    actions: {
        readStatusOfLegalAppTooltips({commit}) {
            commit('setLegalAppTooltipsOption', {option: true});
        },
        setIdToken({state}, token) {
            console.warn("Saving token: ", token)
            setLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN, token)
            state.userToken = token
        },
    },
}
