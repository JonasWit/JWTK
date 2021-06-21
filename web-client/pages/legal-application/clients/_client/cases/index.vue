<template>
  <layout>
    <template v-slot:content>
      <v-container>
        <h1 class="my-2">
          Lista spraw
        </h1>
        <v-expansion-panels class="expansion" focusable>
          <v-expansion-panel v-for="item in groupedCases" :key="item[0].group">
            <v-expansion-panel-header>
              {{ item[0].group }}
            </v-expansion-panel-header>
            <v-expansion-panel-content v-for="object in item" :key="object.id">
              <v-list>
                <v-list-item>
                  <v-list-item-title>
                    {{ object.name }}
                  </v-list-item-title>
                  <v-list-item-title>
                    {{ object.signature }}
                  </v-list-item-title>
                  <v-list-item-subtitle>
                    {{ object.id }}
                  </v-list-item-subtitle>
                </v-list-item>
                <cases-notes :selected-case="object"/>
              </v-list>

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


  }),

  async fetch() {
    try {
      await this.searchListOfCases()
    } finally {
      return this.groupByKey()


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
    }

  },


}
</script>

<style scoped>
.expansion {
  border-left: 3px solid #B41946 !important;
}

</style>
