const initState = () => ({
  metaArray: []
});

export const state = initState;

export const mutations = {
  setMetaTag(state, {pageName, fullPath}) {
    console.warn('from store', {pageName, fullPath})

    if (!state.metaArray.some(x => x.route === fullPath)) {
      state.metaArray.push({
        pageName: pageName['pageName'],
        fullPath
      })
    }

  }
};

export const actions = {};
