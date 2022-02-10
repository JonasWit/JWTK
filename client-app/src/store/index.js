import {createStore} from 'vuex'
import {NavModel} from "@/models/general/portalDisplayModel";
import {NAV_TYPES} from "@/models/enums";
import {
    readDarkMode,
    readGateAPIAddress, readGDPRConsent,
    setLocalStoreItem
} from "@/appControl/controlFunctions";
import snack from "@/store/snack";
import auth from "@/store/auth";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";

export default createStore({
    state: {
        navType: new NavModel(NAV_TYPES.MAIN),
        apiGateBaseAddress: readGateAPIAddress(),
        darkMode: readDarkMode(),
        gdpr: readGDPRConsent()
    },
    getters: {
        isDark(state) {
            return state.darkMode
        },
        gdprAccepted(state) {
            return state.gdpr
        }
    },
    mutations: {
        toggleDarkTheme(state, option) {
            state.darkMode = option
        },
        toggleGDPR(state, option) {
            state.gdpr = option
        },
    },
    actions: {
        setDark({commit, state}) {
            if (state.darkMode) {
                setLocalStoreItem(LOCAL_STORE_NAMES.DARK_THEME, false)
                commit('toggleDarkTheme', false)
            } else {
                setLocalStoreItem(LOCAL_STORE_NAMES.DARK_THEME, true)
                commit('toggleDarkTheme', true)
            }
        },
        acceptGDPR({commit}) {
            setLocalStoreItem(LOCAL_STORE_NAMES.GDPR_CONSENT, true)
            commit('toggleGDPR', true)
        }
    },
    modules: {
        snack,
        auth
    }
})
