<template>
  <div class="ma-3">
    <padmin-back-to-main/>
    <padmin-portal-log-manager-panel/>
  </div>
</template>

<script>
export default {
  middleware: ["portal-admin"],
  name: "index"
};
</script>

<style scoped>

</style>
