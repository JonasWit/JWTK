<template>
  <div>
    <v-dialog v-model="dialog" persistent width="500">
      <template #activator="{ on: dialog }" v-slot:activator="{ on }">
        <v-tooltip bottom>
          <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
            <v-btn color="error" icon v-on="{ ...tooltip, ...dialog }">
              <v-icon>
                mdi-delete
              </v-icon>
            </v-btn>
          </template>
          <span>Usuń dane</span>
        </v-tooltip>
      </template>
      <v-card>
        <v-card-title class="justify-center">Usuń dane</v-card-title>
        <v-alert type="error">Usunięcie danych jest trwałe i nieodwracalne!</v-alert>
        <v-divider/>
        <v-card-actions>
          <v-btn text color="primary" @click="dialog = false">Anuluj</v-btn>
          <v-spacer/>
          <v-btn text color="error" @click="deleteData">Usuń</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <progress-bar v-if="loader"/>
  </div>
</template>
<script>
import {mapActions, mapState} from "vuex";
import {handleError} from "@/data/functions";
import ProgressBar from "@/components/legal-app/progress-bar";

export default {
  name: "confirm-personal-data-delete",
  components: {ProgressBar},
  data: () => ({
    dialog: false,
    loader: false
  }),
  computed: {
    ...mapState('auth', ['profile']),
  },
  methods: {
    ...mapActions('auth', ['initialize']),
    async deleteData() {
      this.loader = true
      try {
        let profileId = this.profile.id
        await this.$axios.$delete(`/api/users/personal-data/clear/${profileId}`)
        this.$notifier.showSuccessMessage("Dane zostały usuniete");
      } catch (error) {
        handleError(error)
      } finally {
        setTimeout(() => {
          this.initialize();
          this.dialog = false;
        }, 1500)
      }
    }
  }
};
</script>

<style scoped>

</style>
