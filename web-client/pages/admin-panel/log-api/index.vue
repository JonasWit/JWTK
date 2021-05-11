<template>
  <div class="ma-3">
    <back-to-main/>
    <api-log-manager-panel/>
  </div>
</template>

<script>
import BackToMain from "@/components/portal-admin/back-to-main";
import ApiLogManagerPanel from "@/components/portal-admin/log-manager/api-log-manager-panel";

export default {
  components: {ApiLogManagerPanel, BackToMain},
  middleware: ["portal-admin"],
  name: "index"
};
</script>

<style scoped>

</style>
