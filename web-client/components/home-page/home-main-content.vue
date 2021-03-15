<template>
<v-container>
  <v-row class="d-flex justify-space-between" >

    <v-card v-for="item in items" :key="item.id" width="350px" class="mx-auto my-2">
      <v-img :src="item.img" height="125" contain></v-img>
      <v-card-title>{{ item.title }} text here</v-card-title>
      <v-card-text>{{ item.text }}</v-card-text>
    </v-card>

  </v-row>
</v-container>
</template>

<script>
export default {
name: "home-main-content",
  data: () => {
    return ({
      items: [
        {
          id: 1,
          img: `${require('~/assets/images/home-about-1.png')}`,
          title: 'Kim jesteśmy?',
          text: 'WebCouple to projekt'
        },
        {
          id: 2,
          img: `${require('~/assets/images/home-about-2.png')}`,
          title: 'Co robimy',
          text: 'Tworzymy strony internetowe i rozwiązania'
        },
        {
          id: 3,
          img: `${require('~/assets/images/home-about-3.png')}`,
          title: 'Nasz cel',
          text: 'Skupiamy się na dostarczaniu usług dostosowanych'
        },

      ],

    });
     },
  created() {
    console.log(this.items)
  },

     }
</script>

<style scoped>

</style>
