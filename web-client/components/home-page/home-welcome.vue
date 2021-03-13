<template>
  <video :src="video" style="max-height: 700px; height: 100vh; min-width: 100%; object-fit: cover" autoplay="true" loop="true"></video>
</template>
<script>

export default {
  name: "home-welcome",

  data: () => ({
    video: `${require('~/assets/images/blubki-video.mp4')}`
  }),
}
</script>

<style scoped>

</style>
