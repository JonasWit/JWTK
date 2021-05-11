<template>
  <div class="ma-3">
    <back-to-main/>
    <access-keys-admin-panel/>
  </div>
</template>

<script>
import BackToMain from "@/components/portal-admin/back-to-main";
import AccessKeysAdminPanel from "@/components/portal-admin/access-keys/access-keys-admin-panel";

export default {
  components: {AccessKeysAdminPanel, BackToMain},
  middleware: ["portal-admin"],
  name: "index"
};
</script>

<style scoped>

</style>
