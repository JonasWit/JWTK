<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3">
        <v-toolbar-title class="mr-3">
          Lista spraw
        </v-toolbar-title>
      </v-toolbar>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";

export default {
  name: "index",
  components: {Layout}
}
</script>

<style scoped>

</style>
