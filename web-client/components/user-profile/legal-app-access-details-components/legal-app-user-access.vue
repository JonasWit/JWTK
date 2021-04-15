<template xmlns="">
  <div>
    <v-row v-if="!loading">
      <v-col class="d-flex align-start" cols="12" md="6">
        <div>
          <v-card elevation="0">
            <v-card-title class="mb-2">Uzytkownicy</v-card-title>
            <v-card-subtitle>Jako Administrator możesz zarządzać dostępem do danych dla poszczególnych użytkowników
            </v-card-subtitle>
            <v-select class="px-4" no-data-text="Brak danych" clearable
                      :item-text="item => item.username +' - '+ item.email" v-model="selectedUser" :items="normalUsers"
                      filled label="Wybierz Uzytkownika" return-object></v-select>
            <div class="d-flex flex-column">
              <v-btn class="mb-1" text color="success" @click="reset">Odśwież</v-btn>
            </div>
            <div class="d-flex flex-column" v-if="this.selectedUser && this.treeViewData.length > 0">
              <default-confirmation-dialog v-on:action-confirmed="updateAccess" title="Zmiana Dostępów"
                                           button-text="Zapisz Zmiany"
                                           message="Czy na pewno chcesz zmienić zakres dostępu tego użytkownika?"/>
              <default-confirmation-dialog v-on:action-confirmed="grantFullAccess" title="Pełny Dostęp"
                                           button-text="Nadaj Pełny Dostęp"
                                           message="Uzytkownik otrzyma dostęp do wszyskich danych Klientów i Spraw, które są obecnie wprowadzone w aplikacji!"/>
              <default-confirmation-dialog button-color="error" v-on:action-confirmed="revokeAllAccesses"
                                           title="Obierz Dostęp" button-text="Obierz dostęp"
                                           message="Użytkownikowi zostanią odebrane waszystkie dostępy do danych, które obecnie posiada!"/>
            </div>
          </v-card>
        </div>
      </v-col>
      <v-col class="d-flex align-start" cols="12" md="6">
        <div>
          <v-card elevation="0">
            <v-card-title class="mb-2">Dostęp do danych</v-card-title>
            <v-card-subtitle>Określ do których Klientów i Spraw użytkownik zwyczajny będzie miał dostęp
            </v-card-subtitle>
            <div class="my-3" v-if="this.selectedUser && this.treeViewData.length > 0">
              <div>
                <v-card-actions class="pt-0">

                </v-card-actions>
              </div>
              <div>
                <v-treeview color="warning" item-children="cases" v-model="treeViewSelection" :items="treeViewData"
                            item-key="key" :selection-type="selectionType" selectable return-object>
                  <template v-slot:label="{ item, open }">
                    <v-tooltip bottom>
                      <template v-slot:activator="{ on, attrs }">
                        <span v-bind="attrs" v-on="on">{{ item.displayText }}</span>
                      </template>
                      <span>{{ item.name }}</span>
                    </v-tooltip>
                  </template>
                </v-treeview>
              </div>
            </div>
            <div class="px-4" v-else>
              <p class="success--text">Wybierz Użytkownika by zmienić jego dostęp do danych </p>
            </div>
          </v-card>
        </div>
      </v-col>
    </v-row>
    <v-row v-else>
      <v-col class="d-flex justify-center align-center mt-2" cols="12">
        <v-progress-circular :size="50" :width="7" indeterminate/>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import {mapActions, mapGetters} from "vuex";
import {LegalAppDataAccessItems} from "@/data/enums";
import DefaultConfirmationDialog from "@/components/default-confirmation-dialog";

export default {
  name: "legal-app-user-access",
  components: {DefaultConfirmationDialog},
  data: () => ({
    displayTextSize: 20,
    loading: false,
    selectedUser: null,
    treeViewData: [],
    treeViewSelection: [],
    selectionType: 'independent',
  }),
  watch: {
    selectedUser(selection) {
      if (selection) {
        this.treeViewSelection = [];
        if (this.treeViewData.length > 0) {
          if (selection.dataAccess) {
            if (selection.dataAccess.length > 0) {
              const casesToSelect = selection.dataAccess
                .filter(x => x.restrictedType === LegalAppDataAccessItems.CASE);
              const clientsToSelect = selection.dataAccess
                .filter(x => x.restrictedType === LegalAppDataAccessItems.CLIENT);

              let itemsToPush = [];

              this.treeViewData.forEach(cl => {
                cl.cases.forEach(cs => {
                  if (casesToSelect.some(z => z.itemId === cs.id)) {
                    itemsToPush.push(cs);
                  }
                });
              });

              this.treeViewData.forEach(cl => {
                if (clientsToSelect.some(z => z.itemId === cl.id)) {
                  itemsToPush.push(cl);
                }
              });
              this.treeViewSelection = this.treeViewSelection.concat([...new Set(itemsToPush)]);
            }
          }
        }
      }
    },
    treeViewSelection(selection) {
      if (this.selectedUser) {
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
    }
  },
  fetch() {
    this.loading = true;
    this.getClients();
    this.loading = false;
  },
  beforeMount() {
    this.loading = true;
    this.getClients();
    this.loading = false;
  },
  computed: {
    ...mapGetters('profile-panel-legal-app-store', ['relatedUsers', 'normalUsers']),
  },
  methods: {
    ...mapActions('popup', ['success']),
    ...mapActions('profile-panel-legal-app-store', ['getRelatedUsers']),
    getClients() {
      this.loading = true;
      return this.$axios.$get("/api/legal-app-clients/admin/flat")
        .then((clients) => {
          let response = clients.map(client => ({...client, key: `client-${client.id}`}));
          response.forEach(client => {
            client.displayText = `${client.name.substring(0, this.displayTextSize)}...`;
            client.cases = client.cases.map(c => ({
              ...c, key: `case-${c.id}`,
              displayText: `${c.name.substring(0, this.displayTextSize)}...`
            }));
          });
          this.treeViewData = response;
        })
        .catch(() => {
        })
        .finally(() => {
          this.loading = false;
        });
    },
    reset() {
      Object.assign(this.$data, this.$options.data.call(this));
      this.getUsersAndClients();
    },
    getUsersAndClients() {
      this.getRelatedUsers();
      this.getClients();
    },
    updateAccess() {
      this.loading = true;

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
      return this.$axios.$post("/api/legal-app-admin/update-legal-app-data-access", payload)
        .then(() => {
          this.$notifier.showSuccessMessage("Zmieniono dotępy!");
          Object.assign(this.$data, this.$options.data.call(this));
          this.getUsersAndClients();
        })
        .catch(() => {
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        })
        .finally(() => {
          this.loading = false;
        });
    },
    grantFullAccess() {
      this.loading = true;

      const payload = {
        userId: this.selectedUser.id,
      };

      return this.$axios.$post("/api/legal-app-admin/full-legal-app-data-access", payload)
        .then(() => {
          this.$notifier.showSuccessMessage("Zmieniono dotępy!");
          Object.assign(this.$data, this.$options.data.call(this));
          this.getUsersAndClients();
        })
        .catch(() => {
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        })
        .finally(() => {
          this.loading = false;
        });
    },
    revokeAllAccesses() {
      this.loading = true;

      const payload = {
        userId: this.selectedUser.id,
      };

      return this.$axios.$post("/api/legal-app-admin/revoke-legal-app-data-access", payload)
        .then(() => {
          this.$notifier.showSuccessMessage("Zmieniono dotępy!");
          Object.assign(this.$data, this.$options.data.call(this));
          this.getUsersAndClients();
        })
        .catch(() => {
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>

<style scoped>

</style>
