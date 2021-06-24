<template>
  <v-dialog v-model="dialog" :value="selectedUser" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-2" icon v-bind="attrs" v-on="on">
        <v-icon color="success" v-if="!selectedUser.locked" medium>mdi-lock-open-variant</v-icon>
        <v-icon color="warning" v-else medium>mdi-lock</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center" v-if="selectedUser.locked">Unlock User</v-card-title>
      <v-card-title class="justify-center" v-else>Lock User</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn v-if="selectedUser.locked" color="warning" text @click="unlock">
          Unlock
        </v-btn>
        <v-btn v-else=outlined color="warning" text @click="lock">
          Lock
        </v-btn>
        <v-spacer/>
        <v-btn color="success" text @click="dialog = false">
          Cancel
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "lock-user-dialog",
  props: {
    selectedUser: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loading: false,
    form: {
      userId: ""
    },
  }),
  methods: {
    ...mapActions('portal-admin-store', ['getUsers']),
    async lock() {
      if (this.loading) return;
      this.loading = true;

      try {
        await this.$axios.$post("/api/portal-admin/user-admin/user/lock", this.form);
        this.$notifier.showSuccessMessage("User locked");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getUsers();
        this.loading = false;
        this.dialog = false;
      }
    },
    async unlock() {
      if (this.loading) return;
      this.loading = true;

      try {
        this.form.userId = this.selectedUser.id;
        await this.$axios.$post("/api/portal-admin/user-admin/user/unlock", this.form);
        this.$notifier.showSuccessMessage("User unlocked");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getUsers();
        this.loading = false;
        this.dialog = false;
      }
    },
  }
};
</script>

<style scoped>

</style>
