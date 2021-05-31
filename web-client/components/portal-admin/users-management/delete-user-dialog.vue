<template>
  <v-dialog v-model="dialog" :value="selectedUser" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-2" icon v-bind="attrs" v-on="on">
        <v-icon medium color="error">mdi-delete</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">Delete User</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteUser">
          Delete
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
  name: "delete-user-dialog",
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
    ...mapActions('admin-panel-store', ['getUsers']),
    async deleteUser() {
      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;

      try {
        let response = await this.$axios.$post(`/api/portal-admin/user-admin/user/delete`, this.form);
        console.warn("Delete log response", response);
      } catch (error) {
        console.error("deleteUser - Error", error);
      } finally {
        this.getUsers();
        this.loading = false;
        this.dialog = false;
      }
    }
  }
};
</script>

<style scoped>

</style>
