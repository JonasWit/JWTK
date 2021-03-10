<template>
  <div>
    <padmin-log-feed :load-logs="loadLogs"/>
  </div>
</template>

<script>
export default {
  name: "log-manager-panel",
  data: () => ({}),
  methods: {
    loadLogs(query) {
      return this.$axios.$get(`/api/portal-admin/log-admin/logs/split${query}`);
    }
  }
};
</script>

<style scoped>

</style>
