<template>
  <layout>
    <template v-slot:content>
      <v-toolbar prominent class="my-3">
        <v-toolbar-title class="mr-3">
          Lista Kontaktów
        </v-toolbar-title>

        <v-autocomplete return-object clearable v-model="searchResult" placeholder="Start typing to Search" dense
                        hide-details append-icon="" prepend-inner-icon="mdi-magnify" :items="contactItems"
                        :filter="searchFilter">
          <template v-slot:item="{item ,on , attrs}">
            <v-list-item v-on="on" :attrs="attrs">
              <v-list-item-content>{{ item.name }}</v-list-item-content>
            </v-list-item>
          </template>
        </v-autocomplete>
        <template v-slot:extension>
          <add-contact-dialog/>
        </template>
      </v-toolbar>
      <v-expansion-panels focusable>
        <v-expansion-panel v-for="item in contactList" :key="item.id">
          <v-expansion-panel-header>
            <v-row>
              <v-col> Nazwa: {{ item.title }}</v-col>
              <v-col> Imię i nazwisko: {{ item.name }} {{ item.surname }}</v-col>
              <v-col>Komentarz: {{ item.comment }}</v-col>
              <v-col>Dodano: {{ item.created }}</v-col>
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
import {hasOccurrences} from "@/data/functions";
import DeleteContactDialog from "@/components/legal-app/contacts/dialogs/delete-contact-dialog";
import ContactListDetails from "@/components/legal-app/contacts/contact-list-details";


const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  meta: {
    pageName: 'Kontakty'
  },
  name: "index",
  components: {ContactListDetails, DeleteContactDialog, AddContactDialog, Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated', 'meta-reader'],

  data: () => ({
    contactItemsFromFetch: [],
    contactList: [],
    searchResult: "",
    finished: false,
    loading: false,


  }),

  async fetch() {
    this.contactItemsFromFetch = await this.$axios.$get(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contacts`)
    console.warn('clients contacts search results', this.contactItemsFromFetch)
    this.contactList = this.contactItemsFromFetch
  },

  watch: {
    searchResult() {
      if (this.searchResult) {
        this.contactList = []
        this.contactList.push(this.contactItemsFromFetch.find(contactItem => contactItem.id === this.searchResult.id));
      } else {
        this.contactList = this.contactItemsFromFetch
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
    searchFilter(item, queryText) {
      return hasOccurrences(item.searchIndex, queryText);
    }
    ,
  }


}
</script>

<style scoped>

</style>
