<template>
  <div>
    <v-card flat class="mb-2">
      <v-card-title class="d-flex justify-center">Usunięcie Konta</v-card-title>
      <v-col class="d-flex justify-center" cols="12">
        <div class="d-flex flex-column">
          <v-card-subtitle>
            Możesz trawale usunąć swoje konto. Ta akcja jest nieodwracalna i spowoduje utratę dostępu do usług
            związanych z portalem. W celu ponownego uzyskania dostępu konieczne będzie przesłanie prośby o utworzenie
            nowego konta i otrzymanie nowego zaproszenia.
          </v-card-subtitle>
          <v-divider class="my-4"></v-divider>
          <default-confirmation-dialog button-color="error" v-on:action-confirmed="deleteAccount" title="Usuń Konto"
                                       button-text="Usuń Konto" tooltip-message="Usuń konto"
                                       message="Czy na pewno chcesz trwale usunąć konto na portalu?"/>
        </div>
      </v-col>
    </v-card>
  </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "account-remove",
  methods: {
    ...mapActions('auth', ['deleteAccount'])
  }
};


</script>

<style scoped>

</style>
