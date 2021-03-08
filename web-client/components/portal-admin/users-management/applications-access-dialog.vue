<template>
  <v-dialog :value="selectedUser" persistent width="500">
    <v-card>
      <v-card-title>Applications Access Management</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn v-if="selectedUser.legalAppAllowed" color="warning" text @click="revokeLegalAppAccess">
          Revoke Legal App Access
        </v-btn>
        <v-btn v-else=outlined color="warning" text @click="grantLegalAppAccess">
          Grant Legal App Access
        </v-btn>
        <v-spacer/>
        <v-btn color="success" text @click="cancelDialog">
          Cancel
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
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
    loading: false,
  }),
  methods: {
    async grantLegalAppAccess() {
      if (this.loading) return;
      this.loading = true;

      return this.$axios.post("/api/portal-admin/user/grant/legal-app", {userId: this.selectedUser.id})
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
        });
    },
    async revokeLegalAppAccess() {
      if (this.loading) return;
      this.loading = true;

      return this.$axios.post("/api/portal-admin/user/revoke/legal-app", {userId: this.selectedUser.id})
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
        });
    },
    cancelDialog() {
      this.$emit('action-completed');
    }
  },
};
</script>

<style scoped>

</style>
