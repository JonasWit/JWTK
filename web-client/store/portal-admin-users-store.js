const initState = () => ({
  users: [],
});

export const state = initState;

export const getters = {};

export const mutations = {
  updateUsersList(state, {users}) {
    state.users = users;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  async getUsers({commit}) {
    try {
      let response = this.$axios.$get("/api/portal-admin/user-admin/users");
      console.warn('getUsers from store', response);
      commit('updateUsersList', {response});
    } catch (error) {

    }
  },
};
