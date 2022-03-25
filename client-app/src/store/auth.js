import {deserializeUserObject, getLocalStoreItem, setLocalStoreItem} from "@/appControl/controlFunctions";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";

export default {
    namespaced: true,
    state: {
        userToken: getLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN),
        userObject: deserializeUserObject(),
    },
    getters: {
        isAuthorized(state){
            return !!(state.userToken && state.userObject.expire > Date.now());
        },
    },
    mutations: {
        resetCredentials(state) {
            state.userToken = null
            state.userObject = null
        },
        setUserData(state, { token, userObject}){
            state.userToken = token
            state.userObject = userObject
        }
    },
    actions: {
        setIdToken({commit}, token) {
            console.warn("Saving token: ", token)
            setLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN, token)
            commit('setUserData', { token: token, userObject: deserializeUserObject() })
        },
    },
}
