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
    deleteUser() {

    }
  }
};
</script>

<style scoped>

</style>
