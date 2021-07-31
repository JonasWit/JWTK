<template>
  <div class="d-flex align-center" :class="{'flex-row-reverse': reverse}">
    <div class="d-flex align-center">
      <v-avatar :size="size">
        <img v-if="imageUrl" :src="imageUrl">
        <v-icon v-else>mdi-account</v-icon>
      </v-avatar>
      <div>
        <div class="mx-3" v-if="username">
          {{ username }}
        </div>
        <v-card-subtitle class="py-1 px-3" v-if="role">{{ convertRole(role) }}</v-card-subtitle>
      </div>
    </div>
    <v-spacer></v-spacer>
    <slot name="append"></slot>
  </div>
</template>

<script>
import {ROLES, ROLES_POLISH} from "@/data/enums";

export default {
  name: "user-header",
  props: {
    imageUrl: {
      required: false,
      type: String
    },
    username: {
      required: false,
      type: String
    },
    size: {
      required: false,
      type: String,
      default: '42'
    },
    reverse: {
      required: false,
      type: Boolean,
      default: false
    },
    link: {
      required: false,
      type: Boolean,
      default: true
    },
    role: {
      required: false,
      type: String,
    }
  },
  methods: {
    convertRole(role) {
      if (role === ROLES.USER_ADMIN) {
        return ROLES_POLISH.CLIENT_ADMIN;
      }
      if (role === ROLES.USER) {
        return ROLES_POLISH.CLIENT;
      }
      if (role === ROLES.INVITED) {
        return ROLES_POLISH.INVITED;
      }
      if (role === ROLES.PORTAL_ADMIN) {
        return ROLES_POLISH.PORTAL_ADMIN;
      }
    }
  }
};
</script>

<style scoped></style>
