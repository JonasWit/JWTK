<template>
  <v-dialog v-model="dialog" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn text depressed color="error" v-bind="attrs" v-on="on">Usuń</v-btn>
    </template>
    <v-card>
      <v-card-title class="error--text justify-center">Usunięcie danych jest trwałe i nieodwracalne!</v-card-title>
      <v-divider/>
      <v-card-actions>
        <v-btn text color="error" @click="deleteData">Usuń</v-btn>
        <v-spacer/>
        <v-btn text color="success" @click="dialog = false">Anuluj</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
  name: "confirm-personal-data-delete",
  data: () => ({
    dialog: false,
    loading: false
  }),
  computed: {
    ...mapState('auth', ['profile']),
  },
  methods: {
    ...mapActions('auth', ['initialize']),
    deleteData() {
      if (this.loading) return;
      this.loading = true;

      return this.$axios.$delete(`/api/users/personal-data/clear/${this.profile.id}`)
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.initialize();
          this.dialog = false;
        });
    }
  }
};
</script>

<style scoped>

</style>
