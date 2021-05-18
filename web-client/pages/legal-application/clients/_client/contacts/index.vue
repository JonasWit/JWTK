<template>
  <layout>
    <h1> LIst of contacts</h1>
    <v-card tile>
      <v-list-item>

      </v-list-item>
    </v-card>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";

export default {
  name: "index",
  components: {Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],


  data: () => ({
    searchResult: "",
    contactList: [],
    contactSearchItems: [],
    finished: false,
    loading: false,

  }),

  async fetch() {
    this.contactSearchItems = await this.$axios.$get(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contacts`)
    console.warn('clients contacts search results', this.contactSearchItems)
  }
}
</script>

<style scoped>

</style>
