﻿import {checkCookie, getCookie, setCookie} from "@/data/cookie-handlers";
import {COOKIE_NAME} from "@/data/enums";

const initState = () => ({
  legalAppTooltips: false,
  darkThemeStored: false
});

export const state = initState;

export const getters = {};

export const mutations = {
  reset(state) {
    Object.assign(state, initState());
  },
  setLegalAppTooltipsOption(state, {option}) {
    state.legalAppTooltips = option;
  },
  setLightTheme(state) {
    state.darkThemeStored = false;
  },
  setDarkTheme(state) {
    state.darkThemeStored = true;
  },
};

export const actions = {
  readStatusOfLegalAppTooltips({commit}) {
    if (checkCookie(COOKIE_NAME.LAPP_TIPS)) {
      if (getCookie(COOKIE_NAME.LAPP_TIPS) === 'true') {
        commit('setLegalAppTooltipsOption', {option: true});
      } else {
        commit('setLegalAppTooltipsOption', {option: false});
      }
    } else {
      commit('setLegalAppTooltipsOption', {option: false});
    }
  },
  turnOnLegalAppTooltips({commit}) {
    if (checkCookie(COOKIE_NAME.LAPP_TIPS)) {
      setCookie(COOKIE_NAME.LAPP_TIPS, "", 0);
      setCookie(COOKIE_NAME.LAPP_TIPS, "true", 365);
      commit('setLegalAppTooltipsOption', {option: true});
    } else {
      setCookie("lapp-tooltips", "true", 365);
      commit('setLegalAppTooltipsOption', {option: true});
    }
  },
  turnOffLegalAppTooltips({commit}) {
    if (checkCookie(COOKIE_NAME.LAPP_TIPS)) {
      setCookie(COOKIE_NAME.LAPP_TIPS, "", 0);
      setCookie(COOKIE_NAME.LAPP_TIPS, "false", 365);
      commit('setLegalAppTooltipsOption', {option: false});
    } else {
      setCookie(COOKIE_NAME.LAPP_TIPS, "false", 365);
      commit('setLegalAppTooltipsOption', {option: false});
    }
  },
};
