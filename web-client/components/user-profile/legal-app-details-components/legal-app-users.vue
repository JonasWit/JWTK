<template>
  <div>
    <v-row v-if="!loading" justify="space-around">
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card-title>Uzytkownicy</v-card-title>
          <v-card-subtitle>Jako Administratom możesz zarządzać dostępem do danych dla poszczególnych użytkowników
          </v-card-subtitle>
          <v-select class="px-4" no-data-text="Brak danych" clearable
                    :item-text="item => item.username +' - '+ item.email" v-model="selectedUser" :items="normalUsers"
                    filled label="Wybierz Uzytkownika" return-object></v-select>
        </div>
      </v-col>
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card-title>Dostęp do danych</v-card-title>
          <v-card-subtitle>Określ do których Klientów i Spraw użytkownik zwyczajny będzie miał dostęp
          </v-card-subtitle>
          <div class="my-3" v-if="this.selectedUser">
            <div class="px-4">
              <h4>Lista Klientów</h4>
            </div>
            <div>
              <v-treeview color="warning" item-children="cases" v-model="selection" :items="clients" item-key="key"
                          :selection-type="selectionType" selectable return-object></v-treeview>


            </div>
            <div>
              <v-card-actions class="pt-0">
                <v-btn text color="warning" @click="updateAccess">Zmień dostępy</v-btn>
                <v-spacer/>
                <v-btn text color="success" @click="reset">Odśwież</v-btn>
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
  name: "legal-app-users",
  data: () => ({
    loading: false,
    selectedUser: null,
    relatedUsers: [],
    clients: [],
    selection: [],
    selectionType: 'leaf',
  }),
  watch: {
    selectedUser() {
      this.selectedClients = [];
      this.selectedCases = [];
    },
    selection(selection) {
      console.log('selected data:', selection);
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
      return this.relatedUsers
        .filter(x => x.role === "Client");
    },
    selectedClientsCases() {
      if (this.selectedClients) {
        let availableCases = [];
        this.clients.filter(client => this.selectedClients.some(clientId => clientId === client.id))
          .forEach(client => availableCases.push(client.cases.map(c => ({
            id: c.id,
            name: c.name,
            clientName: client.name
          }))));
        return [].concat.apply([], availableCases);
      }
    }
  },
  methods: {
    ...mapActions('popup', ['success']),
    getRelatedUsers() {
      return this.$axios.$get("/api/legal-app-admin/related-users")
        .then((relatedUsers) => {
          this.relatedUsers = relatedUsers;
        })
        .catch(() => {
        });
    },
    getClients() {
      console.log('Getting clients');
      return this.$axios.$get("/api/legal-app-clients/admin/flat")
        .then((clients) => {
          let response = clients.map(client => ({...client, key: `client-${client.id}`}));
          response.forEach(client => {
            client.cases = client.cases.map(c => ({...c, key: `case-${c.id}`}));
          });
          this.clients = response;
        })
        .catch(() => {
        });
    },

    reset() {
      Object.assign(this.$data, this.$options.data.call(this));
    },
    async updateAccess() {
      console.log('selected cases:', this.selectedCases);
      console.log('selected clients:', this.selectedClients);

      const payload = {
        userId: this.selectedUser.id,
        allowedClients: this.selectedClients && this.selectedClients.map(x => x),
        allowedCases: this.selectedCases && this.selectedCases.map(x => x.id),
      };

      console.log('payload:', payload);

      // await this.$axios.$post("/api/legal-app-admin/update-legal-app-data-access", payload)
      //   .then(() => {
      //     this.$notifier.showSuccessMessage("Zmieniono dotępy!");
      //   })
      //   .catch(() => {
      //     this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
      //   });
    },
  }
};
</script>

<style scoped>

</style>
