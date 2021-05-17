<template>
  <div>
    <portal-log-feed/>
  </div>
</template>

<script>
import PortalLogFeed from "@/components/portal-admin/log-manager/portal-log-feed";

export default {
  name: "portal-log-manager-panel",
  components: {PortalLogFeed},
};
</script>

<style scoped>

</style>
