const initState = () => ({
  users: [],
  accessKeys: [],
  logs: []
});

export const state = initState;

export const getters = {
  accessKeys(state) {
    return state.accessKeys;
  }
};

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
        console.log('get users called');
        commit('updateUsersList', {users});
      })
      .catch(() => {
      });
  },
  getAccessKeys({commit}) {
    return this.$axios.$get("/api/portal-admin/key-admin/access-keys")
      .then((keys) => {
        console.log('get access keys called');
        commit('updateAccessKeysList', {keys});
      })
      .catch(() => {
      });
  },
};
