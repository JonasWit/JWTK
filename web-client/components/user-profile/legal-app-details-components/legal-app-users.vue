<template>
  <div>
    <v-row v-if="!loading" justify="space-around">
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card-title>Uzytkownicy</v-card-title>
          <v-card-subtitle>Jako Administratom możesz zarządzać dostępem do danych dla poszczególnych użytkowników
          </v-card-subtitle>
          <v-select no-data-text="Brak danych" clearable :item-text="item => item.username +' - '+ item.email"
                    v-model="selectedUser" :items="normalUsers" filled label="Wybierz Uzytkownika"
                    return-object></v-select>
        </div>
      </v-col>
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card-title>Dostęp do danych</v-card-title>
          <v-card-subtitle>Określ do których Klientów i Spraw użytkownik zwyczajny będzie miał dostęp
          </v-card-subtitle>
          <div v-if="this.selectedUser">
            <v-select no-data-text="Brak danych" multiple chips clearable :item-text="item => item.name"
                      v-model="selectedClients" :items="clients" filled label="Wybierz Klienta" return-object
                      deletable-chips small-chips></v-select>

            <v-select no-data-text="Brak danych" multiple chips v-if="this.selectedClients" clearable
                      :item-text="item => item.name" v-model="selectedCases" :items="this.cases" filled
                      label="Wybierz Sprawę" return-object deletable-chips small-chips></v-select>

            <div>
              <v-card-actions class="pt-0">
                <v-btn text color="warning" @click="updateAccess">Zmień dostępy</v-btn>
                <v-spacer/>
                <v-btn text color="success" @click="resetState">Odśwież</v-btn>
              </v-card-actions>
            </div>
          </div>
        </div>
      </v-col>
    </v-row>
    <v-row v-else justify="space-around">
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <v-skeleton-loader type="card, actions"></v-skeleton-loader>
      </v-col>
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <v-skeleton-loader type="card, actions"></v-skeleton-loader>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import {mapActions} from "vuex";

export default {
  name: "legal-app-user-statistics",
  data: () => ({
    loading: true,
    selectedUser: null,
    selectedClients: null,
    selectedCases: null,
    relatedUsers: [],
    clients: [],
    cases: []
  }),
  watch: {
    selectedUser(selectedUser) {
      if (selectedUser) {
      }
    },
    selectedClients(selectedClients) {
      this.cases = [];
      selectedClients.forEach(x => this.cases.push(...x.cases));
    }
  },
  beforeMount() {
    this.loading = true;
    this.getRelatedUsers();
    this.getClients();
    this.loading = false;
  },
  computed: {
    normalUsers(state) {
      return state.relatedUsers
        .filter(x => x.role === "Client");
    },
  },
  methods: {
    ...mapActions('popup', ['success']),
    showSnackbar() {

    },
    resetState() {
      this.loading = true;
      this.selectedUser = null;
      this.selectedClients = [];
      this.selectedCases = [];
      this.relatedUsers = [];
      this.getRelatedUsers();
      this.getClients();
      this.loading = false;
    },
    async updateAccess() {
      const payload = {
        userId: this.selectedUser.id,
        allowedClients: this.selectedClients && this.selectedClients.map(x => x.id),
        allowedCases: this.selectedCases && this.selectedCases.map(x => x.id),
      };

      console.log(payload);

      await this.$axios.$post("/api/legal-app-admin/update-legal-app-data-access", payload)
        .then(() => {
          this.$notifier.showSuccessMessage("Zmieniono dotępy!");
        })
        .catch(() => {
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        });
    },
    getRelatedUsers() {
      return this.$axios.$get("/api/legal-app-admin/related-users")
        .then((relatedUsers) => {
          this.relatedUsers = relatedUsers;
        })
        .catch(() => {
        });
    },
    getClients() {
      return this.$axios.$get("/api/legal-app-clients/admin/flat")
        .then((clients) => {
          this.clients = clients;
        })
        .catch(() => {
        });
    },
  }
};
</script>

<style scoped>

</style>
