<template>
  <div>
    <v-row justify="space-around">
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card-title>Uzytkownicy</v-card-title>
          <v-card-subtitle>Jako Administratom możesz zarządzać dostępem do danych dla poszczególnych użytkowników
          </v-card-subtitle>
          <v-select clearable :item-text="item => item.username +' - '+ item.email" v-model="selectedUser"
                    :items="normalUsers" filled label="Wybierz Uzytkownika" return-object></v-select>
        </div>
      </v-col>
      <v-col class="d-flex justify-center align-start" cols="12" md="3">
        <div>
          <v-card-title>Dostęp do danych</v-card-title>
          <v-card-subtitle>Określ do których Klientów i Spraw użytkownik zwyczajny będzie miał dostęp
          </v-card-subtitle>
        </div>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import {mapActions, mapGetters, mapState} from "vuex";

export default {
  name: "legal-app-user-statistics",
  data: () => ({
    selectedUser: null
  }),
  watch: {
    selectedUser(selectedUser) {
      console.log(selectedUser);
    }
  },
  computed: {
    ...mapState('profile-panel-store', ['relatedUsers']),
    ...mapGetters('profile-panel-store', ['normalUsers'])
  },
  methods: {
    ...mapActions('profile-panel-store', ['getRelatedUsers'])
  }

};
</script>

<style scoped>

</style>
