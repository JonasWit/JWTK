<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn medium color="error" v-on="{ ...tooltip, ...dialog }">
            Nadaj dostęp
          </v-btn>
        </template>
        <span>Nadaj dostęp</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-toolbar color="primary" dark class="mb-3">
        <v-toolbar-title>
          Nadaj dostęp do klienta
        </v-toolbar-title>
      </v-toolbar>
      <v-select class="mx-2"
                v-model="selectedUser"
                :items="eligibleUsersForCase"
                item-text="email"
                :menu-props="{ maxHeight: '200' }"
                return-object
                small-chips
                deletable-chips
                label="Wybierz użytkownika"
                persistent-placeholder
                outlined
      ></v-select>
      <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
        Z listy powyżej wybierz użytkownika, któremu chcesz nadać dostęp do klienta. Upoważniona osoba będzie miała
        dostęp do panelu klienta, w którym będzie mogła zarządzać swoimi notatkami, rozliczeniami oraz sprawami.
      </v-alert>
      <v-list>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>
              Użytkownik: {{ selectedUser.username }}
            </v-list-item-title>
            <v-list-item-subtitle>
              Adres email: {{ selectedUser.email }}
            </v-list-item-subtitle>
            <v-list-item-subtitle>
              Rola: {{ selectedUser.role }}
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
import {grantAccess} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import {mapActions, mapState} from "vuex";

export default {
  name: "case-grant-access",
  data: () => ({
    selectedUser: [],
    dialog: false,
    loading: false,
  }),

  async fetch() {
    let caseId = this.$route.params.case
    await this.getEligibleUsersForCase({caseId})
    console.warn('eligible users list:', this.eligibleUsersForCase)
  },

  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['eligibleUsersForCase']),
  },

  methods: {
    ...mapActions('legal-app-client-store', ['getAllowedUsersForCase']),
    ...mapActions('legal-app-client-store', ['getEligibleUsersForCase']),

    async grantAccess() {
      const payload = {
        userId: this.selectedUser.id
      }
      try {
        let caseId = this.$route.params.case
        console.warn('user id', payload)
        await this.$axios.$post(grantAccess(caseId), payload)
        this.$notifier.showSuccessMessage("Dostęp nadany pomyślnie");
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      } finally {
        Object.assign(this.$data, this.$options.data.call(this));
        let caseId = this.$route.params.case
        console.warn('case id:', caseId)
        await this.getAllowedUsersForCase({caseId})
        await this.getEligibleUsersForCase({caseId})
        this.dialog = false

      }
    }
  }
}
</script>

<style scoped>

</style>
