<template>
  <v-card flat class="mb-2">
    <v-toolbar color="primary" dense>
      <v-toolbar-title class="white--text">Usunięcie Danych Aplikacji</v-toolbar-title>
    </v-toolbar>
    <v-col class="d-flex justify-center" cols="12">
      <div class="d-flex flex-column">
        <v-card-subtitle>
          Wybierajac opcję 'USUŃ DANE' możesz trawale usunąć wszystkie dane wprowadzone w dostępnych aplikacjach.
          Tym samym usuniesz dostęp do aplikacji dla wszystkich powiązanych użytkowników. Upewnij się, że nikt
          nie potrzebuje wglądu we wprowadzone dane, ponieważ odzyskanie ich nie będzie możliwe.
        </v-card-subtitle>
        <v-divider class="my-4"></v-divider>
        <default-confirmation-dialog v-if="legalAppAllowed && legalAppKeyAvailable" button-color="error"
                                     v-on:action-confirmed="deleteLegalAppKey" title="Usuń Dane Kancelarii"
                                     button-text="Usuń Dane Kancelarii"
                                     message="Czy na pewno chcesz trwale usunąć wszystkie dane?"
                                     tooltip-message="Usuń dane Kancelarii"/>
        <default-confirmation-dialog v-if="medicalAppAllowed && medicalAppKeyAvailable" button-color="error"
                                     v-on:action-confirmed="deleteMedicalAppKey" title="Usuń Dane Gabinetu"
                                     button-text="Usuń Dane Gabinetu"
                                     message="Czy na pewno chcesz trwale usunąć wszystkie dane?"
                                     tooltip-message="Usuń dane Gabinetu"/>
        <default-confirmation-dialog v-if="restaurantAppAllowed && restaurantAppKeyAvailable" button-color="error"
                                     v-on:action-confirmed="deleteRestaurantAppKey" title="Usuń Dane Restauracji"
                                     button-text="Usuń Dane Restauracji"
                                     message="Czy na pewno chcesz trwale usunąć wszystkie dane?"
                                     tooltip-message="Usuń dane Restauracji"/>
      </div>
    </v-col>
  </v-card>
</template>

<script>
import {mapActions, mapGetters} from "vuex";

export default {
  name: "key-remove",
  computed: {
    ...mapGetters('auth', ['legalAppAllowed', 'legalAppKeyAvailable', 'medicalAppAllowed', 'medicalAppKeyAvailable', 'restaurantAppAllowed', 'restaurantAppKeyAvailable'])
  },
  methods: {
    ...mapActions('auth', ['deleteLegalAppKey', 'deleteRestaurantAppKey', 'deleteMedicalAppKey'])
  }
};


</script>

<style scoped>

</style>
