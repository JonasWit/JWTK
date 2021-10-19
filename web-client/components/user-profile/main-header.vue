<template>
  <v-list-item class="my-5">
    <v-list-item-icon>
      <v-list-item-avatar>
        <v-avatar color="orange" size="56" class="white--text">
          {{ initials(profile) }}
        </v-avatar>
      </v-list-item-avatar>
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
    initials(profile) {
      if (profile.name && profile.surname) {
        return profile.name[0][0].toUpperCase() + profile.surname[0][0].toUpperCase()
      } else
        return profile.username[0][0].toUpperCase()
    },
  },
};
</script>

<style scoped></style>
