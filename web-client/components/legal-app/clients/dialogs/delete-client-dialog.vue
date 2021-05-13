<template>
  <v-dialog v-model="dialog" :value="selectedClient" persistent width="500">
    <template v-slot:activator="{ on, attrs }" #activator="{ on: dialog }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="warning">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń klienta</span>
      </v-tooltip>
    </template>


    <v-card>
      <v-card-title class="justify-center">Usuń klienta</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteClient">
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
  name: "delete-client-dialog",
  props: {
    selectedClient: {
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
    deleteClient() {

    }
  }
}
</script>

<style scoped>

</style>
