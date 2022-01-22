export default {
    namespaced: true,

    state: {
        snack: null
    },

    getters: {
        getSanck(state) {
            return state.snack
        }
    },

    mutations: {
        setSnack(state, text) {
            state.snack = text
        }
    },

    actions: {
        snack({commit}, text) {
            commit('setSnack', text)
        }
    }
}