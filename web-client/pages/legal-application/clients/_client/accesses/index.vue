<template>

  <layout>
    <template v-slot:content>
      <v-container>
        <v-card>
          <v-toolbar class="mb-2 white--text" color="primary">
            <v-toolbar-title>Panel dostępów do klienta</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-title>Nazwa klienta: {{ clientDataFromFetch.name }}</v-toolbar-title>
          </v-toolbar>
          <v-alert elevation="5" text type="info" v-if="legalAppTooltips">
            W panelu dostępów możesz nadać lub usunąć dostęp do Klienta. Rzwiń listę, aby zobaczyć listę użytkowników,
            którzy mogą uzyskać dostęp do Klienta. Z listy poniżej wybierz użytkownika, któremu chcesz nadać dostęp do
            klienta. Upoważniona osoba będzie
            miała
            dostęp do panelu klienta, w którym będzie mogła zarządzać swoimi notatkami, rozliczeniami oraz sprawami.
          </v-alert>
          <v-card class="mx-0" flat v-if="userAdmin">
            <v-card-title>Lista użytkowników uprawnionych do uzyskania dostępu</v-card-title>
            <v-select class="mx-5" @change="grantAccess(value)" v-model="value" :items="eligibleUsersList" chips
                      label="Wybierz użytkownika" item-text="email" item-value="email" return-object>

            </v-select>
            <v-card-title v-if="allowedUsersList.length > 0">Lista użytkowników z dostępem do klienta</v-card-title>
            <v-alert v-if="allowedUsersList.length > 0 && legalAppTooltips" elevation="5" text type="info">
              Aby usunąć dostęp dla wybranego użytkonwika kliknij "krzyżyk" obok odpowiedniego adresu email.
            </v-alert>
            <v-chip v-for="(item) in allowedUsersList" :key="item.id" class="mx-5 my-3" close color="red"
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
import Layout from "@/components/legal-app/layout";
import {mapActions, mapGetters, mapState} from "vuex";
import {handleError} from "@/data/functions";
import {grantAccess, revokeAccess} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

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
      let clientId = this.$route.params.client;
      await this.getClientData({clientId});
      await this.getAllowedUsers({clientId});
      await this.getEligibleUsersList({clientId})
      this.allowedUsersList = this.allowedUsers
      this.eligibleUsersList = this.eligibleUsers
    } catch (error) {
      handleError(error);
    }
  },

  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['clientDataFromFetch']),
    ...mapGetters('auth', ['userAdmin']),
    ...mapGetters('legal-app-client-store', ['allowedUsers', 'eligibleUsers'])

  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientData', 'getEligibleUsersList', 'getAllowedUsers']),
    async grantAccess(item) {
      const payload = {
        userId: item.id
      }
      try {
        let clientId = this.$route.params.client;
        await this.$axios.$post(grantAccess(clientId), payload)
        this.$notifier.showSuccessMessage("Dostęp nadany pomyślnie");
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
        let clientId = this.$route.params.client
        await this.$axios.$post(revokeAccess(clientId), payload)
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
        this.value = null
      } catch (error) {
        handleError(error);
      } finally {
        this.$nuxt.refresh()

      }
    }
  }
};
</script>

<style scoped>

</style>
