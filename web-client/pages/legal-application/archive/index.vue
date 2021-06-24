<template>
  <layout>
    <template v-slot:content>
      <v-toolbar color="primary" class="white--text">
        <v-toolbar-title>Archiwum</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-icon color="white">mdi-archive</v-icon>
      </v-toolbar>
      <h1>This is archive storage for all data.</h1>
    </template>

  </layout>

</template>

<script>
import Layout from "@/components/legal-app/layout";

export default {
  name: "index",
  components: {Layout}
}
</script>

<style scoped>

</style>
