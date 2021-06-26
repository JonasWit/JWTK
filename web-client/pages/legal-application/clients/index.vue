<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3 white--text" color="primary" dark>
        <v-toolbar-title class="mr-3">
          Lista Klientów
        </v-toolbar-title>
        <v-autocomplete flat hide-no-data hide-details label="Wyszukaj klienta" solo-inverted return-object clearable
                        v-model="searchResult"
                        placeholder="Wpisz nazwę klienta" prepend-inner-icon="mdi-magnify" :items="clientItems"
                        :filter="searchFilter">
          <template v-slot:item="{item ,on , attrs}">
            <v-list-item v-on="on" :attrs="attrs">
              <v-list-item-content>{{ item.name }}</v-list-item-content>
            </v-list-item>
          </template>
        </v-autocomplete>
        <template v-slot:extension>
          <add-client-dialog/>
        </template>
      </v-toolbar>
      <div v-scroll="onScroll" class="my-6">
        <v-alert v-if="clientList.length === 0" elevation="5" text type="info" dismissible
                 close-text="Zamknij">
          Witaj w bazie Klientów! Użyj zielonej ikonki "+", aby dodać pierwszego klienta.
        </v-alert>
        <client-list-item :client-item="ci" v-for="ci in clientList" :key="`ci-item-${ci.id}`"/>
      </div>
      <button-to-go-up/>
    </template>
  </layout>

</template>

<script>

import AddClientDialog from "@/components/legal-app/clients/dialogs/add-client-dialog";
import {hasOccurrences} from "@/data/functions";
import {mapState} from "vuex";
import Layout from "@/components/legal-app/layout";
import ClientListItem from "@/components/legal-app/clients/client-list-item";
import ButtonToGoUp from "@/components/legal-app/button-to-go-up";
import {getClients, getClientsBasicList} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  name: "index",
  components: {ButtonToGoUp, AddClientDialog, ClientListItem, Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],


  data: () => ({
    searchResult: "",
    clientList: [],
    clientSearchItems: [],
    finished: false,
    loading: false,
    searchConditionsProvided: false,
    cursor: 0,
    takeAmount: 30,


  }),
  async fetch() {
    this.cursor = 0;
    this.clientList = [];
    this.clientSearchItems = await this.$axios.$get(getClientsBasicList());
    console.log('lista klientów', this.clientList)
    await this.handleFeed();

  },

  watch: {
    searchResult() {
      if (this.searchResult) {
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
      this.$nuxt.refresh();
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
        return `cursor=${this.cursor}&take=${this.takeAmount}`;
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

      return this.$axios.$get(`${getClients()}?${this.query}`)
        .then(clientsFeed => {
          if (clientsFeed.length === 0) {
            this.finished = true;
          } else {
            clientsFeed.forEach(x => this.clientList.push(x));
            this.cursor += this.takeAmount;
          }
        })
        .catch(e => {
          console.warn('ERROR', e);
        })
        .finally(() => {
          this.loading = false;
        });
    },


  }
};


</script>

<style scoped>

</style>
