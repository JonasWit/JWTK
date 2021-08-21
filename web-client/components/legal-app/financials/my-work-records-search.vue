<template>
  <div>
    <my-work-date-picker/>
    <v-divider></v-divider>
    <my-work-records-list/>


  </div>
</template>

<script>
import MyWorkDatePicker from "@/components/legal-app/financials/my-work-date-picker";
import {mapGetters, mapMutations} from "vuex";
import {formatDate} from "@/data/date-extensions";
import MyWorkRecordsList from "@/components/legal-app/financials/my-work-records-list";
import AddNewWorkRecord from "~/components/legal-app/financials/dialogs/add-new-work-record";

export default {
  name: "my-work-records-search",
  components: {AddNewWorkRecord, MyWorkRecordsList, MyWorkDatePicker},
  computed: {
    ...mapGetters('legal-app-client-store', ['workRecordsList']),
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['updateFinancialRecordsFromFetch']),
    formatDate(date) {
      return formatDate(date);
    },
  }
}
</script>

<style scoped>

</style>
