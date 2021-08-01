﻿import {checkCookie, setCookie} from "@/data/cookie-handlers";
import {COOKIE_NAME} from "@/data/enums";

const initState = () => ({
  legalAppTooltips: false
});

export const state = initState;

export const getters = {};

export const mutations = {
  reset(state) {
    Object.assign(state, initState());
  },
  setLegalAppTooltipsOption(state, {option}) {
    state.legalAppTooltips = option;
  }
};

export const actions = {
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
