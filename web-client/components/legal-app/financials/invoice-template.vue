<template>
  <v-card>
    <v-card-title>Invoice content here</v-card-title>
    <v-col>
      <v-list class="d-flex justify-space-between" v-for="item in selectedRecords" :key="item.id">
        <v-list-item>
          <v-list-item-content>
            <v-list-item-subtitle>Hours: {{ item.hours }}</v-list-item-subtitle>
            <v-list-item-subtitle>Minutes: {{ item.minutes }}</v-list-item-subtitle>
            <v-list-item-subtitle>Rate: {{ item.rate }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-col>
  </v-card>
</template>

<script>
export default {
  name: "invoice-template",
  props: {
    selectedRecords: {
      required: true,
      type: Array,
      default: null
    },
  },

}
</script>

<style scoped>

</style>
