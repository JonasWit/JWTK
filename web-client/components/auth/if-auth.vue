<template>
  <div>
    <slot v-if="authenticated" name="allowed" :clientAdmin="clientAdmin" :portalAdmin="portalAdmin"
          :legalAppAllowed="legalAppAllowed" :legalAppKeyAvailable="legalAppKeyAvailable" :client="client"></slot>
    <slot v-else name="forbidden" :login="login"></slot>
  </div>
</template>

<script>
import {mapActions, mapGetters} from "vuex";

export default {
  name: "if-auth",
  computed: {
    ...mapGetters('auth', ['authenticated', 'clientAdmin', 'portalAdmin', 'client', 'legalAppAllowed', 'legalAppKeyAvailable']),
  },
  methods: {
    ...mapActions('auth', ['login'])
  },
};
</script>

<style scoped></style>
