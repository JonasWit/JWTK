<template>
  <v-dialog v-model="dialog" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn text color="warning" v-bind="attrs" v-on="on">{{ buttonText }}</v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">{{ title }}</v-card-title>
      <v-card-text class="my-1">
        {{ message }}
      </v-card-text>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="warning" text @click="confirm">
          Potwierdzam
        </v-btn>
        <v-spacer/>
        <v-btn color="success" text @click="dialog = false">
          Anuluj
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "default-confirmation-dialog",
  props: {
    message: {
      required: true,
      type: String,
      default: "No message!"
    },
    title: {
      required: true,
      type: String,
      default: "No Title!"
    },
    buttonText: {
      required: true,
      type: String,
      default: "No Button Text!"
    }
  },
  data: () => ({
    dialog: false,
  }),
  methods: {
    ...mapActions('admin-panel-store', ['getUsers']),
    confirm() {
      this.$emit('action-confirmed');
      this.dialog = false;
    }
  }
};
</script>

<style scoped>

</style>
