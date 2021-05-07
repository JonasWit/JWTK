<template>
  <div>
    <padmin-api-log-feed :load-logs="loadLogs"/>
  </div>
</template>

<script>
export default {
  name: "api-log-manager-panel",
  methods: {
    loadLogs(query) {
      return this.$axios.$get(`/api/portal-admin/log-admin/logs/server/split${query}`);
    }
  }
};
</script>

<style scoped>

</style>
