<template>
  <div>
    <v-card flat v-for="item in billingDataFromFetch" :key="item.id">
      <v-row class="d-flex mx-2 mt-4 mb-2">
        <edit-billing-data :selected-billing-record="item"/>
        <delete-billing-data :selected-billing-record="item"/>
      </v-row>
      <v-col>
        <v-list-item-content>
          <v-list-item-subtitle>{{ item.name }}</v-list-item-subtitle>
          <v-list-item-subtitle>Ulica: {{ item.street }}, Nr: {{ item.address }}</v-list-item-subtitle>
          <v-list-item-subtitle>Miasto: {{ item.city }}, Kod pocztowy: {{ item.postalCode }}</v-list-item-subtitle>
          <v-list-item-subtitle>Tel.: {{ item.phoneNumber }}, Fax: {{ item.faxNumber }}</v-list-item-subtitle>
          <v-list-item-subtitle>NIP: {{ item.nip }}, REGON: {{ item.regon }}</v-list-item-subtitle>
        </v-list-item-content>
      </v-col>
    </v-card>
  </div>
</template>
<script>
import EditBillingData from "@/components/legal-app/financials/dialogs/edit-billing-data";
import DeleteBillingData from "@/components/legal-app/financials/dialogs/delete-billing-data";
import {mapActions, mapMutations, mapState} from "vuex";
import {handleError} from "@/data/functions";

export default {
  name: "billing-details-list",
  components: {DeleteBillingData, EditBillingData},
  async fetch() {
    try {
      await this.getBillingDataFromFetch();
    } catch (error) {
      handleError(error);
    }
  },
  computed: {
    ...mapState('legal-app-client-store', ['billingDataFromFetch']),
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['updateBillingDataFromFetch']),
    ...mapActions('legal-app-client-store', ['getBillingDataFromFetch'])
  }
};
</script>

<style scoped>

</style>
