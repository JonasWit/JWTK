<template>
  <div class="fixed w-full top-0 left-0 flex justify-center">
    <transition name="snack">
      <div v-if="snack.text" class="px-8 py-2 rounded-lg text-sm font-bold mt-6"
           :class="`${snack.textColor} ${snack.backColor}`">
        {{ snack.text }}
      </div>
    </transition>
  </div>
</template>

<script>
import {useStore} from "vuex";
import {computed} from "vue";

export default {
  name: "SnackPopup",
  setup() {
    const store = useStore()

    return {
      snack: computed(() => store.getters["snack/getSanck"]),
    }
  }
}
</script>

<style scoped>

</style>