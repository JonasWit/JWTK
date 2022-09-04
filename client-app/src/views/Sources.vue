<template>
<div>
  <div class="flex flex-row justify-between header-top">
    <ToSRouterLink />
    <PrivacyRouterLink />
  </div>
  <div>
    <h1>Na stronie wykorzystaliśmy poniższe zasoby:</h1>

    <div class="text-xs">
      <h3 class="py-3">Ikonki użyte na stronie pochodzą z portalu <a href="https://icons8.com/" target="_blank"><span
            style="font-weight: bolder">icons8.com:</span></a></h3>

      <a href="https://icons8.com/icon/113570/calculator" target="_blank">Calculator</a> icon by <a
        href="https://icons8.com" target="_blank">Icons8</a>
      <a href="https://icons8.com/icon/114898/gears" target="_blank">Gears</a> icon by <a href="https://icons8.com"
        target="_blank">Icons8</a>
      <a href="https://icons8.com/icon/115346/restaurant" target="_blank">Restaurant</a> icon by <a
        href="https://icons8.com" target="_blank">Icons8</a>
      <a href="https://icons8.com/icon/123620/ingredients" target="_blank">Ingredients</a> icon by <a
        href="https://icons8.com" target="_blank">Icons8</a>

    </div>
  </div>
</div>
  
</template>

<script>
import ToSRouterLink from '../components/portal/ToSRouterLink.vue';
import PrivacyRouterLink from '../components/portal/PrivacyRouterLink.vue';
export default {
    name: "Sources",
    components: { ToSRouterLink, PrivacyRouterLink }
}
</script>

<style scoped>

</style>