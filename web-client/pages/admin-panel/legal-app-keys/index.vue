<template>
  <p>Legal app keys</p>
</template>

<script>
import {mapActions} from "vuex";

export default {
  middleware: ["portal-admin"],
  name: "index",
  async fetch() {
    await this.getAccessKeys();
  },
  methods: {
    ...mapActions('portal-admin-access-keys-store', ['getAccessKeys']),
  }
};
</script>

<style scoped>

</style>
