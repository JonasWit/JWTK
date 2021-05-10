<template>
  <div class="ma-3">
    <padmin-back-to-main/>
    <padmin-user-admin-panel/>
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
