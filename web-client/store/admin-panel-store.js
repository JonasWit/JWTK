const initState = () => ({
  users: [],
  accessKeys: [],
  logs: []
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
    return this.$axios.$get("/api/portal-admin/user-admin/users")
      .then((users) => {
        commit('updateUsersList', {users});
      })
      .catch(() => {
      });
  },
  getAccessKeys({commit}) {
    return this.$axios.$get("/api/portal-admin/key-admin/access-keys")
      .then((keys) => {
        commit('updateAccessKeysList', {keys});
      })
      .catch(() => {
      });
  },
};
