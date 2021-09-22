﻿import {ROLES} from "@/data/enums";
import {getGDPRConsent} from "~/data/cookie-handlers";

const initState = () => ({
  profile: null,
  relatedUsers: [],
});

export const state = initState;

export const APP_ACCESS = {
  LEGAL_APP: "LegalApp",
};

export const getters = {
  otherRelatedUsers(state) {
    return state.relatedUsers.filter(x => x.id !== state.profile.id);
  },
  userRole(state) {
    if (state.profile.role === ROLES.INVITED) {
      return "Zaproszony";
    } else if (state.profile.role === ROLES.USER) {
      return "Klient";
    } else if (state.profile.role === ROLES.USER_ADMIN) {
      return "Klient z uprawnieniemiami Administratora";
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
  saveRelatedUsers(state, {users}) {
    state.relatedUsers = users;
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
      console.warn("Not authorized");
    }

    if (getters.userAdmin) {
      try {
        await dispatch('reloadRelatedUsers');
      } catch (error) {
        console.warn("No related users");
      }
    } else {
      console.warn("Related users not visible");
    }
  },
  async reloadProfile({commit}) {
    try {
      let profile = await this.$axios.$get('/api/users/me');
      console.warn("User Profile: ", profile);
      commit('saveProfile', {profile});
    } catch (error) {
      console.warn("Not authorized");
    }
  },
  async reloadRelatedUsers({commit}) {
    try {
      let users = await this.$axios.$get('/api/legal-app-admin/general/all-related-users');
      commit('saveRelatedUsers', {users});
    } catch (error) {
      console.warn("Not authorized");
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
      await this.$axios.$get(this.$config.auth.deleteLeglaAppKeyPath);
      this.$notifier.showSuccessMessage("Dane zostały usunięte!");
    } catch (error) {
      this.$notifier.showErrorMessage("Błąd podczas usuwania danych!");
      console.error('Data delete error!', error);
    } finally {
      dispatch('reloadProfile');
    }
  }
};
