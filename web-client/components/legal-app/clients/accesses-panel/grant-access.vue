<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn x-large color="primary" v-on="{ ...tooltip, ...dialog }">
            Dodaj dostęp
          </v-btn>
        </template>
        <span>Dodaj dostęp</span>
      </v-tooltip>
    </template>
    <v-card min-height="400px">
      <v-toolbar color="primary" dark class="mb-3">
        <v-toolbar-title>
          Nadaj dostęp do klienta
        </v-toolbar-title>
      </v-toolbar>
      <v-select class="mx-2"
                v-model="selectedUsers"
                :items="eligibleUsers"
                item-text="email"
                :menu-props="{ maxHeight: '200' }"
                multiple
                return-object
                small-chips
                deletable-chips
                label="Wybierz użytkownika"
                persistent-placeholder
                outlined
      ></v-select>
      <v-alert v-if="selectedUsers.length === 0" elevation="5" text type="info" dismissible
               close-text="Zamknij">
        Z listy powyżej wybierz użytkowników, którym chcesz nadać dostęp do klienta. Upoważniona osoba będzie miała
        wgląd w dane klienta, rozliczenia i coś jeszcze? Jonasz to confirm :)
      </v-alert>
      <v-list>
        <v-list-item v-for="user in selectedUsers" :key="user.id">
          <v-list-item-content>
            <v-list-item-title>
              Użytkownik: {{ user.username }}
            </v-list-item-title>
            <v-list-item-subtitle>
              Adres email: {{ user.email }}
            </v-list-item-subtitle>
            <v-list-item-subtitle>
              Rola: {{ user.role }}
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn text color="error" @click="dialog=false">
          Anuluj
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn text color="success" @click="grantAccess()">
          Zatwierdź
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {getClientEligibleUsers, grantAccess} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "grant-access",
  props: {
    clientItem: {
      type: Object,
      required: true
    }
  },
  data: () => ({
    selectedUsers: [],
    eligibleUsers: [],
    dialog: false,
    loading: false,
    user: null,
  }),

  async fetch() {
    await this.getEligibleUsersList()
  },

  methods: {
    async getEligibleUsersList() {
      try {
        let clientId = this.clientItem.id;
        this.eligibleUsers = await this.$axios.$get(getClientEligibleUsers(clientId))
        console.warn('eligible users list:', this.eligibleUsers)
      } catch (e) {
        console.warn('error:', e)
      }
    },

    async grantAccess() {
      try {
        let clientId = this.clientItem.id;
        let userId = this.selectedUsers.id;
        console.warn('user id', userId)
        await this.$axios.$post(grantAccess(clientId, userId))
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      } finally {
        this.dialog = false

      }
    }
  }
}
</script>

<style scoped>

</style>
