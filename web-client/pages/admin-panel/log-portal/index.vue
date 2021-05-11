<template>
  <div class="ma-3">
    <back-to-main/>
    <portal-log-manager-panel/>
  </div>
</template>

<script>
import BackToMain from "@/components/portal-admin/back-to-main";
import PortalLogManagerPanel from "@/components/portal-admin/log-manager/portal-log-manager-panel";

export default {
  components: {PortalLogManagerPanel, BackToMain},
  middleware: ["portal-admin"],
  name: "index"
};
</script>

<style scoped>

</style>
