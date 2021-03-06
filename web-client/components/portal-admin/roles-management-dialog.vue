<template>
  <v-dialog :value="selectedUser" persistent width="500">
    <v-card>
      <v-card-title>Roles Management</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn v-if="selectedUser.role === 'Client'" outlined color="warning" text @click="switchRole('ClientAdmin')">
          <v-icon medium color="warning">mdi-arrow-up-thick</v-icon>
          Client Admin
        </v-btn>
        <v-btn v-if="selectedUser.role === 'ClientAdmin'" outlined color="warning" text @click="switchRole('Client')">
          <v-icon medium color="warning">mdi-arrow-down-thick</v-icon>
          Client
        </v-btn>
        <v-spacer/>
        <v-btn outlined color="success" text @click="cancelDialog">
          Cancel
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "roles-management-dialog",
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
  computed: {},
  methods: {
    switchRole(role) {
      if (role === this.selectedUser.role) return;

      if (this.loading) return;
      this.loading = true;

      return this.$axios.$post("/api/portal-admin/user/change-role", {
        userId: this.selectedUser.id,
        role: role
      })
        .catch((e) => {
          console.log(e);
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
        });
    },
    cancelDialog() {
      this.$emit('action-completed');
    }
  }
};
</script>

<style scoped>

</style>
