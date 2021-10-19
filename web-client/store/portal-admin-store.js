import {
  getLegalAppKeys,
  getMedicalAppKeys,
  getRestaurantAppKeys,
  getUsers
} from "@/data/endpoints/admin/admin-panel-endpoints";

const initState = () => ({
  users: [],
  legalAppAccessKeys: [],
  medicalAppAccessKeys: [],
  restaurantAppAccessKeys: [],
});

export const state = initState;

export const getters = {
  usersAvailableForLegalKey(state) {
    return state.users.filter(mainUser =>
      !state.legalAppAccessKeys.some(key => key.users.some(user => mainUser.id === user.id))
    );
  },
  usersAvailableForMedicalKey(state) {
    return state.users.filter(mainUser =>
      !state.medicalAppAccessKeys.some(key => key.users.some(user => mainUser.id === user.id))
    );
  },
  usersAvailableForRestaurantKey(state) {
    return state.users.filter(mainUser =>
      !state.restaurantAppAccessKeys.some(key => key.users.some(user => mainUser.id === user.id))
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
  updateRestaurantAppAccessKeysList(state, {keys}) {
    state.restaurantAppAccessKeys = keys;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  async getUsers({commit}) {
    try {
      let users = await this.$axios.$get(getUsers());
      console.warn("Users: ", users);
      commit('updateUsersList', {users});
    } catch (error) {
      console.error("getUsers - Error", error);
    }
  },
  async getLegalAppAccessKeys({commit}) {
    try {
      let keys = await this.$axios.$get(getLegalAppKeys());
      keys.forEach(x => x.keyType = "legal-app");
      commit('updateLegalAppAccessKeysList', {keys});
    } catch (error) {
      console.error("getLegalAppAccessKeys - Error", error);
    }
  },
  async getMedicalAppAccessKeys({commit}) {
    try {
      let keys = await this.$axios.$get(getMedicalAppKeys());
      keys.forEach(x => x.keyType = "medical-app");
      commit('updateMedicalAppAccessKeysList', {keys});
    } catch (error) {
      console.error("getMedicalAppAccessKeys - Error", error);
    }
  },
  async getRestaurantAppAccessKeys({commit}) {
    try {
      let keys = await this.$axios.$get(getRestaurantAppKeys());
      keys.forEach(x => x.keyType = "restaurant-app");
      commit('updateRestaurantAppAccessKeysList', {keys});
    } catch (error) {
      console.error("getRestaurantAppAccessKeys - Error", error);
    }
  },
};
