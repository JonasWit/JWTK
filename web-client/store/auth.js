﻿import {ROLES} from "@/data/enums";
import {getGDPRConsent} from "~/data/cookie-handlers";

const initState = () => ({
  profile: null,
  relatedUsers: [],
  darkThemeStored: false
});

export const state = initState;

export const APP_ACCESS = {
  LEGAL_APP: "LegalApp",
};

export const getters = {
  userRole(state) {
    if (state.profile.role === ROLES.INVITED) {
      return "Zaproszony";
    } else if (state.profile.role === ROLES.CLIENT) {
      return "Klient";
    } else if (state.profile.role === ROLES.CLIENT_ADMIN) {
      return "Klient z uprawnieniemiami Administratora";
    } else if (state.profile.role === ROLES.PORTAL_ADMIN) {
      return "Administarator Portalu";
    } else {
      return "Brak Roli!";
    }
  },
  authenticated: state => state.profile != null,
  invited: (state, getters) => getters.authenticated && state.profile.role === ROLES.INVITED,
  client: (state, getters) => getters.authenticated && state.profile.role === ROLES.CLIENT,
  clientAdmin: (state, getters) => getters.authenticated && (state.profile.role === ROLES.CLIENT_ADMIN || getters.portalAdmin),
  portalAdmin: (state, getters) => getters.authenticated && state.profile.role === ROLES.PORTAL_ADMIN,
  legalAppAllowed: (state, getters) => getters.authenticated && state.profile.legalAppAllowed === true,
};

export const mutations = {
  saveProfile(state, {profile}) {
    state.profile = profile;
  },
  saveRelatedUsers(state, {users}) {
    state.relatedUsers = users;
  },
  setLightTheme(state) {
    state.darkThemeStored = false;
  },
  setDarkTheme(state) {
    state.darkThemeStored = true;
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  async initialize({commit}) {
    try {
      let profile = await this.$axios.$get('/api/users/me');
      console.warn('User Profile: ', profile);
      commit('saveProfile', {profile});

      let users = await this.$axios.$get('/api/legal-app-admin/general/all-related-users');
      console.warn('Related Users: ', users);
      commit('saveRelatedUsers', {users});

    } catch (error) {
      console.error("Not authorized");
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
    commit('reset');
    window.location = this.$config.auth.deletePath;
  }
};
