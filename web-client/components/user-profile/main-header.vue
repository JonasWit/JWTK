<template>
  <v-list-item class="my-5">
    <v-list-item-icon>
      <v-icon x-large>
        mdi-account-circle
      </v-icon>
    </v-list-item-icon>
    <v-list-item-content>
      <v-list-item-title class="title">
        {{ profile.username }}
      </v-list-item-title>
      <v-list-item-subtitle>{{ userRole }}</v-list-item-subtitle>
    </v-list-item-content>
  </v-list-item>
</template>

<script>
import {mapActions, mapGetters, mapState} from "vuex";

export default {
  name: "main-header",
  data: () => ({
    uploadingImage: false,
  }),
  computed: {
    ...mapState("auth", ["profile"]),
    ...mapGetters("auth", ["userRole"]),
  },
  methods: {
    ...mapActions("auth", ["initialize"]),
  },
};
</script>

<style scoped></style>
