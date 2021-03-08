const initState = () => ({
  users: [],
  accessKeys: []
});

export const state = initState;

export const getters = {};

export const mutations = {
  updateUsersList(state, {users}) {
    state.users = users;
  },
  updateAccessKeysList(state, {keys}) {
    state.accessKeys = keys;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  getUsers({commit}) {
    return this.$axios.$get("/api/portal-admin/users")
      .then((users) => {
        commit('updateUsersList', {users});
      })
      .catch(() => {
      });
  },
  getAccessKeys({commit}) {
    return this.$axios.$get("/api/portal-admin/access-keys")
      .then((keys) => {
        commit('updateAccessKeysList', {keys});
      })
      .catch(() => {
      });
  },
};
