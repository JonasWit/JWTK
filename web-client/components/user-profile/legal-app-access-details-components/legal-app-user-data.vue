<template>
  <div>
    <v-row v-if="!loading" justify="space-around">
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card>
            <v-card-title class="mb-2">Powiązani Użytkownicy</v-card-title>
            <v-card-subtitle>Jako Administrator możesz zarządzać dostępem do danych dla poszczególnych użytkowników
            </v-card-subtitle>
            <v-list style="max-height: 300px" class="overflow-y-auto">
              <v-list-item-group v-model="selectedUser" return-object>
                <v-list-item v-for="(user, i) in relatedUsers" :key="i">
                  <user-header :image-url="user.image" :username="user.username" :role="user.role" :link="false"
                               size="36"/>
                </v-list-item>
              </v-list-item-group>
            </v-list>
          </v-card>
        </div>
      </v-col>
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card>
            <v-card-title class="mb-2">Dane Użytkownika</v-card-title>
            <v-card-subtitle>Statystyki i informacje o powiązanym użytkowniku
            </v-card-subtitle>
            <div class="my-3" v-if="selectedUser >= 0">
              <v-simple-table dense>
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
            </div>
            <div class="px-4" v-else>
              <p class="success--text">Wybierz Użytkownika z listy by zobaczyć szczegóły</p>
            </div>
          </v-card>
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
import {mapGetters} from "vuex";
import UserHeader from "@/components/user-header";
import {LegalAppDataAccessItems} from "@/data/enums";

export default {
  name: "legal-app-user-data",
  components: {UserHeader},
  data: () => ({
    selectedUser: null,
    loading: false
  }),
  watch: {
    selectedUser(selection) {
      console.log(selection);
    }
  },
  beforeMount() {
  },
  computed: {
    ...mapGetters('profile-panel-legal-app-store', ['relatedUsers']),
    countAllowedClients() {
      if (!this.selectedUser) return 0;
      return this.selectedUser.dataAccess.filter(x => x.restrictedType === LegalAppDataAccessItems.CLIENT);
    },
    countAllowedCases() {
      if (!this.selectedUser) return 0;
      return this.selectedUser.dataAccess.filter(x => x.restrictedType === LegalAppDataAccessItems.CASE);
    }
  },
  methods: {}
};
</script>

<style scoped>

</style>
