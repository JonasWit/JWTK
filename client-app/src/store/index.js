import {createStore} from 'vuex'
import {NavModel} from "@/models/general/portalDisplayModel";
import {NAV_TYPES} from "@/models/enums";
import {
    hideNavBar,
    readDarkMode,
    readGateAPIAddress, readGDPRConsent,
    setLocalStoreItem, toggleNavBar
} from "@/appControl/controlFunctions";
import snack from "@/store/snack";
import auth from "@/store/auth";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";
import gastronomy from "@/store/gastronomy";

export default createStore({
    state: {
        navType: new NavModel(NAV_TYPES.MAIN),
        navBarVisible: false,
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
        },
        navBarVisibility(state) {
            return state.navBarVisible
        }
    },
    mutations: {
        toggleDarkTheme(state, option) {
            state.darkMode = option
        },
        toggleGDPR(state, option) {
            state.gdpr = option
        },
        toggleNavBar(state) {
            state.navBarVisible = toggleNavBar();
        },
        hideNavBar(state) {
            hideNavBar()
            state.navBarVisible = false
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
        auth,
        gastronomy
    }
})
