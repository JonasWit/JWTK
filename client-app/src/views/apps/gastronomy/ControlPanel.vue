<template>
  <div>control panel</div>
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