<template>
  <layout>
    <template v-slot:content>
      <v-container>
        <v-card class="my-6">
          <v-card-title class="white--text orange darken-4">
            Lista spraw
            <v-spacer></v-spacer>
            <v-btn color="white" class="text--primary" fab small>
              <v-icon>mdi-plus</v-icon>
            </v-btn>
          </v-card-title>
        </v-card>
        <v-expansion-panels focusable>
          <v-expansion-panel v-for="item in groupedCases" :key="item[0].group" class="expansion">
            <v-expansion-panel-header>{{ item[0].group }}</v-expansion-panel-header>
            <v-expansion-panel-content>


              <v-card v-for="object in item" :key="object.id" class="d-flex justify-space-between">
                <v-card-subtitle>
                  {{ object.name }}
                </v-card-subtitle>
                <go-to-case-panel :selected-case="object" :client-item="clientNumber"/>
                <delete-case :selected-case="object"/>
              </v-card>


            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </v-container>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {groupByKey} from "@/data/functions";
import CasesNotes from "@/components/legal-app/cases/cases-notes";
import GoToCasePanel from "@/components/legal-app/cases/go-to-case-panel";
import DeleteCase from "@/components/legal-app/cases/dialogs/delete-case";


export default {
  name: "index",
  components: {DeleteCase, GoToCasePanel, CasesNotes, Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],

  data: () => ({

    listOfCases: [],
    name: "",
    signature: "",
    description: "",
    groupedCases: [],

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

  },


}
</script>

<style scoped>


</style>
