<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3 white--text" color="primary" dark>
        <v-toolbar-title class="mr-3">
          Lista Kontaktów
        </v-toolbar-title>
        <v-autocomplete flat hide-no-data hide-details label="Wyszukaj kontakt"
                        solo-inverted return-object clearable v-model="searchResult"
                        prepend-inner-icon="mdi-magnify" :items="contactItems"
                        :filter="searchFilter">
          <template v-slot:item="{item ,on , attrs}">
            <v-list-item v-on="on" :attrs="attrs">
              <v-list-item-content>{{ item.name }}</v-list-item-content>
            </v-list-item>
          </template>
        </v-autocomplete>
        <template v-slot:extension>
          <add-contact-dialog v-on:action-completed="actionDone"/>
        </template>
      </v-toolbar>
      <v-alert :value="alertMessage" v-if="!contactList" elevation="5" text type="info" dismissible
               close-text="Zamknij">
        Zarządzaj kontaktami dla Klienta! Użyj zielonej ikonki "+", aby uzupełnić pierwszy kontakt.
        Następnie wybierz sekcję, którą chcesz uzupełnić.
      </v-alert>
      <v-expansion-panels focusable multiple class="expansion">
        <v-expansion-panel v-for="item in contactList" :key="item.id">
          <v-expansion-panel-header>
            <template v-slot:actions>
              <v-icon color="primary">
                $expand
              </v-icon>
            </template>
            <v-row class="d-flex align-center">
              <v-col>
                <v-col> Nazwa: {{ item.title }}</v-col>
                <v-col> Imię i nazwisko: {{ item.name }} {{ item.surname }}</v-col>
              </v-col>
              <v-spacer></v-spacer>
              <v-col>
                <v-col class="hidden-sm-and-down">Komentarz: {{ item.comment }}</v-col>
                <v-col class="hidden-sm-and-down">Dodano: {{ formatDate(item.created) }}</v-col>
              </v-col>

            </v-row>
          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <contact-list-details :selected-contact="item"/>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </template>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import AddContactDialog from "@/components/legal-app/contacts/dialogs/add-contact-dialog";
import {handleError, hasOccurrences} from "@/data/functions";
import DeleteContactDialog from "@/components/legal-app/contacts/dialogs/delete-contact-dialog";
import ContactListDetails from "@/components/legal-app/contacts/contact-list-details";
import {formatDate} from "@/data/date-extensions";
import {getContacts} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import EditContactDialog from "@/components/legal-app/contacts/dialogs/edit-contact-dialog";


const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  name: "index",
  components: {EditContactDialog, ContactListDetails, DeleteContactDialog, AddContactDialog, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],

  data: () => ({
    contactItemsFromFetch: [],
    contactList: [],
    searchResult: "",
    finished: false,
    loading: false,
    alertMessage: false
  }),

  async fetch() {
    await this.getContactsList()
    setTimeout(() => {
      this.alertMessage = true
    }, 500)
  },

  watch: {
    searchResult() {
      if (this.searchResult) {
        this.contactList = [];
        this.contactList.push(this.contactItemsFromFetch.find(contactItem => contactItem.id === this.searchResult.id));
      } else {
        this.contactList = this.contactItemsFromFetch;
      }
    },
  },
  computed: {
    contactItems() {
      return []
        .concat(this.contactItemsFromFetch.map(x => searchItemFactory(x.name, x.id)));
    },
  },
  methods: {
    async getContactsList() {
      try {
        let clientId = this.$route.params.client
        this.contactItemsFromFetch = await this.$axios.$get(getContacts(clientId));
        this.contactItemsFromFetch.sort((a, b) => {
          let contactA = new Date(a.created)
          let contactB = new Date(b.created)
          return contactB - contactA;
        })
        this.contactList = this.contactItemsFromFetch;
      } catch (error) {
        console.error('creating contact error', error)
        this.$notifier.showErrorMessage(error.response.data)
      }
    },

    async actionDone() {
      try {
        await this.getContactsList();
      } catch (error) {
        handleError(error)
      }
    },

    searchFilter(item, queryText) {
      return hasOccurrences(item.searchIndex, queryText);
    },

    formatDate(date) {
      return formatDate(date)
    },

  }
};
</script>

<style scoped>

</style>
