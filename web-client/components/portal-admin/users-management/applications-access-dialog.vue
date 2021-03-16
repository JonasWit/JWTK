<template>
  <v-dialog v-model="dialog" :value="selectedUser" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-3" icon v-bind="attrs" v-on="on">
        <v-icon medium color="success">mdi-application-cog</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">Applications Access Management</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn v-if="selectedUser.legalAppAllowed" color="warning" text @click="revokeLegalAppAccess">
          Revoke Legal App Access
        </v-btn>
        <v-btn v-else=outlined color="warning" text @click="grantLegalAppAccess">
          Grant Legal App Access
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
  name: "applications-access-dialog",
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
  }),
  methods: {
    ...mapActions('admin-panel-store', ['getUsers']),
    async grantLegalAppAccess() {
      if (this.loading) return;
      this.loading = true;

      await this.$axios.$post("/api/portal-admin/user-admin/user/grant/legal-app", {userId: this.selectedUser.id})
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.getUsers();
          this.dialog = false;
        });
    },
    async revokeLegalAppAccess() {
      if (this.loading) return;
      this.loading = true;

      await this.$axios.$post("/api/portal-admin/user-admin/user/revoke/legal-app", {userId: this.selectedUser.id})
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.getUsers();
          this.dialog = false;
        });
    },
  },
};
</script>

<style scoped>

</style>
