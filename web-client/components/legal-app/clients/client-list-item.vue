<template>
  <v-card tile>
    <v-list-item>
      <v-list-item-title> Nazwa Klienta: {{ clientItem.name }}</v-list-item-title>
      <v-list-item-subtitle>Dodano: {{ clientItem.created }}</v-list-item-subtitle>

      <v-list-item-icon class="d-flex justify-end">
        <delete-client-dialog :selected-client="clientItem"/>
      </v-list-item-icon>
      <v-list-item-icon class="d-flex justify-end">
        <v-tooltip bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-btn icon link nuxt :to="`/legal-application/clients/${clientItem.id}/c-details`"
                   :key="`ci-record-${clientItem.id}`" dark v-bind="attrs" v-on="on">
              <v-icon medium color="success">mdi-arrow-right-bold-box</v-icon>
            </v-btn>
          </template>
          <span>Przejdź do panelu klienta</span>
        </v-tooltip>
      </v-list-item-icon>


    </v-list-item>
  </v-card>
</template>

<script>

import DeleteClientDialog from "@/components/legal-app/clients/dialogs/delete-client-dialog";

export default {
  name: "client-list-item",
  components: {DeleteClientDialog},
  props: {
    clientItem: {
      type: Object,
      required: true
    }
  },
};
</script>

<style scoped>

</style>
