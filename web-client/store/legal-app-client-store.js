import {hasOccurrences} from "@/data/functions";

const initState = () => ({
  clientAutocompleteSearchResult: null,
  clientSearchItems: [],
  clientList: [],
  finished: false,
  cursor: 0,

});

const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export const state = initState;

export const getters = {
  clientItems(state) {
    return []
      .concat(state.clientSearchItems.map(x => searchItemFactory(x.name, x.id)));
  },

  query(state) {
    if (state.clientAutocompleteSearchResult) {
      state.cursor = 0;
      state.clientList = [];
      //state.showSelectedClient();
    } else {
      return `cursor=${state.cursor}&take=10`;
    }
  },
};

export const mutations = {
  updateClientAutocompleteSearchResult(state, {value}) {
    state.clientAutocompleteSearchResult = value;
    console.log('update done')
  },
  updateClientSearchItems(state, {clientSearchItems}) {
    state.clientSearchItems = clientSearchItems;
  },
  addItemToClientList(state, {item}) {
    state.clientList.push(item)
  },
  clearClientList(state) {
    state.clientList = []
  },
  finishClientFeed(state) {
    state.finished = true
  },
  unfinishClientFeed(state) {
    state.finished = false
  },
  incrementCursor(state) {
    state.cursor += 10
  },
  clearCursor(state) {
    state.cursor = 0
  },
  reset(state) {
    Object.assign(state, initState());
  },
};

export const actions = {
  async fetchClients({commit}) {
    try {
      let clientSearchItems = await this.$axios.$get("/api/legal-app-clients/clients/basic-list");
      commit('updateClientSearchItems', {clientSearchItems});
    } catch (error) {
      console.error(error);
    }
  },
  handleFeed({commit, getters}) {
    this.$axios.$get(`/api/legal-app-clients/clients?${getters.query}`)
      .then(clientsFeed => {
        if (clientsFeed.length === 0) {
          commit('finishClientFeed')
        } else {
          clientsFeed.forEach(item => commit('addItemToClientList', {item}))
          commit('incrementCursor')
        }
      })
  },
  searchFilter(state, item, queryText, itemText) {
    return hasOccurrences(item.searchIndex, queryText);
  },
  onScroll({commit}) {
    if (this.finished || this.loading || this.searchResult) return;
    const loadMore = document.body.offsetHeight - (window.pageYOffset + window.innerHeight) < 500;
    if (loadMore) {
      //this.handleFeed();
    }
  },
  handleSearchResult({commit, state, dispatch}) {
    if (state.clientAutocompleteSearchResult) {
      this.$axios.$get(`/api/legal-app-clients/client/${this.searchResult.id}`)
        .then(item => {
          if (item) {
            commit('clearClientList')
            commit('clearCursor')
            commit('unfinishClientFeed')
            commit('addItemToClientList', {item})
          }
        })
    } else {
      commit('clearClientList')
      dispatch('handleFeed')
    }


  }


};
