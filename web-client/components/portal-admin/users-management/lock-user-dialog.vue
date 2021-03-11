<template>
  <v-dialog :value="selectedUser" persistent width="500">
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
        <v-btn color="success" text @click="cancelDialog">
          Cancel
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
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
    loading: false,
    form: {
      userId: ""
    },
  }),
  created() {
    if (this.selectedUser.dataAccessKey) {
      this.form.dataAccessKey = this.selectedUser.dataAccessKey;
    }
  },
  methods: {
    lock() {
      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;

      return this.$axios.$post("/api/portal-admin/user-admin/user/lock", this.form)
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
        });
    },
    unlock() {
      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;

      return this.$axios.$post("/api/portal-admin/user-admin/user/unlock", this.form)
        .catch((e) => {
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
