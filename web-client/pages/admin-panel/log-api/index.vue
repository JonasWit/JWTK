<template>
  <div class="ma-3">
    <padmin-back-to-main/>
    <padmin-api-log-manager-panel/>
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
