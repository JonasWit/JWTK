<template>
  <v-list>
    <v-list-item v-for="item in billingDataList" :key="item.id">
      <v-col>
        <v-list-item-content>
          <v-list-item-subtitle>{{ item.name }}</v-list-item-subtitle>
          <v-list-item-subtitle>Ulica: {{ item.street }}, Nr: {{ item.address }}</v-list-item-subtitle>
          <v-list-item-subtitle>Tel.: {{ item.phone }}, Fax: {{ item.fax }}</v-list-item-subtitle>
          <v-list-item-subtitle>NIP: {{ item.nip }}, REGON: {{ item.regon }}</v-list-item-subtitle>
        </v-list-item-content>
      </v-col>
      <v-col>
        <v-list-item-content>
          <v-list-item>
            <edit-billing-data :selected-billing-record="item"/>
          </v-list-item>
          <v-list-item>
            <delete-billing-data :selected-billing-record="item"/>
          </v-list-item>
          <v-list-item>
          </v-list-item>
        </v-list-item-content>
      </v-col>
    </v-list-item>
  </v-list>
</template>
<script>
import EditBillingData from "@/components/legal-app/financials/dialogs/edit-billing-data";
import DeleteBillingData from "@/components/legal-app/financials/dialogs/delete-billing-data";
import {mapActions, mapMutations, mapState} from "vuex";

export default {
  name: "billing-details-list",
  components: {DeleteBillingData, EditBillingData},

  async fetch() {
    await this.getBillingDataFromFetch();
  },
  computed: {
    ...mapState('legal-app-client-store', ['billingDataFromFetch'])
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['updateBillingDataFromFetch']),
    ...mapActions('legal-app-client-store', ['getBillingDataFromFetch'])
  }
}
</script>

<style scoped>

</style>
