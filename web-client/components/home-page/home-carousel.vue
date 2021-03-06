<template>
  <div>
    <v-carousel cycle hide-delimiter-background show-arrows-on-hover>
      <v-carousel-item v-for="(image, i) in images" :key="i" :src="image.src">
        <v-row class="fill-height d-flex align-end flex-column spacing-playground pa-6">
          <div class="white lighten-5 black--text p-4 justify-space-between spacing-playground pa-6" width="120px">
            <div>
              Slide Lorem ipsum dolor sit amet, consectetur adipisicing elit. Accusamus nihil nulla soluta sunt!
            </div>
          </div>
        </v-row>
      </v-carousel-item>
    </v-carousel>
  </div>
</template>

<script>

export default {
  name: "home-carousel",
  data: () => ({
    images: [
      {src: `${require('~/assets/images/carousel-item-1.jpg')}`},
      {src: `${require('~/assets/images/carousel-item-3.jpg')}`},
      {src: `${require('~/assets/images/carousel-item-4.jpg')}`},
    ],

  })

};
</script>

<style scoped>

</style>
