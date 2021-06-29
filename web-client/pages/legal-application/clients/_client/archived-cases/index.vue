<template>
  <layout>
    <template v-slot:content>
      <archived-cases/>
    </template>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import ArchivedCases from "@/components/legal-app/archive/archived-cases";

export default {
  name: "index",
  components: {ArchivedCases, Layout}
}
</script>

<style scoped>

</style>
