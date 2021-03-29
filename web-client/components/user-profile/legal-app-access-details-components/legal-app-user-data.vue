<template>
  <div>
    <v-row v-if="!loading">
      <v-col class="d-flex align-start" cols="12" md="6">
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
                </v-list-item>
              </v-list-item-group>
            </v-list>
          </v-card>
        </div>
      </v-col>
      <v-col class="d-flex align-start" cols="12" md="6">
        <div>
          <v-card elevation="0">
            <v-card-title class="mb-2">Dane Użytkownika</v-card-title>
            <v-card-subtitle>Statystyki i informacje o powiązanym użytkowniku
            </v-card-subtitle>
            <div class="my-3" v-if="selectedUser">
              <v-simple-table class="mb-3" dense>
                <template v-slot:default>
                  <thead>
                  <tr>
                    <th class="text-left">Dostęp</th>
                    <th class="text-left">Ilość</th>
                  </tr>
                  </thead>
                  <tbody>
                  <tr>
                    <td>Klienci</td>
                    <td>{{ countAllowedClients }}</td>
                  </tr>
                  <tr>
                    <td>Sprawy</td>
                    <td>{{ countAllowedCases }}</td>
                  </tr>
                  </tbody>
                </template>
              </v-simple-table>
              <div v-if="selectedUser.lastLogin">
                <v-card-text>
                  <div class="subtitle-1">
                    Ostatnie Logowanie:
                  </div>
                  <div>{{ formatDate(selectedUser.lastLogin) }}</div>
                </v-card-text>
              </div>
            </div>
            <div class="px-4" v-else>
              <p class="success--text">Wybierz Użytkownika z listy by zobaczyć szczegóły</p>
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
import {mapGetters} from "vuex";
import UserHeader from "@/components/user-header";
import {LegalAppDataAccessItems} from "@/data/enums";
import {formatDate} from "@/data/date-extensions";

export default {
  name: "legal-app-user-data",
  components: {UserHeader},
  data: () => ({
    selectedUser: null,
    loading: false
  }),
  computed: {
    ...mapGetters('profile-panel-legal-app-store', ['relatedUsers']),
    countAllowedClients() {
      if (!this.selectedUser) return 0;
      return this.selectedUser.dataAccess.filter(x => x.restrictedType === LegalAppDataAccessItems.CLIENT).length;
    },
    countAllowedCases() {
      if (!this.selectedUser) return 0;
      return this.selectedUser.dataAccess.filter(x => x.restrictedType === LegalAppDataAccessItems.CASE).length;
    }
  },
  methods: {
    formatDate(date) {
      return formatDate(date);
    },
  }
};
</script>

<style scoped>

</style>
