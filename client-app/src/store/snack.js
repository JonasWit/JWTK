import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";

let defaults = {
    text: null, 
    textColor: SNACK_TEXT.WHITE, 
    backColor: SNACK_BACKGROUNDS.DEFAULT
}

export default {
    namespaced: true,

    state: {
        snack: defaults
    },
    getters: {
        getSanck(state) {
            return state.snack
        }
    },
    mutations: {
        setSnack(state, options) {
            state.snack.text = options.text
            state.snack.textColor = options.textColor
            state.snack.backColor = options.backColor
        }
    },
    actions: {
        snack({commit}, options) {
            console.warn('Showing Snack')
            options = { ...defaults, ...options }
            commit('setSnack', options)
            setTimeout(() => {
                commit('setSnack', { ...defaults, text: null })
            }, 2000)
        }
    }
}
