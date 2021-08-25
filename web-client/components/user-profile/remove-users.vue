<template>
  <div>
    <v-card flat class="mt-2">
      <v-toolbar color="primary" dense>
        <v-toolbar-title class="white--text">Usunięcie powiązanych użytkowników</v-toolbar-title>
      </v-toolbar>
      <v-col class="d-flex justify-center" cols="12">
        <div class="d-flex flex-column">
          <v-card-subtitle>
            Możesz usunąc powiązanych użytkowników. Ta akcja jest nieodwracalna i spowoduje utratę dostępu do usług
            związanych z portalem. W celu ponownego uzyskania dostępu konieczne będzie przesłanie prośby o utworzenie
            nowego konta i otrzymanie nowego zaproszenia.
          </v-card-subtitle>
          <v-divider></v-divider>
          <v-card-text v-if="relatedUsers.length > 0">
            <v-card-title>Lista powiązanych użytkowników</v-card-title>
            <v-card flat v-for="item in relatedUsers" :key="item.id">
              <v-row class="d-flex justify-space-between align-center mx-3">
                <v-card-subtitle>
                  Nazwa użytkownika: {{ item.username }}
                </v-card-subtitle>
                <v-card-subtitle>Adres email: {{ item.email }}</v-card-subtitle>
                <revoke-access-related-users :user-for-action="item"/>
              </v-row>
            </v-card>
          </v-card-text>

        </div>
      </v-col>
    </v-card>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import AllowedUsers from "@/components/legal-app/clients/accesses-panel/allowed-users";
import RevokeAccessRelatedUsers from "@/components/portal-admin/users-management/revoke-access-related-users";

export default {
  name: "remove-users",
  components: {RevokeAccessRelatedUsers, AllowedUsers},
  computed: {
    ...mapState('auth', ['relatedUsers'])
  },
  methods: {
    ...mapActions('auth', ['deleteAccount'])
  }
};


</script>

<style scoped>

</style>
