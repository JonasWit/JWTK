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
  getUsers({commit}) {
    return this.$axios.$get("/api/portal-admin/users")
      .then((users) => {
        console.log('Downloaded Users', users);
        commit('updateUsersList', {users});
      })
      .catch(() => {
      });
  },
};
