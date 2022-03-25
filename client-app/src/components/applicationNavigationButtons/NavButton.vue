<template>
  <router-link :to="{ 'name': props.navigateTo }">
    <div class="app-navigation-link">
      <img :src="props.iconPath" alt="gastronomy" class="w-full">
      <div class="flex justify-center align-middle text-sm">
        <p class="font-bold">{{ props.text }}</p>
      </div>
    </div>
  </router-link>
</template>

<script>

export default {
  name: "NavButton",
  props: {
    text: String,
    navigateTo: String,
    iconPath: String,
  },
  setup(props) {
    return {
      props
    }
  }
}
</script>

<style scoped>

</style>