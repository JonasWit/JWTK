import {createStore} from 'vuex'
import {NavModel} from "@/models/general/portalDisplayModel";
import {NAV_TYPES} from "@/models/enums";
import {
    readDarkMode,
    readGateAPIAddress,
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
    },
    getters: {
        isDark(state) {
            return state.darkMode
        }
    },
    mutations: {
        toggleDarkTheme(state, option) {
            state.darkMode = option
        }
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
        }
    },
    modules: {
        snack,
        auth
    }
})
