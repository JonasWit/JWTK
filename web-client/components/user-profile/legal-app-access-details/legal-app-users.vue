<template>
  <div>
    <v-row v-if="!loading">
      <v-col class="d-flex justify-start" cols="12">
        <div>
          <v-card elevation="0">
            <v-card-title class="pb-0">Powiązani</v-card-title>
            <v-card-title class="pt-0 mb-2">Użytkownicy</v-card-title>
            <v-card-subtitle>Jako Administrator możesz zarządzać dostępem do danych dla poszczególnych użytkowników
            </v-card-subtitle>
            <v-list style="max-height: 300px" class="overflow-y-auto">
              <v-list-item-group v-model="selectedUser" return-object>
                <v-list-item v-for="(user, i) in relatedUsers" :key="i" :value="user">
                  <user-header :image-url="user.image" :username="user.username" :role="user.role" :link="false"
                               size="36"/>
                  <v-spacer/>
                  <div v-if="selectedUser && selectedUser.id === user.id && standardUsetCheck">
                    <default-confirmation-dialog v-on:action-confirmed="updateAccess" title="Zmiana Dostępów"
                                                 icon="mdi-content-save-edit-outline"
                                                 tooltip-message="Zapisz zmiany dostępów" button-text="Zapisz Zmiany"
                                                 message="Czy na pewno chcesz zmienić zakres dostępu tego użytkownika?"/>

                    <default-confirmation-dialog button-color="error" v-on:action-confirmed="revokeAllAccesses"
                                                 icon="mdi-lock-minus-outline"
                                                 tooltip-message="Odbierz wszystkie dostępy do danych"
                                                 title="Obierz Dostęp" button-text="Obierz dostęp"
                                                 message="Użytkownikowi zostanią odebrane waszystkie dostępy do danych, które obecnie posiada!"/>
                    <default-confirmation-dialog v-on:action-confirmed="grantFullAccess" title="Pełny Dostęp"
                                                 button-color="error" tooltip-message="Nadaj pełny dostęp do danych"
                                                 icon="mdi-lock-open-plus-outline" button-text="Nadaj Pełny Dostęp"
                                                 message="Uzytkownik otrzyma dostęp do wszyskich danych Klientów i Spraw, które są obecnie wprowadzone w aplikacji!"/>
                    <default-confirmation-dialog v-on:action-confirmed="testMethod" title="Usunięcie powiązania"
                                                 button-color="error" tooltip-message="Usuń użytkownika z grupy"
                                                 icon="mdi-account-off-outline" button-text="Nadaj Pełny Dostęp"
                                                 message="Użytkownik nie będzie mógł mieć dostępu do żadnych danych, ponowne dopięcie uzytkownika może nastąpić jedynie po kontakcie z administratorem portalu!"/>
                  </div>
                </v-list-item>
              </v-list-item-group>
            </v-list>
          </v-card>
        </div>
      </v-col>
      <v-col class="d-flex justify-start" cols="12">
        <div>
          <v-card elevation="0" v-if="selectedUser">
            <v-card-title class="mb-2">Dostęp do danych</v-card-title>
            <v-card-subtitle v-if="selectedUser && !standardUsetCheck">Administrator ma automatyczny dostę do wszystkich
              danych.
            </v-card-subtitle>
            <v-card-subtitle v-else>Określ do których Klientów i Spraw użytkownik
              będzie miał dostęp.
            </v-card-subtitle>
            <div class="my-3" v-if="this.selectedUser && this.treeViewData.length > 0 && standardUsetCheck">
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
          </v-card>
        </div>
      </v-col>
    </v-row>
    <v-row v-else justify="space-around">
      <v-col class="d-flex justify-center align-center mt-2" cols="12">
        <v-progress-circular :size="50" :width="7" indeterminate/>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import {mapActions, mapGetters} from "vuex";
import UserHeader from "@/components/user-header";
import DefaultConfirmationDialog from "@/components/default-confirmation-dialog";
import {LegalAppDataAccessItems, ROLES} from "@/data/enums";
import {formatDate} from "@/data/date-extensions";

export default {
  name: "legal-app-users",
  components: {UserHeader, DefaultConfirmationDialog},
  data: () => ({
    selectedUser: null,
    loading: false,
    displayTextSize: 20,
    treeViewData: [],
    treeViewSelection: [],
    selectionType: 'independent',
  }),
  watch: {
    selectedUser(selection) {
      this.loading = true;
      console.warn('selected user:', selection);
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
      this.loading = false;
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
    this.getRelatedUsers();
    this.loading = false;
  },
  computed: {
    ...mapGetters('profile-panel-legal-app-store', ['relatedUsers']),
    countAllowedClients() {
      if (!this.selectedUser) return 0;
      return this.selectedUser.dataAccess.filter(x => x.restrictedType === LegalAppDataAccessItems.CLIENT).length;
    },
    standardUsetCheck() {
      if (this.selectedUser.role !== ROLES.CLIENT_ADMIN &&
        this.selectedUser.role !== ROLES.PORTAL_ADMIN &&
        this.selectedUser.role !== ROLES.INVITED) {
        return true;
      }
    },
    countAllowedCases() {
      if (!this.selectedUser) return 0;
      return this.selectedUser.dataAccess.filter(x => x.restrictedType === LegalAppDataAccessItems.CASE).length;
    }
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
          //this.selectedUser = this.normalUsers.find(x => x.id === payload.userId);
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
    },
    testMethod() {
      console.warn('test', 'test');
    },
    formatDate(date) {
      return formatDate(date);
    },
  }
};
</script>

<style scoped>

</style>
