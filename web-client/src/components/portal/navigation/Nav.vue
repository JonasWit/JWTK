<template>
<nav>
  <div>
    <h1 class="font-bold uppercase p-4 border-b border-gray-100">
      <router-link class="text-blue-700" to="/">systemywp</router-link>    
    </h1>
  </div>
  <slot></slot>
</nav>
</template>

<script>
import useStore from "vuex";
import {computed} from "vue";

export default {
  name: "Nav",
}
</script>

<style scoped>

</style>