<template>
  <v-dialog v-model="dialog" :value="selectedUser" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-2" icon v-bind="attrs" v-on="on">
        <v-icon medium color="success">mdi-account-group</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">Roles Management</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn v-if="selectedUser.role === 'User'" color="warning" text @click="switchRole('UserAdmin')">
          <v-icon medium color="warning">mdi-arrow-up-thick</v-icon>
          User Admin
        </v-btn>
        <v-btn v-if="selectedUser.role === 'UserAdmin'" color="warning" text @click="switchRole('User')">
          <v-icon medium color="warning">mdi-arrow-down-thick</v-icon>
          User
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
  name: "roles-management-dialog",
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
  computed: {},
  methods: {
    ...mapActions('portal-admin-store', ['getUsers']),
    async switchRole(role) {
      if (role === this.selectedUser.role) return;
      if (this.loading) return;
      this.loading = true;

      try {
        await this.$axios.$post("/api/portal-admin/user-admin/user/change-role", {
          userId: this.selectedUser.id,
          role: role
        });
        this.$notifier.showSuccessMessage("Role chenged");
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
