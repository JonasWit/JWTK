<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3 white--text" color="primary" dark>
        <v-toolbar-title class="mr-3">
          Lista Klientów
        </v-toolbar-title>
        <v-autocomplete flat hide-no-data hide-details label="Wyszukaj klienta" solo-inverted return-object clearable
                        v-model="searchResult" placeholder="Wpisz nazwę klienta" prepend-inner-icon="mdi-magnify"
                        :items="clientItems" :filter="searchFilter">
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
      <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
        Witaj w bazie Klientów! Użyj zielonej ikonki "+", aby dodać pierwszego klienta.
      </v-alert>
      <v-card v-for="clientItem in clientList" :key="`ci-item-${clientItem.id}`" class="expansion my-5">
        <v-card-text>
          <v-row class="align-center">
            <v-col class="mx-2">
              <v-list class="d-flex justify-space-between">
                <v-list-item-content>
                  <v-list-item-title>{{ clientItem.name }}</v-list-item-title>
                </v-list-item-content>
              </v-list>
            </v-col>
            <v-col class="mx-2">
              <v-list class="d-flex justify-md-end justify-sm-space-between">
                <go-to-client-panel :client-item="clientItem"/>
                <edit-client-name-dialog :selected-client="clientItem"/>
                <delete-client-dialog v-if="userAdmin" :selected-client="clientItem"/>
                <archive-client-dialog v-if="userAdmin" :selected-client="clientItem"/>
              </v-list>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </template>
  </layout>

</template>

<script>

import AddClientDialog from "@/components/legal-app/clients/dialogs/add-client-dialog";
import {handleError, hasOccurrences} from "@/data/functions";
import {mapActions, mapGetters, mapMutations, mapState} from "vuex";
import Layout from "@/components/legal-app/layout";
import DeleteClientDialog from "@/components/legal-app/clients/dialogs/delete-client-dialog";
import ArchiveClientDialog from "@/components/legal-app/clients/dialogs/archive-client-dialog";
import GoToClientPanel from "@/components/legal-app/clients/go-to-client-panel";
import EditClientNameDialog from "@/components/legal-app/clients/dialogs/edit-client-name-dialog";
import AllowedUsers from "@/components/legal-app/clients/accesses-panel/allowed-users";
import GrantAccess from "@/components/legal-app/clients/accesses-panel/grant-access";
import IfAuth from "@/components/auth/if-auth";

const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  name: "index",
  components: {
    GoToClientPanel,
    EditClientNameDialog,
    ArchiveClientDialog,
    DeleteClientDialog,
    AllowedUsers,
    GrantAccess,
    AddClientDialog,
    Layout,
    IfAuth
  },
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
    searchResult: "",
    clientList: [],
    clientSearchItems: [],
    searchConditionsProvided: false,

  }),
  async fetch() {
    try {
      await this.getBasicListOfClients()
      this.clientSearchItems = this.basicClientsInfo
      this.clientList = this.clientSearchItems
    } catch (error) {
      handleError(error)
    }
  },
  watch: {
    searchResult() {
      if (this.searchResult)
        this.$axios.$get(`/api/legal-app-clients/client/${this.searchResult.id}`)
          .then(clientFound => {
            if (clientFound) {
              this.clientList = [];
              this.clientList.push(clientFound);
            }
          })
      else {
        this.clientList = this.clientSearchItems
      }
    },
  },

  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapGetters('legal-app-client-store', ['basicClientsInfo']),
    ...mapGetters('auth', ['userAdmin']),
    clientItems() {
      return []
        .concat(this.clientSearchItems.map(x => searchItemFactory(x.name, x.id)));
    },
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['updateBasicClientsListFromFetch']),
    ...mapActions('legal-app-client-store', ['getBasicListOfClients']),
    searchFilter(item, queryText) {
      return hasOccurrences(item.searchIndex, queryText);
    }
  }
};


</script>

<style scoped>

</style>
