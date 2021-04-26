<template>
  <div>
    <padmin-portal-log-feed :load-logs="loadLogs"/>
  </div>
</template>

<script>
export default {
  name: "portal-log-manager-panel",
  methods: {
    loadLogs(query) {
      return this.$axios.$get(`/api/portal-admin/log-admin/logs/split${query}`);
    }
  }
};
</script>

<style scoped>

</style>
