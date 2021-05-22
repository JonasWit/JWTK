<template>
  <layout>
    <template v-slot:content>
      <app-breadcrumbs/>
      <v-toolbar prominent>
        <v-toolbar-title class="mr-3">
          Lista Klientów
        </v-toolbar-title>
        <v-autocomplete return-object clearable v-model="searchResult" placeholder="Start typing to Search" dense
                        hide-details append-icon="" prepend-inner-icon="mdi-magnify" :items="clientItems"
                        :filter="searchFilter">
          <template v-slot:item="{item ,on , attrs}">
            <v-list-item v-on="on" :attrs="attrs">
              <v-list-item-content>{{ item.id }}{{ item.name }}</v-list-item-content>
            </v-list-item>
          </template>
        </v-autocomplete>
        <template v-slot:extension>
          <add-client-dialog/>

        </template>
      </v-toolbar>
      <div v-scroll="onScroll">
        <v-list>
          <client-list-item :client-item="ci" v-for="ci in clientList" :key="`ci-item-${ci.id}`"/>
        </v-list>
      </div>
      <button-to-go-up/>
    </template>
  </layout>

</template>

<script>

import NavigationDrawer from "@/components/legal-app/navigation-drawer";
import AddClientDialog from "@/components/legal-app/clients/dialogs/add-client-dialog";
import {hasOccurrences} from "@/data/functions";
import {mapState} from "vuex";
import Layout from "@/components/legal-app/layout";
import ClientListItem from "@/components/legal-app/clients/client-list-item";
import ButtonToGoUp from "@/components/legal-app/button-to-go-up";
import AppBreadcrumbs from "@/components/legal-app/app-breadcrumbs";


const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  name: "index",
  components: {AppBreadcrumbs, ButtonToGoUp, AddClientDialog, ClientListItem, Layout, NavigationDrawer},
  middleware: ['legal-app-permission', 'client', 'authenticated'],

  data: () => ({
    searchResult: "",
    clientList: [],
    clientSearchItems: [],
    finished: false,
    loading: false,
    searchConditionsProvided: false,
    cursor: 0,
  }),

  async fetch() {
    this.clientSearchItems = await this.$axios.$get("/api/legal-app-clients/clients/basic-list");
    await this.handleFeed();
    console.warn('fetched clients')
  },

  watch: {
    searchResult() {
      if (this.searchResult) {
        console.warn('Search result:', this.searchResult);
        this.loading = true;
        this.$axios.$get(`/api/legal-app-clients/client/${this.searchResult.id}`)
          .then(clientFound => {
            if (clientFound) {
              this.clientList = [];
              this.clientList.push(clientFound);
              this.cursor = 0;
              this.finished = false;
            }
          })
          .finally(() => this.loading = false);
      } else {
        this.clientList = [];
        this.handleFeed();
      }
    },

    clientForAction() {
      Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)
      this.$nuxt.refresh()
      console.warn('Client list refreshed', this.cursor)
    }
  },

  computed: {
    ...mapState('legal-app-client-store', ['clientForAction']),
    clientItems() {
      return []
        .concat(this.clientSearchItems.map(x => searchItemFactory(x.name, x.id)));
    },
    query() {
      if (this.searchConditionsProvided) {
        this.cursor = 0;
        this.clientList = [];
      } else {
        return `cursor=${this.cursor}&take=10`;
      }
    },

  }
  ,
  methods: {
    searchFilter(item, queryText) {
      return hasOccurrences(item.searchIndex, queryText);
    }
    ,
    onScroll() {
      if (this.finished || this.loading || this.searchResult) return;
      const loadMore = document.body.offsetHeight - (window.pageYOffset + window.innerHeight) < 500;
      if (loadMore) {
        this.handleFeed();
      }
    }
    ,
    handleFeed() {
      if (this.loading) return;
      this.loading = true;

      return this.$axios.$get(`/api/legal-app-clients/clients?${this.query}`)
        .then(clientsFeed => {
          console.warn('query feed', this.query)
          console.warn('query feed', clientsFeed)
          if (clientsFeed.length === 0) {
            this.finished = true;
          } else {
            clientsFeed.forEach(x => this.clientList.push(x));
            this.cursor += 10;
          }
        })
        .catch(e => {
          console.warn('ERROR', e)
        })
        .finally(() => {
          this.loading = false
        })
    },


  }
}


</script>

<style scoped>

</style>
