<template>
  <!--  <legalapp-layout>-->
  <!--    <template v-slot:content>-->

  <!--    </template>-->
  <!--  </legalapp-layout>-->


  <v-container fluid>
    <v-row no-gutters>
      <v-col cols="2">
        <legalapp-navigation-drawer/>
      </v-col>
      <v-col cols="9">
        <v-autocomplete return-object clearable v-model="searchResult" placeholder="Start typing to Search" dense
                        hide-details append-icon="" prepend-inner-icon="mdi-magnify" :items="clientItems"
                        :filter="searchFilter">
          <template v-slot:item="{item ,on , attrs}">
            <v-list-item v-on="on" :attrs="attrs">
              <v-list-item-content>{{ item.name }}</v-list-item-content>
            </v-list-item>
          </template>
        </v-autocomplete>


        <div v-scroll="onScroll">
          <v-list>
            <legalapp-client-list-item :client-item="ci" v-for="ci in clientList" :key="`ci-item-${ci.id}`"/>
          </v-list>
        </div>


      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import NavigationDrawer from "@/components/legal-app/navigation-drawer";
import {hasOccurrences} from "@/data/functions";

const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  name: "index",
  components: {NavigationDrawer},
  data: () => ({
    searchResult: "",
    clientSearchItems: [],
    clientList: [],
    finished: false,
    loading: false,
    searchConditionsProvided: false,
    cursor: 0
  }),
  async fetch() {
    await this.$axios.$get("/api/legal-app-clients/clients/basic-list")
      .then((clientsToSearch) => {
        console.warn('client to search', clientsToSearch);
        this.clientSearchItems = clientsToSearch;
      })
      .catch(() => {
      });
  },
  created() {
    this.handleFeed();
  },
  watch: {
    searchResult(searchResult) {
      if (this.searchResult) {
        console.warn('search result:', this.searchResult);
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
    }
  },
  computed: {
    clientItems() {
      return []
        .concat(this.clientSearchItems.map(x => searchItemFactory(x.name, x.id)));
    },
    query() {
      if (this.searchConditionsProvided) {
        this.cursor = 0;
        this.clientList = [];
        this.showSelectedClient();
      } else {
        return `cursor=${this.cursor}&take=10`;
      }
    },
  },
  methods: {
    searchFilter(item, queryText, itemText) {
      return hasOccurrences(item.searchIndex, queryText);
    },
    onScroll() {
      if (this.finished || this.loading || this.searchResult) return;
      const loadMore = document.body.offsetHeight - (window.pageYOffset + window.innerHeight) < 500;
      if (loadMore) {
        this.handleFeed();
      }
    },
    handleFeed() {
      console.warn('query!', this.query);
      this.loading = true;

      this.$axios.$get(`/api/legal-app-clients/clients?${this.query}`)
        .then(clientsFeed => {
          console.warn('clients form API for feed', clientsFeed);
          if (clientsFeed.length === 0) {
            this.finished = true;
          } else {
            clientsFeed.forEach(x => this.clientList.push(x));
            this.cursor += 10;
          }
        })
        .finally(() => this.loading = false);
    }
  }
};
</script>

<style scoped>

</style>
