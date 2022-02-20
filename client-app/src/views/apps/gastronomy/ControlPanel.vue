<template>
  <div>
    <div class="mt-8 grid lg:grid-cols-3 gap-7">

      <router-link :to="{ 'name': 'GastronomyMenus' }">

        
        
        
      </router-link>
  
  
  
</template>

<script>
import {onMounted} from "vue";
import {hideNavBar} from "@/appControl/controlFunctions";

export default {
  name: "ControlPanel",
  setup() {
    onMounted(() => {
      hideNavBar()
    })
  }
}
</script>

<style scoped>

</style>