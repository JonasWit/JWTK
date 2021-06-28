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

      <v-alert elevation="5" text type="info" dismissible
               close-text="Zamknij">
        Witaj w bazie spraw Klienta! Użyj zielonej ikonki "+", aby dodać pierwszą sprawę.
      </v-alert>
      <v-expansion-panels>
        <v-expansion-panel v-for="item in groupedCases" :key="item[0].group" class="expansion">
          <v-expansion-panel-header>
            {{ item[0].group }}
            <template v-slot:actions>
              <v-icon color="primary">
                $expand
              </v-icon>
            </template>
          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <v-card v-for="object in item" :key="object.id" class="my-4">
              <v-row class="d-flex align-center">
                <v-col cols="4">
                  <v-card-text>
                    <div class="font-weight-bold">Nazwa:{{ object.name }}</div>
                    <div> Sygnatura: {{ object.signature }}</div>
                  </v-card-text>
                </v-col>
                <v-col cols="5">
                  <v-card-text>
                    <div class="font-weight-bold">Dodano: {{ formatDate(object.created) }}</div>
                    <div> Ostatnia modyfikacja: {{ formatDate(object.updated) }}</div>
                    <div>Użytkownik: {{ object.updatedBy }}</div>
                  </v-card-text>
                </v-col>
                <v-col cols="3">
                  <case-details :selected-case="object" :client-item="clientNumber"/>
                </v-col>
              </v-row>
            </v-card>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {groupByKey} from "@/data/functions";
import CasesNotes from "@/components/legal-app/clients/cases/cases-notes";
import DeleteCase from "@/components/legal-app/clients/cases/dialogs/delete-case";
import {formatDate} from "@/data/date-extensions";
import CaseDetails from "@/components/legal-app/clients/cases/case-details";
import AddCase from "@/components/legal-app/clients/cases/dialogs/add-case";


export default {
  name: "index",
  components: {AddCase, CaseDetails, DeleteCase, CasesNotes, Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],

  data: () => ({

    listOfCases: [],
    name: "",
    signature: "",
    description: "",
    groupedCases: [],
    dialog: false

  }),

  async fetch() {
    try {
      await this.searchListOfCases()
    } finally {
      this.groupByKey()
    }
  },

  computed: {
    items() {
      return Array.from({length: this.length}, (k, v) => v + 1)
    },
    length() {
      return 7000
    },
    clientNumber() {
      return this.$route.params.client
    }
  },

  methods: {
    async searchListOfCases() {
      try {
        let listOfCases = await this.$axios.$get(`/api/legal-app-cases/client/${this.$route.params.client}/cases`)
        console.warn('list of cases', listOfCases)
        this.listOfCases = listOfCases

      } catch (e) {
        console.warn('list of cases fetch error', e)
      }

    },

    groupByKey() {
      let input = this.listOfCases;
      let key = 'group';
      const groups = groupByKey(input, key)
      this.groupedCases = groups
      console.log('group by category fired', groups)
    },
    genRandomIndex(length) {
      return Math.ceil(Math.random() * (length - 1))
    },
    formatDate(date) {
      return formatDate(date)
    }

  },


}
</script>

<style scoped>


</style>
