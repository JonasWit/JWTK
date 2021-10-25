<template>
  <v-container>
    <!--    <v-row class="mt-6" no-gutters v-for="item in videos" :key="item.id">-->
    <!--      <v-col cols="7" sm="12" md="8">-->
    <!--        <video :src="videos" width="320" height="240" controls></video>-->
    <!--      </v-col>-->
    <!--      <v-col cols="5" sm="12" md="4">-->
    <!--        Opis tutaj-->
    <!--      </v-col>-->
    <!--    </v-row>-->
  </v-container>
</template>
<script>
import Layout from "@/components/legal-app/layout";

export default {
  name: "index",
  components: {Layout},
  data: () => ({
    // videos: [
    //   {
    //     id: 1
    //   }
    //   `${require('~/assets/videos/client-section.mp4')}`, `${require('~/assets/videos/client-contact.mp4')}`
    // ]
  })
};
</script>

<style scoped>

</style>
