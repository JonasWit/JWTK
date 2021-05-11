<template>
  <div class="ma-3">
    <back-to-main/>
    <user-admin-panel/>
  </div>
</template>

<script>
import BackToMain from "@/components/portal-admin/back-to-main";
import UserAdminPanel from "@/components/portal-admin/users-management/user-admin-panel";

export default {
  components: {UserAdminPanel, BackToMain},
  middleware: ["portal-admin"],
  name: "index"
};
</script>

<style scoped>

</style>
