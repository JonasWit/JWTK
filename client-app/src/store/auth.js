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
            console.log("token check", state.userToken)
            return !!state.userToken;
        },
        getUserObject(){
            
        }
    },
    mutations: {
        resetCredentials(state) {
            state.userToken = null
            state.userObject = null
        },
        deserializeUserObject(state) {
            state.userToken = null
            state.userObject = null
        }
    },
    actions: {
        setIdToken({state}, token) {
            console.warn("Saving token: ", token)
            setLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN, token)
            state.userToken = token
        },
    },
}
