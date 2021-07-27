<template>
  <div>
    <v-card class="mb-2">
      <v-card-title class="d-flex justify-center error--text">Usunięcie Danych Kancelarii</v-card-title>
      <v-col class="d-flex justify-center" cols="12">
        <div class="d-flex flex-column">
          <v-card-subtitle>
            Możesz trawale usunąć wszystkie dane wprowadzone dla Twojej Kancelarii.
          </v-card-subtitle>
          <default-confirmation-dialog button-color="error" v-on:action-confirmed="deleteLegalAppKey" title="Usuń Dane"
                                       button-text="Usuń Dane"
                                       message="Czy na pewno chcesz trwale usunąć wszystkie dane?"/>
        </div>
      </v-col>
    </v-card>
  </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "legalapp-key-remove",
  methods: {
    ...mapActions('auth', ['deleteLegalAppKey'])
  }
};


</script>

<style scoped>

</style>
