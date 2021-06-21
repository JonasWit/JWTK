<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3">
        <v-toolbar-title class="mr-3">
          Lista spraw
        </v-toolbar-title>
        <v-btn @click="groupByKey">Test for grouping
        </v-btn>
      </v-toolbar>

      <v-card v-for="item in groupedCases" :key="item.id">
        <v-card-title v-for="group in item" :key="group.id">{{ group.group }}</v-card-title>
      </v-card>


      <v-card>
        <v-list v-for="item in listOfCases" :key="item.id">
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>
                {{ item.name }}
              </v-list-item-title>
              <v-list-item-subtitle>{{ item.signature }}</v-list-item-subtitle>
              <v-list-item-subtitle>{{ item.description }}</v-list-item-subtitle>
              <v-list-item-subtitle>{{ item.group }}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-card>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {groupByKey} from "@/data/functions";


export default {
  name: "index",
  components: {Layout},
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

</style>
