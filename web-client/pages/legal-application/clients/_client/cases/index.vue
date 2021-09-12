<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3 white--text" color="primary" dark>
        <v-toolbar-title class="mr-3">
          Lista Spraw
        </v-toolbar-title>
        <v-autocomplete flat hide-no-data hide-details label="Wyszukaj sprawę" solo-inverted return-object clearable
                        placeholder="Wpisz sygnaturę sprawy" prepend-inner-icon="mdi-magnify" :items="listOfCases"
        >
          <template v-slot:item="{item ,on , attrs}">
            <v-list-item v-on="on" :attrs="attrs">
              <v-list-item-content>{{ item.name }}</v-list-item-content>
            </v-list-item>
          </template>
        </v-autocomplete>
        <template v-slot:extension>
          <add-case/>
        </template>
      </v-toolbar>
      <v-alert elevation="5" text type="info" v-if="legalAppTooltips">
        Witaj w bazie spraw Klienta! Użyj zielonej ikonki "+", aby dodać pierwszą sprawę.
      </v-alert>

      <v-list class="expansion">
        <v-list-group :value="false" prepend-icon="mdi-text-box-multiple-outline" v-for="item in groupedCases"
                      :key="item[0].group"
                      no-action>
          <template v-slot:activator>
            <v-list-item-title> {{ item[0].group }}</v-list-item-title>
          </template>
          <v-list-item v-for="object in item" :key="object.id" link>
            <v-list-item-title>Nazwa:{{ object.name }}</v-list-item-title>
            <v-list-item-subtitle>Sygnatura: {{ object.signature }}</v-list-item-subtitle>
            <v-list-item-subtitle>Dodano: {{ formatDate(object.created) }}</v-list-item-subtitle>
            <v-list-item-action class="mx-1">
              <go-to-case-details :selected-case="object" :client-item="clientNumber"/>
            </v-list-item-action>
            <v-list-item-action class="mx-1">
              <delete-case :case-for-action="object"/>
            </v-list-item-action>
            <v-list-item-action class="mx-1">
              <archive-case :case-for-action="object"/>
            </v-list-item-action>
          </v-list-item>
        </v-list-group>
      </v-list>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {formatDate} from "@/data/date-extensions";

import AddCase from "@/components/legal-app/clients/cases/dialogs/add-case";
import {mapActions, mapState} from "vuex";
import GoToCaseDetails from "@/components/legal-app/clients/cases/go-to-case-details";
import {handleError} from "@/data/functions";
import DeleteCase from "@/components/legal-app/clients/cases/dialogs/delete-case";
import ArchiveCase from "@/components/legal-app/clients/cases/dialogs/archive-case";


export default {
  name: "index",
  components: {ArchiveCase, DeleteCase, GoToCaseDetails, AddCase, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],

  data: () => ({
    listOfCases: [],
    name: "",
    signature: "",
    description: "",
    dialog: false,
  }),
  async fetch() {
    try {
      let clientId = this.$route.params.client
      await this.getListOfGroupedCases({clientId})
    } catch (error) {
      handleError(error);
    }
  },

  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['groupedCases']),
    clientNumber() {
      return this.$route.params.client
    }
  },

  methods: {
    ...mapActions('legal-app-client-store', ['getListOfGroupedCases']),
    formatDate(date) {
      return formatDate(date)
    }
  },
}
</script>

<style scoped>


</style>
