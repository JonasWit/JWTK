<template>
  <div>
    <p>Number from state: {{ count }}</p>
    <button v-on:click="increment">Increment</button>
    <button v-on:click="decrement">Decrement</button>
  </div>

</template>

<script lang="ts">
import {computed, defineComponent} from 'vue';
import {useStore} from "@/store";


export default defineComponent({
  name: 'Counter',
  components: {},
  setup() {
    const store = useStore()
    
    return {
      count: computed(() => store.state.count),
      increment: () => store.commit('increment'),
      decrement: () => store.commit('decrement'),   
    }
  }
});
</script>

<style scoped>

</style>