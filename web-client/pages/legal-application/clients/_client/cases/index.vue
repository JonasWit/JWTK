<template>
  <layout>
    <template v-slot:content>
      <v-container>
        <v-card class="mx-auto" v-for="item in groupedCases" :key="item[0].group">
          <v-card-title class="white--text orange darken-4">
            {{ item[0].group }}
            <v-spacer></v-spacer>
            <v-btn color="white" class="text--primary" fab small>
              <v-icon>mdi-plus</v-icon>
            </v-btn>
          </v-card-title>
          <v-divider></v-divider>
          <v-virtual-scroll height="300" :items="items" :item-height="50">
            <template v-slot:default="{ item }">
              <v-list-item>
                <v-list-item-content v-for="object in item" :key="object.id">
                  <v-list-item-title> {{ object.name }}</v-list-item-title>
                </v-list-item-content>
                <v-list-item-action>
                  <v-btn depressed small>
                    Przejdź do szczegółów
                    <v-icon color="orange darken-4" right>
                      mdi-open-in-new
                    </v-icon>
                  </v-btn>
                </v-list-item-action>
              </v-list-item>
            </template>
          </v-virtual-scroll>
        </v-card>


        <h1 class="my-5">
          Lista spraw
        </h1>
        <v-row class="d-flex justify-space-around">
          <v-card v-for="item in groupedCases" :key="item[0].group">
            <v-card-title>
              {{ item[0].group }}
            </v-card-title>
            <v-card-subtitle v-for="object in item" :key="object.id">
              {{ object.name }}
            </v-card-subtitle>

          </v-card>

        </v-row>


      </v-container>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {groupByKey} from "@/data/functions";
import CasesNotes from "@/components/legal-app/cases/cases-notes";


export default {
  name: "index",
  components: {CasesNotes, Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],

  data: () => ({

    listOfCases: [],
    name: "",
    signature: "",
    description: "",
    groupedCases: [],
    benched: 0,


  }),

  async fetch() {
    try {
      await this.searchListOfCases()
    } finally {
      return this.groupByKey()


    }

  },

  computed: {
    items() {
      return Array.from({length: this.length}, (k, v) => v + 1)
    },
    length() {
      return 7000
    },
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
.expansion {
  border-left: 3px solid #B41946 !important;
}

</style>
