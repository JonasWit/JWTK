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
          <div class="d-flex justify-center">
            <v-btn text color="success" @click="reset">Odśwież Panel</v-btn>
          </div>
        </div>
      </v-col>
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card-title>Dostęp do danych</v-card-title>
          <v-card-subtitle>Określ do których Klientów i Spraw użytkownik zwyczajny będzie miał dostęp
          </v-card-subtitle>
          <div class="my-3" v-if="this.selectedUser && this.treeViewData.length > 0">
            <div>
              <v-card-actions class="pt-0">
                <default-confirmation-dialog v-on:action-confirmed="updateAccess" title="Zmiana Dostępów"
                                             button-text="Zapisz Zmiany"
                                             message="Czy na pewno chcesz zmienić zakres dostępu tego użytkownika?"/>
              </v-card-actions>
            </div>
            <div>
              <v-treeview color="warning" item-children="cases" v-model="treeViewSelection" :items="treeViewData"
                          item-key="key" :selection-type="selectionType" selectable return-object></v-treeview>
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
    treeViewData: [],
    treeViewSelection: [],
    selectionType: 'independent',
  }),
  watch: {
    selectedUser() {
      this.treeViewSelection = [];
      // this.selectedClients = [];
      // this.selectedCases = [];
    },
    treeViewSelection(selection) {
      let clients = selection.filter(x => x.key.includes('client'));

      const casesWithoutClient = selection
        .filter(x => x.key.includes('case'))
        .filter(cs => !clients.some(cl => cl.cases.some(c => c.id === cs.id)));
      if (casesWithoutClient.length > 0) {
        let clientsToPush = casesWithoutClient.map(caseWithoutClient => {
          return this.treeViewData
            .filter(item => item.key.includes('client'))
            .find(cl => cl.cases.some(cs => cs.id === caseWithoutClient.id));
        });
        this.treeViewSelection = this.treeViewSelection.concat([...new Set(clientsToPush)]);
      }
    }
  },
  beforeMount() {
    this.loading = true;
    this.getRelatedUsers();
    this.getClients();
    this.loading = false;
  },
  computed: {
    normalUsers() {
      return this.relatedUsers
        .filter(x => x.role === "Client");
    },
  },
  methods: {
    ...mapActions('popup', ['success']),
    async getRelatedUsers() {
      await this.$axios.$get("/api/legal-app-admin/related-users")
        .then((relatedUsers) => {
          this.relatedUsers = relatedUsers;
        })
        .catch(() => {
        });
    },
    async getClients() {
      await this.$axios.$get("/api/legal-app-clients/admin/flat")
        .then((clients) => {
          let response = clients.map(client => ({...client, key: `client-${client.id}`}));
          response.forEach(client => {
            client.cases = client.cases.map(c => ({...c, key: `case-${c.id}`}));
          });
          this.treeViewData = response;
        })
        .catch(() => {
        });
    },

    reset() {
      this.loading = true;
      Object.assign(this.$data, this.$options.data.call(this));
      this.getRelatedUsers();
      this.getClients();
      this.loading = false;
    },
    async updateAccess() {
      let accessToClients = this.treeViewSelection
        .filter(x => x.key.includes('client'))
        .map(x => x.id);
      let accessToCases = this.treeViewSelection
        .filter(x => x.key.includes('case'))
        .map(x => x.id);

      const payload = {
        userId: this.selectedUser.id,
        allowedCases: accessToCases,
        allowedClients: accessToClients
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
