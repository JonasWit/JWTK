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
    const cookieRes = this.$cookies.get(COOKIE_NAME.LAPP_TIPS);
    if (cookieRes === true) {
      commit('setLegalAppTooltipsOption', {option: true});
      return;
    }
    commit('setLegalAppTooltipsOption', {option: false});
  },
  turnOnLegalAppTooltips({commit}) {
    this.$cookies.remove(COOKIE_NAME.LAPP_TIPS);
    this.$cookies.set(COOKIE_NAME.LAPP_TIPS, "true", {
      path: '/',
      maxAge: 60 * 60 * 24 * 360,
      sameSite: true
    });
    commit('setLegalAppTooltipsOption', {option: true});
  },
  turnOffLegalAppTooltips({commit}) {
    this.$cookies.remove(COOKIE_NAME.LAPP_TIPS);
    this.$cookies.set(COOKIE_NAME.LAPP_TIPS, "false", {
      path: '/',
      maxAge: 60 * 60 * 24 * 360,
      sameSite: true
    });
    commit('setLegalAppTooltipsOption', {option: false});
  },
};
