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
      <div v-scroll="onScroll" class="my-6">
        <v-alert v-if="clientList.length === 0" elevation="5" text type="info" dismissible close-text="Zamknij">
          Witaj w bazie Klientów! Użyj zielonej ikonki "+", aby dodać pierwszego klienta.
        </v-alert>
        <v-expansion-panels>
          <v-expansion-panel v-for="clientItem in clientList" :key="`ci-item-${clientItem.id}`" class="expansion">
            <v-expansion-panel-header>Nazwa: {{ clientItem.name }}
              <template v-slot:actions>
                <v-icon color="primary">
                  $expand
                </v-icon>
              </template>
            </v-expansion-panel-header>
            <v-expansion-panel-content>
              <v-card flat>
                <v-card-text>
                  <v-row class="align-center">
                    <v-col class="mx-2">
                      <v-list class="d-flex justify-space-between">
                        <v-list-item-content>
                          <v-list-item-title> Nazwa:{{ clientItem.id }} {{ clientItem.name }}</v-list-item-title>
                        </v-list-item-content>
                      </v-list>
                    </v-col>
                    <v-col class="mx-2">
                      <v-list class="d-flex justify-space-between">
                        <v-list-item-content>
                          <v-list-item-subtitle>Dodano: {{ formatDate(clientItem.created) }}
                          </v-list-item-subtitle>
                          <v-list-item-subtitle>Dodane przez: {{ clientItem.createdBy }}</v-list-item-subtitle>
                          <v-list-item-subtitle>Edytowano: {{ formatDate(clientItem.updated) }}
                          </v-list-item-subtitle>
                          <v-list-item-subtitle>Edytowane przez: {{ clientItem.updatedBy }}</v-list-item-subtitle>
                        </v-list-item-content>
                      </v-list>
                    </v-col>
                    <v-col class="mx-2">
                      <v-list class="d-flex justify-md-end justify-sm-space-between">
                        <go-to-client-panel :client-item="clientItem"/>
                        <edit-client-name-dialog :selected-client="clientItem"/>
                        <delete-client-dialog v-if="clientAdmin" :selected-client="clientItem"/>
                        <archive-client-dialog v-if="clientAdmin" :selected-client="clientItem"/>
                      </v-list>
                    </v-col>
                  </v-row>
                </v-card-text>
              </v-card>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </div>
      <progress-bar v-if="loader"/>
    </template>
  </layout>

</template>

<script>

import AddClientDialog from "@/components/legal-app/clients/dialogs/add-client-dialog";
import {handleError, hasOccurrences} from "@/data/functions";
import {mapGetters, mapState} from "vuex";
import Layout from "@/components/legal-app/layout";
import ButtonToGoUp from "@/components/legal-app/button-to-go-up";
import {getClients, getClientsBasicList} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import DeleteClientDialog from "@/components/legal-app/clients/dialogs/delete-client-dialog";
import ArchiveClientDialog from "@/components/legal-app/clients/dialogs/archive-client-dialog";
import GoToClientPanel from "@/components/legal-app/clients/go-to-client-panel";
import EditClientNameDialog from "@/components/legal-app/clients/dialogs/edit-client-name-dialog";
import {formatDate} from "@/data/date-extensions";
import AllowedUsers from "@/components/legal-app/clients/accesses-panel/allowed-users";
import GrantAccess from "@/components/legal-app/clients/accesses-panel/grant-access";
import IfAuth from "@/components/auth/if-auth";
import ProgressBar from "@/components/legal-app/progress-bar";

const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  name: "index",
  components: {
    ProgressBar,
    GoToClientPanel,
    EditClientNameDialog,
    ArchiveClientDialog,
    DeleteClientDialog, AllowedUsers, GrantAccess, ButtonToGoUp, AddClientDialog, Layout, IfAuth
  },
  middleware: ['legal-app-permission', 'user', 'authenticated'],

  data: () => ({
    searchResult: "",
    clientList: [],
    clientSearchItems: [],
    finished: false,
    loader: false,
    searchConditionsProvided: false,
    cursor: 0,
    takeAmount: 30,
  }),
  async fetch() {
    try {
      this.cursor = 0;
      this.clientList = [];
      this.clientSearchItems = await this.$axios.$get(getClientsBasicList());
      await this.handleFeed();
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false;
    }
  },
  watch: {
    searchResult() {
      if (this.searchResult) {
        this.loader = true;
        this.$axios.$get(`/api/legal-app-clients/client/${this.searchResult.id}`)
          .then(clientFound => {
            if (clientFound) {
              this.clientList = [];
              this.clientList.push(clientFound);
              this.cursor = 0;
              this.finished = false;
            }

          })
          .finally(() => this.loader = false);
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
    ...mapGetters('auth', ['clientAdmin']),
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
      if (this.loader) return;
      this.loader = true;

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
          this.loader = false;
        });
    },
    formatDate(date) {
      return formatDate(date);
    }
  }
};


</script>

<style scoped>

</style>
