import {ROLES} from "@/data/enums";
import {getGDPRConsent} from "~/data/cookie-handlers";
import {
  getCurrentUser,
  getLegalAppRelatedUsers,
  getMedicalAppRelatedUsers,
  getRestaurantAppRelatedUsers
} from "@/data/endpoints/admin/admin-panel-endpoints";

const initState = () => ({
  profile: null,
  legalRelatedUsers: [],
  medicalRelatedUsers: [],
  restaurantRelatedUsers: [],
});

export const state = initState;

export const APP_ACCESS = {
  LEGAL_APP: "LegalApp",
};

export const getters = {
  otherLegalRelatedUsers(state) {
    return state.legalRelatedUsers.filter(x => x.id !== state.profile.id);
  },
  otherMedicalRelatedUsers(state) {
    return state.medicalRelatedUsers.filter(x => x.id !== state.profile.id);
  },
  otherRestaurantRelatedUsers(state) {
    return state.restaurantRelatedUsers.filter(x => x.id !== state.profile.id);
  },
  userRole(state) {
    if (state.profile.role === ROLES.INVITED) {
      return "Zaproszony";
    } else if (state.profile.role === ROLES.USER) {
      return "Użytkownik";
    } else if (state.profile.role === ROLES.USER_ADMIN) {
      return "Użytkownik Administrator";
    } else if (state.profile.role === ROLES.PORTAL_ADMIN) {
      return "Administarator Portalu";
    } else {
      return "Brak Roli!";
    }
  },
  legalAppKeyAvailable: state => !!state.profile.legalAppDataAccessKey,
  legalAppAllowed: (state, getters) => getters.authenticated && state.profile.legalAppAllowed === true,
  medicalAppKeyAvailable: state => !!state.profile.medicalAppDataAccessKey,
  medicalAppAllowed: (state, getters) => getters.authenticated && state.profile.medicalAppAllowed === true,
  restaurantAppKeyAvailable: state => !!state.profile.restaurantAppDataAccessKey,
  restaurantAppAllowed: (state, getters) => getters.authenticated && state.profile.restaurantAppAllowed === true,
  authenticated: state => state.profile != null,
  invited: (state, getters) => getters.authenticated && state.profile.role === ROLES.INVITED,
  user: (state, getters) => getters.authenticated && state.profile.role === ROLES.USER,
  userAdmin: (state, getters) => getters.authenticated && (state.profile.role === ROLES.USER_ADMIN || getters.portalAdmin),
  portalAdmin: (state, getters) => getters.authenticated && state.profile.role === ROLES.PORTAL_ADMIN,
};

export const mutations = {
  saveProfile(state, {profile}) {
    state.profile = profile;
  },
  saveLegalAppRelatedUsers(state, {users}) {
    state.legalRelatedUsers = users;
  },
  saveMedicalAppRelatedUsers(state, {users}) {
    state.medicalRelatedUsers = users;
  },
  saveRestaurantAppRelatedUsers(state, {users}) {
    state.restaurantRelatedUsers = users;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  async initialize({getters, dispatch}) {
    try {
      await dispatch('reloadProfile');
    } catch (error) {
      console.log("Not authorized");
    }
    if (getters.userAdmin) {
      try {
        await dispatch('reloadLegalAppRelatedUsers');
        await dispatch('reloadMedicalAppRelatedUsers');
        await dispatch('reloadRestaurantAppRelatedUsers');
      } catch (error) {
        console.log("No related users");
      }
    } else {
      console.log("Related users not visible");
    }
  },
  async reloadProfile({commit}) {
    try {
      let profile = await this.$axios.$get(getCurrentUser());
      console.log("User Profile: ", profile);
      commit('saveProfile', {profile});
    } catch (error) {
      console.log("Not authorized");
    }
  },
  async reloadLegalAppRelatedUsers({commit}) {
    try {
      let users = await this.$axios.$get(getLegalAppRelatedUsers());
      commit('saveLegalAppRelatedUsers', {users});
    } catch (error) {
      console.log("Not authorized");
    }
  },
  async reloadMedicalAppRelatedUsers({commit}) {
    try {
      let users = await this.$axios.$get(getMedicalAppRelatedUsers());
      commit('saveMedicalAppRelatedUsers', {users});
    } catch (error) {
      console.log("Not authorized");
    }
  },
  async reloadRestaurantAppRelatedUsers({commit}) {
    try {
      let users = await this.$axios.$get(getRestaurantAppRelatedUsers());
      commit('saveRestaurantAppRelatedUsers', {users});
    } catch (error) {
      console.log("Not authorized");
    }
  },
  login() {
    if (process.server) return;
    if (getGDPRConsent() === false) return;

    const returnUrl = encodeURIComponent(location.href);
    window.location = `${this.$config.auth.loginPath}?returnUrl=${returnUrl}`;
  },
  changePassword() {
    if (process.server) return;
    if (getGDPRConsent() === false) return;

    const returnUrl = encodeURIComponent(location.href);
    window.location = `${this.$config.auth.changePassPath}?returnUrl=${returnUrl}`;
  },
  logout({commit}) {
    if (process.server) return;

    commit('reset');
    window.location = this.$config.auth.logoutPath;
  },
  deleteAccount({commit}) {
    if (process.server) return;
    if (getGDPRConsent() === false) return;

    commit('reset');
    window.location = this.$config.auth.deletePath;
  },
  async deleteLegalAppKey({dispatch}) {
    if (process.server) return;
    if (getGDPRConsent() === false) return;

    try {
      await this.$axios.$get(this.$config.auth.deleteLegalAppKeyPath);
      this.$notifier.showSuccessMessage("Dane zostały usunięte!");
    } catch (error) {
      this.$notifier.showErrorMessage("Błąd podczas usuwania danych!");
      console.error('Data delete error!', error);
    } finally {
      dispatch('reloadProfile');
    }
  },
  async deleteMedicalAppKey({dispatch}) {
    if (process.server) return;
    if (getGDPRConsent() === false) return;

    try {
      await this.$axios.$get(this.$config.auth.deleteMedicalAppKeyPath);
      this.$notifier.showSuccessMessage("Dane zostały usunięte!");
    } catch (error) {
      this.$notifier.showErrorMessage("Błąd podczas usuwania danych!");
      console.error('Data delete error!', error);
    } finally {
      dispatch('reloadProfile');
    }
  },
  async deleteRestaurantAppKey({dispatch}) {
    if (process.server) return;
    if (getGDPRConsent() === false) return;

    try {
      await this.$axios.$get(this.$config.auth.deleteRestaurantAppKeyPath);
      this.$notifier.showSuccessMessage("Dane zostały usunięte!");
    } catch (error) {
      this.$notifier.showErrorMessage("Błąd podczas usuwania danych!");
      console.error('Data delete error!', error);
    } finally {
      dispatch('reloadProfile');
    }
  }
};
