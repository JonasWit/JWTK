<template>
  <div>
    <v-card flat class="mt-2">
      <v-toolbar color="primary" dense>
        <v-toolbar-title class="white--text">Usunięcie powiązanych użytkowników</v-toolbar-title>
      </v-toolbar>
      <v-col class="d-flex justify-center" cols="12">
        <div class="d-flex flex-column">
          <v-card-subtitle>
            Możesz usunąć powiązanych użytkowników. Ta akcja jest nieodwracalna i spowoduje utratę dostępu do usług
            związanych z portalem. W celu ponownego uzyskania dostępu konieczne będzie przesłanie prośby o utworzenie
            nowego konta i otrzymanie nowego zaproszenia.
          </v-card-subtitle>
          <v-divider></v-divider>
          <v-card-text v-if="relatedUsers.length > 0">
            <v-card-title>Lista powiązanych użytkowników</v-card-title>

            <v-chip v-for="(user, index) in relatedUsers" :key="user.id" class="ma-2" close color="red"
                    text-color="white"
                    @click:close="revokeAccess(user)">
              {{ user.email }}
            </v-chip>

          </v-card-text>
          <default-confirmation-dialog button-color="error"
                                       v-on:action-confirmed="revokeAccess" title="Usuń dostęp"
                                       button-text="Usuń Dostęp"
                                       message="Czy na pewno chcesz trwale usunąć powiązanego użytkownika?"
                                       tooltip-message="Usuń powiązanego użytkownika"/>
        </div>
      </v-col>
    </v-card>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import AllowedUsers from "@/components/legal-app/clients/accesses-panel/allowed-users";
import RevokeAccessRelatedUsers from "@/components/portal-admin/users-management/revoke-access-related-users";
import {handleError} from "@/data/functions";

export default {
  name: "remove-users",
  components: {RevokeAccessRelatedUsers, AllowedUsers},
  computed: {
    ...mapState('auth', ['relatedUsers'])
  },
  methods: {
    ...mapActions('auth', ['deleteAccount']),
    async revokeAccess(user) {
      this.loader = true;
      const payload = {
        userId: user.id
      }
      console.log('User Id:', payload)
      try {
        await this.$axios.$post('/api/portal-admin/key-admin/legal-app/access-key/revoke}', payload);
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        setTimeout(() => {
          this.$nuxt.refresh()
          this.loader = false;
        }, 1500)
      }
    }
  }
};


</script>

<style scoped>

</style>
