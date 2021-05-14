<template>
  <legalapp-layout>
    <template v-slot:content>
      <snackbar/>
      <v-toolbar extended>
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

          <client-create-dialog/>

        </template>
      </v-toolbar>
      <div v-scroll="onScroll">
        <v-list>
          <legalapp-client-list-item :client-item="ci" v-for="ci in clientList" :key="`ci-item-${ci.id}`"/>
        </v-list>
      </div>


    </template>
  </legalapp-layout>
</template>

<script>
import NavigationDrawer from "@/components/legal-app/navigation-drawer";
import ClientCreateDialog from "@/components/legal-app/clients/dialogs/client-create-dialog";
import {mapActions, mapGetters, mapMutations, mapState} from "vuex";

export default {
  name: "index",
  components: {ClientCreateDialog, NavigationDrawer},
  async fetch() {
    this.reset();
    await this.$store.dispatch('legal-app-client-store/fetchClients')
    await this.$store.dispatch('legal-app-client-store/handleFeed')
  },
  watch: {
    searchResult(searchResult) {
      console.warn('watcher fired')
      this.handleSearchResult()

    }
  },
  computed: {
    ...mapState('legal-app-client-store', ['clientAutocompleteSearchResult', 'clientList']),
    ...mapGetters('legal-app-client-store', ['clientItems', 'query']),
    searchResult: {
      get() {
        return this.clientAutocompleteSearchResult
      },
      set(value) {
        this.updateClientAutocompleteSearchResult({value})
      }
    },
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['updateClientAutocompleteSearchResult', 'reset']),
    ...mapActions('legal-app-client-store', ['fetchClients', 'searchFilter', 'onScroll', 'handleFeed', 'handleSearchResult']),
  }
};
</script>

<style scoped>

</style>
