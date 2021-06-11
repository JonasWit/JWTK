<template>

  <v-list>
    <v-list-item v-for="item in billingDataFromFetch" :key="item.id">
      <v-list-item-content>
        <v-list-item-subtitle>{{ item.id }}. {{ item.name }}</v-list-item-subtitle>
        <v-list-item-subtitle>Ulica: {{ item.street }}, Nr: {{ item.address }}</v-list-item-subtitle>
        <v-list-item-subtitle>Tel.: {{ item.phone }}, Fax: {{ item.fax }}</v-list-item-subtitle>
        <v-list-item-subtitle>NIP: {{ item.nip }}, REGON: {{ item.regon }}</v-list-item-subtitle>
      </v-list-item-content>
    </v-list-item>
  </v-list>


</template>

<script>
export default {
  name: "billing-details-list",
  data: () => ({
    billingDataFromFetch: [],
  }),

  async fetch() {
    await this.getBillingData()
  },

  methods: {
    async getBillingData() {
      try {
        let billingDataFromFetch = await this.$axios.$get('/api/legal-app-billing/list');
        console.warn('billing data from fetch', billingDataFromFetch)
        this.billingDataFromFetch = billingDataFromFetch

      } catch (e) {
        console.warn('getBillingData API error', e)

      }

    }

  }
}
</script>

<style scoped>

</style>
