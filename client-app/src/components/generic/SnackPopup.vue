<template>
  <p>{{ snack }}</p>
</template>

<script>
import {useStore} from "vuex";
import {computed} from "vue";

export default {
  name: "SnackPopup",
  setup(){
    const store = useStore()
    
    return{
      snack: computed(() => store.getters["snack/getSanck"]),
    }
  }
}
</script>

<style scoped>

</style>