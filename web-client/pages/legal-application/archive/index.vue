<template>
  <layout>
    <template v-slot:content>
      <archived-clients/>
    </template>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import ArchivedClients from "@/components/legal-app/archive/archived-clients";

export default {
  name: "index",
  components: {ArchivedClients, Layout},
  middleware: ['legal-app-permission', 'user-admin', 'authenticated'],

}


</script>

<style scoped>

</style>
