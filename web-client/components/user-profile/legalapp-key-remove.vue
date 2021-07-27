<template>

  <v-card flat class="mb-2">
    <v-card-title class="d-flex justify-center error--text my-4">Usunięcie Danych Kancelarii</v-card-title>
    <v-col class="d-flex justify-center" cols="12">
      <div class="d-flex flex-column">
        <v-card-subtitle>
          Wybierajac opcję 'USUŃ DANE' możesz trawale usunąć wszystkie dane wprowadzone w aplikacji 'Twoja Kancelaria'
          dla
          Twojej firmy. Tym samym usuniesz dostęp do aplikacji dla wszystkich powiązanych użytkowników. Upewnij się, że
          nikt
          nie potrzebuje wglądu we wprowadzone dane, ponieważ odzyskanie ich nie będzie możliwe.
        </v-card-subtitle>
        <v-divider class="my-4"></v-divider>


        <default-confirmation-dialog button-color="error"
                                     v-on:action-confirmed="deleteLegalAppKey" title="Usuń Dane"
                                     button-text="Usuń Dane"
                                     message="Czy na pewno chcesz trwale usunąć wszystkie dane?"
                                     tooltip-message="usuń dane"/>
      </div>
    </v-col>
  </v-card>
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
