<template>
  <div>
    <slot v-if="authenticated" name="allowed" :userAdmin="userAdmin" :portalAdmin="portalAdmin"
          :legalAppAllowed="legalAppAllowed" :legalAppKeyAvailable="legalAppKeyAvailable" :user="user"></slot>
    <slot v-else name="forbidden" :login="login"></slot>
  </div>
</template>

<script>
import {mapActions, mapGetters} from "vuex";

export default {
  name: "if-auth",
  computed: {
    ...mapGetters('auth', ['authenticated', 'userAdmin', 'portalAdmin', 'user', 'legalAppAllowed', 'legalAppKeyAvailable']),
  },
  methods: {
    ...mapActions('auth', ['login'])
  },
};
</script>

<style scoped></style>
