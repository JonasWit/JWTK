<template>
  <v-dialog :value="selectedUser" persistent width="500">
    <v-card>
      <v-card-title>Applications Access Management</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn v-if="selectedUser.locked" outlined color="warning" text @click="">
          Unlock
        </v-btn>
        <v-btn v-else=outlined color="warning" text @click="">
          Lock
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
    form: {
      userId: ""
    },
  }),
  async fetch() {

  },
  created() {

  },
  methods: {
    async grantLegalAppAccess() {

    },
    async revokeLegalAppAccess() {

    },
    cancelDialog() {
      this.$emit('action-completed');
    }
  },
};
</script>

<style scoped>

</style>
