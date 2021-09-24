<template>
  <layout>
    <template v-slot:content>
      <v-container>
        <v-card>
          <v-toolbar class="mb-2 white--text" color="primary">
            <v-toolbar-title>Panel dostępów do sprawy</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-title>Tytuł sprawy: {{ clientCaseDetails.name }}</v-toolbar-title>
          </v-toolbar>
          <v-alert elevation="5" text type="info" v-if="legalAppTooltips">
            W panelu dostępów możesz nadać lub usunąć dostęp do Sprawy. Rzwiń listę, aby zobaczyć listę użytkowników,
            którzy mogą uzyskać dostęp do Sprawy. Z listy poniżej wybierz użytkownika, któremu chcesz nadać dostęp.
          </v-alert>
          <v-card class="mx-2" flat v-if="userAdmin">
            <v-card-title>Lista użytkowników uprawnionych do uzyskania dostępu</v-card-title>
            <v-select @change="grantAccess(value)" v-model="value" :items="eligibleUsersList" chips
                      label="Wybierz użytkownika" item-text="email" item-value="email" return-object
            >

            </v-select>
            <v-card-title v-if="allowedUsersList.length > 0">Lista użytkowników z dostępem do klienta</v-card-title>
            <v-alert v-if="allowedUsersList.length > 0 && legalAppTooltips" elevation="5" text type="info">
              Aby usunąć dostęp dla wybranego użytkonwika kliknij "krzyżyk" obok odpowiedniego adresu email.
            </v-alert>
            <v-chip v-for="(item) in allowedUsersList" :key="item.id" class="ma-2" close color="red"
                    text-color="white" @click:close="revokeAccess(item)">
              {{ item.email }}
            </v-chip>


          </v-card>
        </v-card>

      </v-container>
    </template>
  </layout>
</template>
<script>
import {mapActions, mapGetters, mapState} from "vuex";
import Layout from "@/components/legal-app/layout";
import {handleError} from "@/data/functions";
import {grantAccess, revokeAccess} from "@/data/endpoints/legal-app/legal-app-case-endpoints";

export default {
  name: "index",
  components: {Layout},
  middleware: ['legal-app-permission', 'user-admin', 'authenticated'],

  data: () => ({
    value: null,
    allowedUsersList: [],
    eligibleUsersList: []
  }),
  async fetch() {
    try {
      let caseId = this.$route.params.case
      await this.getCaseDetails({caseId})
      await this.getEligibleUsersForCase({caseId})
      await this.getAllowedUsersForCase({caseId})
      this.allowedUsersList = this.allowedUsersForCase
      console.log('allowed users', this.allowedUsersList)
      this.eligibleUsersList = this.eligibleUsersForCase
      console.log('eligible users', this.eligibleUsersList)
    } catch (error) {
      handleError(error);
    }
  },

  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['allowedUsersForCase', 'clientCaseDetails', 'eligibleUsersForCase']),
    ...mapGetters('auth', ['userAdmin']),
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getAllowedUsersForCase', 'getCaseDetails', 'getEligibleUsersForCase']),
    async grantAccess(item) {
      const payload = {
        userId: item.id
      }
      try {
        let caseId = this.$route.params.case
        console.warn('user id', payload)
        await this.$axios.$post(grantAccess(caseId), payload)
      } catch (error) {
        handleError(error)
      } finally {
        this.$nuxt.refresh()
      }
    },
    async revokeAccess(item) {
      const payload = {
        userId: item.id
      }
      try {
        let caseId = this.$route.params.case
        await this.$axios.$post(revokeAccess(caseId), payload)
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        this.$nuxt.refresh()

      }
    }
  }

}
</script>

<style scoped>

</style>
