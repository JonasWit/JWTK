<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3 white--text" color="primary" dark>
        <v-toolbar-title class="mr-3">
          Lista Kontaktów
        </v-toolbar-title>
        <v-autocomplete flat hide-no-data hide-details label="Wyszukaj kontakt" solo-inverted return-object clearable
                        v-model="searchResult" prepend-inner-icon="mdi-magnify" :items="contactItems"
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

      <v-card v-for="item in contactList" :key="item.id" class="card">
        <v-list flat>
          <v-list-item>
            <v-list-item-avatar>
              <v-avatar color="orange" size="56" class="white--text">
                {{ initials(item) }}
              </v-avatar>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title> {{ item.title }} {{ item.name }} {{ item.surname }}</v-list-item-title>
            </v-list-item-content>
            <v-list-item-action>
              <v-col class="mx-2">
                <v-list class="d-flex justify-md-end justify-sm-space-between">
                  <contact-list-details :selected-contact="item"/>
                  <delete-contact-dialog :selected-contact="item"/>
                  <edit-contact-dialog :selected-contact="item" v-on:action-completed="actionDone"/>
                </v-list>
              </v-col>
            </v-list-item-action>
          </v-list-item>
        </v-list>
      </v-card>


      <progress-bar v-if="loader"/>
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
import EditContactDialog from "@/components/legal-app/contacts/dialogs/edit-contact-dialog";
import {mapActions, mapState} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";


const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  name: "index",
  components: {ProgressBar, EditContactDialog, ContactListDetails, DeleteContactDialog, AddContactDialog, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],

  data: () => ({
    contactList: [],
    searchResult: "",
    finished: false,
    loader: true
  }),
  async fetch() {
    try {
      let clientId = this.$route.params.client;
      await this.getContactsList({clientId});
      this.contactList = this.contactItemsFromFetch;
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false
    }

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
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['contactItemsFromFetch']),
    contactItems() {
      return []
        .concat(this.contactItemsFromFetch.map(x => searchItemFactory(x.name, x.id)));
    },


  },
  methods: {
    ...mapActions('legal-app-client-store', ['getContactsList']),
    initials(item) {
      if (item.name) {
        return item.name[0][0].toUpperCase()
      } else {
        return item.title[0][0].toUpperCase()
      }

    },
    async actionDone() {
      this.loader = true
      try {
        let clientId = this.$route.params.client;
        await this.getContactsList({clientId});
        this.contactList = this.contactItemsFromFetch;
      } catch (error) {
        handleError(error);
      } finally {
        this.loader = false
      }
    },
    searchFilter(item, queryText) {
      return hasOccurrences(item.searchIndex, queryText);
    },
    formatDate(date) {
      return formatDate(date);
    },
  }
};
</script>

<style scoped>
.card {
  margin: 10px 0px;
  border-left: 3px solid #B41946 !important;
}

</style>
