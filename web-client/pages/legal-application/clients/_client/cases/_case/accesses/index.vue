<template>
  <v-card>
    <h1>accesses</h1>
    <!--    <case-grant-access/>-->
    <v-card-text v-if="allowedUsersForCase.length > 0">
      <v-card-title>Lista użytkowników z dostępem do klienta</v-card-title>
      <v-card flat v-for="item in allowedUsers" :key="item.id">
        <v-row class="d-flex justify-space-between align-center mx-3">
          <v-card-subtitle>
            Nazwa użytkownika: {{ item.username }}
          </v-card-subtitle>
          <v-card-subtitle>Adres email: {{ item.email }}</v-card-subtitle>
          <!--          <revoke-access :user-for-action="item" :client-item-for-action="clientItemForAction"/>-->
        </v-row>
      </v-card>
    </v-card-text>
  </v-card>
</template>

<script>
import {mapActions, mapState} from "vuex";
import CaseGrantAccess from "@/components/legal-app/clients/cases/access-panel/case-grant-access";

export default {
  name: "index",
  components: {CaseGrantAccess},
  async fetch() {
    let caseId = this.$route.params.case
    await this.getAllowedUsersForCase({caseId})
  },
  computed: {
    ...mapState('legal-app-client-store', ['allowedUsersForCase'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getAllowedUsersForCase']),

  }

}
</script>

<style scoped>

</style>
