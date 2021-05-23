const initState = () => ({
  pageName: ""
});

export const state = initState;

export const mutations = {
  setMetaTag(state, pageName) {

    state.pageName = {pageName}
    console.warn('from store', state.pageName)
  }
};

export const actions = {};
