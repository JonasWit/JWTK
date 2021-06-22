<template>
  <v-container>
    <case-details/>
    <cases-notes :selected-case="object"/>
    <case-deadlines/>
  </v-container>

</template>

<script>
import CasesNotes from "@/components/legal-app/cases/cases-notes";
import CaseDeadlines from "@/components/legal-app/cases/case-deadlines";
import CaseDetails from "@/components/legal-app/cases/case-details";

export default {
  name: "c-details",
  components: {CaseDetails, CaseDeadlines, CasesNotes},
}
</script>

<style scoped>

</style>
