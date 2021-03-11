const initState = () => ({
  profile: null,
});

export const state = initState;

export const ROLES = {
  INVITED: "Invited",
  CLIENT: "Client",
  CLIENT_ADMIN: "ClientAdmin",
  PORTAL_ADMIN: "PortalAdmin",
};

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
  clientAdmin: (state, getters) => getters.authenticated &&
    (state.profile.role === ROLES.CLIENT_ADMIN || getters.portalAdmin),
  portalAdmin: (state, getters) => getters.authenticated && state.profile.role === ROLES.PORTAL_ADMIN,
  legalAppAllowed: (state, getters) => getters.authenticated && state.profile.legalAppAllowed === true,
};

export const mutations = {
  saveProfile(state, {profile}) {
    state.profile = profile;
  },
};

export const actions = {
  initialize({commit}) {
    return this.$axios.$get('/api/users/me')
      .then((profile) => {
        console.log(profile);
        commit('saveProfile', {profile},
        );
      })
      .catch(() => {
      });
  },
  login() {
    if (process.server) return;
    const returnUrl = encodeURIComponent(location.href);
    window.location = `${this.$config.auth.loginPath}?returnUrl=${returnUrl}`;
  },
  logout() {
    if (process.server) return;
    window.location = this.$config.auth.logoutPath;
  },
};
