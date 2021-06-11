<template>
  <v-row>
    <v-col>
      <v-list>
        <v-list-item>
          <v-list-item-content>
            <!--            <v-list-item-subtitle>Nazwa firmy: {{ billingData.name }}</v-list-item-subtitle>-->
            <!--            <v-list-item-subtitle>Ulica: {{ billingData.street }}</v-list-item-subtitle>-->
            <!--            <v-list-item-subtitle>Nr budynku i lokalu: {{ billingData.address }}</v-list-item-subtitle>-->
            <!--            <v-list-item-subtitle>Kod pocztowy: {{ billingData.postalCode }}</v-list-item-subtitle>-->
            <!--            <v-list-item-subtitle>Miasto: {{ billingData.city }}</v-list-item-subtitle>-->
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-col>
    <v-col>
      <v-list>
        <v-list-item>
          <v-list-item-content>
            <!--            <v-list-item-subtitle>Nr telefonu: {{ billingData.phoneNumber }}</v-list-item-subtitle>-->
            <!--            <v-list-item-subtitle>Fax: {{ billingData.faxNumber }}</v-list-item-subtitle>-->
            <!--            <v-list-item-subtitle>NIP: {{ billingData.nip }}</v-list-item-subtitle>-->
            <!--            <v-list-item-subtitle>REGON: {{ billingData.regon }}</v-list-item-subtitle>-->
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: "billing-details",
  billingDataFromFetch: [],

  async fetch() {
    await this.getBillingData()
  },

  methods: {
    async getBillingData() {
      try {
        let billingDataFromFetch = await this.$axios.$get('/api/legal-app-billing/list');


        console.warn('billing data from fetch', billingDataFromFetch)

      } catch (e) {
        console.warn('getBillingData API error', e)

      }

    }

  }
}
</script>

<style scoped>

</style>
