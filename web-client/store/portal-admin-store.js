const initState = () => ({
  users: [],
  legalAppAccessKeys: [],
  medicalAppAccessKeys: []
});

export const state = initState;

export const getters = {
  usersAvailableForKey(state) {
    return state.users.filter(mainUser =>
      !state.legalAppAccessKeys.some(key => key.users.some(user => mainUser.id === user.id)) &&
      !state.medicalAppAccessKeys.some(key => key.users.some(user => mainUser.id === user.id))
    );
  }
};

export const mutations = {
  updateUsersList(state, {users}) {
    state.users = users;
  },
  updateLegalAppAccessKeysList(state, {keys}) {
    state.legalAppAccessKeys = keys;
  },
  updateMedicalAppAccessKeysList(state, {keys}) {
    state.medicalAppAccessKeys = keys;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  async getUsers({commit}) {
    try {
      let users = await this.$axios.$get("/api/portal-admin/user-admin/users");
      commit('updateUsersList', {users});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },
  async getLegalAppAccessKeys({commit}) {
    try {
      let keys = await this.$axios.$get("/api/portal-admin/key-admin/legal-app/access-keys");
      keys.forEach(x => x.keyType = "legal-app");
      commit('updateLegalAppAccessKeysList', {keys});
    } catch (error) {
      this.$notifier.showErrorMessage(error.response.data);
    }
  },
  async getMedicalAppAccessKeys({commit}) {
    try {
    } catch (error) {

    }
  },
};
