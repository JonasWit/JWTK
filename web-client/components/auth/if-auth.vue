<template>
  <div>
    <slot v-if="authenticated" name="allowed" :user="user" :userAdmin="userAdmin" :portalAdmin="portalAdmin"
          :legalAppAllowed="legalAppAllowed" :legalAppKeyAvailable="legalAppKeyAvailable"
          :medicalAppAllowed="medicalAppAllowed" :medicalAppKeyAvailable="medicalAppKeyAvailable"
          :restaurantAppAllowed="restaurantAppAllowed" :restaurantAppKeyAvailable="restaurantAppKeyAvailable"></slot>
    <slot v-else name="forbidden" :login="login"></slot>
  </div>
</template>

<script>
import {mapActions, mapGetters} from "vuex";

export default {
  name: "if-auth",
  computed: {
    ...mapGetters('auth', ['authenticated', 'userAdmin', 'portalAdmin', 'user', 'legalAppAllowed', 'legalAppKeyAvailable', 'medicalAppAllowed', 'medicalAppKeyAvailable', 'restaurantAppAllowed', 'restaurantAppKeyAvailable']),
  },
  methods: {
    ...mapActions('auth', ['login'])
  },
};
</script>

<style scoped></style>
