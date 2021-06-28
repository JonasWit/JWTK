<template>
  <video v-if="darkThemeStored" :src="videoDark" style="min-height: 100vh; width: 100%; object-fit: cover"
         autoplay="true" loop="true" muted></video>
  <video v-else :src="videoLight" style="min-height: 100vh; width: 100%; object-fit: cover" autoplay="true" loop="true"
         muted></video>
</template>

<script>
import {mapState} from "vuex";

export default {
  name: "home-video",

  data: () => ({
    videoDark: `${require('~/assets/images/home-page-video.mp4')}`,
    videoLight: `${require('~/assets/images/home-video-light.mp4')}`,
  }),
  computed: {
    ...mapState('auth', ['darkThemeStored']),
  },
};
</script>

<style scoped>

</style>
