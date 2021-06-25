<template>
  <div>
    <my-work-date-picker/>
    <my-work-records-list/>


  </div>
</template>

<script>
import MyWorkDatePicker from "@/components/legal-app/financials/my-work-date-picker";
import {mapGetters, mapMutations} from "vuex";
import {formatDate} from "@/data/date-extensions";
import MyWorkRecordsList from "@/components/legal-app/financials/my-work-records-list";

export default {
  name: "my-work-records-search",
  components: {MyWorkRecordsList, MyWorkDatePicker},
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
