<template>
  <footer class="text-xs text-gray-600 pt-4 mt-3">
    <div class="flex flex-col space-x-4 text-center py-4 bg-gray-200 m-auto">
      <div>©2021 Copyright Systemy Wspomagania Pracy</div>
      <div>
        <router-link :to="{ 'name': 'Sources' }" class="text-gray-600">Źródła</router-link>
      </div>
    </div>

  </footer>
</template>

<script>
export default {
  name: "PortalFooter"
}
</script>

<style scoped>

</style>