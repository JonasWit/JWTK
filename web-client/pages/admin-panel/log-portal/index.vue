<template>
  <div class="ma-3">
    <div>
      <h3 class="text-h3 mb-4 text-center">Portal Logs</h3>
    </div>
    <portal-log-manager-panel/>
  </div>
</template>

<script>
import PortalLogManagerPanel from "@/components/portal-admin/log-manager/portal-log-manager-panel";

export default {
  components: {PortalLogManagerPanel},
  middleware: ["portal-admin"],
  name: "index"
};
</script>

<style scoped>

</style>
