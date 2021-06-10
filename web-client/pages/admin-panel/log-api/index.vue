<template>
  <div class="ma-3">
    <div>
      <h3 class="text-h3 mb-4 text-center">Api Logs</h3>
    </div>
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
