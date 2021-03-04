<template>
  <v-dialog :value="selectedUser" persistent width="500">
    <v-card>
      <v-card-title v-if="selectedUser.locked">Unlock User</v-card-title>
      <v-card-title v-else>Roles Management</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn v-if="selectedUser.locked" outlined color="warning" text @click="unlock">
          Update
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
    form: {
      userId: "",
      role: ""
    }
  }),
  async fetch() {

  },
  methods: {
    switchRole() {
      if (this.loading) return;
      this.loading = true;

      this.dataKeyForm.userId = this.selectedUser.id;

      return this.$axios.$post("/api/admin/user/role", this.form)
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
