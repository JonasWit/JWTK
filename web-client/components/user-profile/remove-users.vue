<template>
  <div>
    <v-card flat class="mt-2">
      <v-toolbar color="primary" dense>
        <v-toolbar-title class="white--text">Usunięcie powiązanych użytkowników z Kancelarii</v-toolbar-title>
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
            <v-card-title>Lista powiązanych użytkowników</v-card-title>

            <v-chip v-for="(user, index) in otherLegalRelatedUsers" :key="user.id" class="ma-2" close color="red"
                    text-color="white" @click:close="revokeLegalAccess(user)">
              {{ user.email }}
            </v-chip>

          </v-card-text>
          <default-confirmation-dialog button-color="error" v-on:action-confirmed="revokeLegalAccess"
                                       title="Usuń dostęp" button-text="Usuń Dostęp"
                                       message="Czy na pewno chcesz trwale usunąć powiązanego użytkownika?"
                                       tooltip-message="Usuń powiązanego użytkownika"/>
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

export default {
  name: "remove-users",
  components: {RevokeAccessRelatedUsers, AllowedUsers},
  computed: {
    ...mapGetters('auth', ['otherLegalRelatedUsers'])
  },
  methods: {
    ...mapActions('auth', ['reloadLegalAppRelatedUsers']),
    async revokeLegalAccess(user) {
      const payload = {
        userId: user.id
      };
      console.log('User Id:', payload);
      try {
        await this.$axios.$post('/api/portal-admin/key-admin/legal-app/access-key/revoke', payload);
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
        await this.$axios.$post('/api/portal-admin/key-admin/medical-app/access-key/revoke', payload);
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        this.reloadLegalAppRelatedUsers();
        this.$nuxt.refresh();
      }
    }
  },
  async revokeRestaurantAccess(user) {
    const payload = {
      userId: user.id
    };
    console.log('User Id:', payload);
    try {
      await this.$axios.$post('/api/portal-admin/key-admin/restaurant-app/access-key/revoke', payload);
      this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
    } catch (error) {
      handleError(error);
    } finally {
      this.reloadLegalAppRelatedUsers();
      this.$nuxt.refresh();
    }
  }
};

</script>

<style scoped>

</style>
