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
        <v-btn v-if="selectedUser.role === 'Client'" color="warning" text @click="switchRole('ClientAdmin')">
          <v-icon medium color="warning">mdi-arrow-up-thick</v-icon>
          Client Admin
        </v-btn>
        <v-btn v-if="selectedUser.role === 'ClientAdmin'" color="warning" text @click="switchRole('Client')">
          <v-icon medium color="warning">mdi-arrow-down-thick</v-icon>
          Client
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
    ...mapActions('admin-panel-store', ['getUsers']),
    switchRole(role) {
      if (role === this.selectedUser.role) return;

      if (this.loading) return;
      this.loading = true;

      return this.$axios.$post("/api/portal-admin/user-admin/user/change-role", {
        userId: this.selectedUser.id,
        role: role
      })
        .catch((e) => {
          console.log(e);
        }).finally(() => {
          this.loading = false;
          this.getUsers();
          this.dialog = false;
        });
    },
  }
};
</script>

<style scoped>

</style>
