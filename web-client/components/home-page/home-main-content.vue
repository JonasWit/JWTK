<template>
<v-container>
  <v-row class="d-flex justify-space-between" >

    <v-card v-for="item in items" :key="item.id">
      <v-img>{{ items.img }}</v-img>
      <v-card-title>{{ items.title }} text here</v-card-title>
      <v-card-text>{{ items.text }}</v-card-text>
    </v-card>

  </v-row>
</v-container>
</template>

<script>
export default {
name: "home-main-content",
  data: () => ({
    items: [
      { id: 1, img:`${require('~/assets/images/home-about-1.png')}`, title: 'Kim jesteśmy?', text: 'WebCouple to projekt'},
      { id: 2, img:`${require('~/assets/images/home-about-2.png')}`, title: 'Co robimy', text: 'Tworzymy strony internetowe i rozwiązania'},
      { id: 3, img:`${require('~/assets/images/home-about-3.png')}`, title: 'Nasz cel', text: 'Skupiamy się na dostarczaniu usług dostosowanych'},
    ]
  })
}
</script>

<style scoped>

</style>
