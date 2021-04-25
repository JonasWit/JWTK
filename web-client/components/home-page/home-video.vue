<template>
  <video v-if="darkTheme" :src="videoDark" style="min-height: 100vh; width: 100%; object-fit: cover" autoplay="true"
         loop="true" muted></video>
  <video v-else :src="videoLight" style="min-height: 100vh; width: 100%; object-fit: cover" autoplay="true"
         loop="true" muted></video>
</template>

<script>
export default {
  name: "home-video",

  data: () => ({
    videoDark: `${require('~/assets/images/home-page-video.mp4')}`,
    videoLight: `${require('~/assets/images/home-video-light.mp4')}`,
    darkTheme: true
  }),
  created() {
    this.darkTheme = this.$vuetify.theme.dark;
    this.$bus.$on('theme-switched', e => {
      this.darkTheme = e
    })
  },
  beforeDestroy() {
    this.vm.$off('theme-switched')
  },
  beforeMount() {
    console.warn('theme check vide component first load', this.$vuetify.theme);
    this.darkTheme = this.$vuetify.theme.dark;

  },
}
</script>

<style scoped>

</style>
