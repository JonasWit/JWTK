<template>
  <div>
    <v-card flat class="mt-2">
      <v-toolbar color="primary" dense>
        <v-toolbar-title class="white--text">Usunięcie powiązanych użytkowników z Aplikacji</v-toolbar-title>
      </v-toolbar>
      <v-col class="d-flex justify-center" cols="12">
        <div class="d-flex flex-column">
          <v-card-subtitle>
            Możesz usunąć powiązanych użytkowników. Ta akcja jest nieodwracalna i spowoduje utratę dostępu do danych
            kancekarii przez usuniętego użytkownika. W celu ponownego uzyskania przez niego dostępu konieczne będzie
            przesłanie prośby o ponowne dodanie.
          </v-card-subtitle>
          <v-divider></v-divider>

          <v-card-text v-if="otherLegalRelatedUsers.length > 0">
            <v-card-text>
              Powiązani użytkownicy dla Mojej Kancelarii
            </v-card-text>
            <v-chip v-for="(user) in otherLegalRelatedUsers" :key="user.id" small class="ma-2" close color="red"
                    text-color="white" @click:close="revokeLegalAccess(user)">
              {{ user.email }}
            </v-chip>
          </v-card-text>

          <v-card-text v-if="otherMedicalRelatedUsers.length > 0">
            <v-card-text>
              Powiązani użytkownicy dla Mojego Gabinetu
            </v-card-text>
            <v-chip v-for="(user) in otherMedicalRelatedUsers" :key="user.id" small class="ma-2" close color="red"
                    text-color="white" @click:close="revokeMedicalAccess(user)">
              {{ user.email }}
            </v-chip>
          </v-card-text>

          <v-card-text v-if="otherRestaurantRelatedUsers.length > 0">
            Powiązani użytkownicy dla Mojej Restauracji
          </v-card-text>
          <v-card-text v-if="otherRestaurantRelatedUsers.length > 0">
            <v-chip v-for="(user) in otherRestaurantRelatedUsers" :key="user.id" small class="ma-2" close color="red"
                    text-color="white" @click:close="revokeRestaurantAccess(user)">
              {{ user.email }}
            </v-chip>
          </v-card-text>
        </div>
      </v-col>
    </v-card>
  </div>
</template>

<script>
import {mapActions, mapGetters} from "vuex";
import AllowedUsers from "@/components/legal-app/clients/accesses-panel/allowed-users";
import RevokeAccessRelatedUsers from "@/components/portal-admin/users-management/revoke-access-related-users";
import {handleError} from "@/data/functions";
import {
  revokeLegalAppUser,
  revokeMedAppUser,
  revokeRestaurantAppUser
} from "@/data/endpoints/legal-app/user-profile-endpoints";

export default {
  name: "remove-users",
  components: {RevokeAccessRelatedUsers, AllowedUsers},
  computed: {
    ...mapGetters('auth', ['otherLegalRelatedUsers', 'otherMedicalRelatedUsers', 'otherRestaurantRelatedUsers'])
  },
  methods: {
    ...mapActions('auth', ['reloadLegalAppRelatedUsers', 'reloadMedicalAppRelatedUsers', 'reloadRestaurantAppRelatedUsers']),
    async revokeLegalAccess(user) {
      const payload = {
        userId: user.id
      };
      console.log('User Id:', payload);
      try {
        await this.$axios.$post(revokeLegalAppUser(), payload);
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        this.reloadLegalAppRelatedUsers();
        this.$nuxt.refresh();
      }
    },
    async revokeMedicalAccess(user) {
      const payload = {
        userId: user.id
      };
      console.log('User Id:', payload);
      try {
        await this.$axios.$post(revokeMedAppUser(), payload);
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        this.reloadMedicalAppRelatedUsers();
        this.$nuxt.refresh();
      }
    },
    async revokeRestaurantAccess(user) {
      const payload = {
        userId: user.id
      };
      console.log('User Id:', payload);
      try {
        await this.$axios.$post(revokeRestaurantAppUser(), payload);
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        this.reloadRestaurantAppRelatedUsers();
        this.$nuxt.refresh();
      }
    }
  },
};

</script>

<style scoped>

</style>
