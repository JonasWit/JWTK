<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3">
        <v-toolbar-title class="mr-3">
          Lista spraw
        </v-toolbar-title>
      </v-toolbar>
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


export default {
  name: "index",
  components: {Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],

  data: () => ({

    listOfCases: [],
    name: "",
    signature: "",
    description: "",
    group: "",


  }),

  computed: {
    groupedItems() {
      let group = this.listOfCases.reduce((r, a) => {
        r[a[this.group]] = [...r[a.this.group] || [], a];
        return r;
      }, {});
      console.log("group", group);
    },


  },
  async fetch() {
    return this.searchListOfCases()

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

    }


  }
}
</script>

<style scoped>

</style>
