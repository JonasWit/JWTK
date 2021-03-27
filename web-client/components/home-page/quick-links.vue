<template>

  <v-row>
    <v-card class="d-flex justify-space-around my-6" width="100vw" color="">
      <div v-for="item in items" :key="item.id">
        <img class="icon" :src="item.img"/>
      </div>
    </v-card>
  </v-row>

</template>

<script>
export default {
  name: "quick-links",
  data: () => {
    return ({
      items: [
        {
          id: 1,
          img: `${require('~/assets/images/Blazor2.png')}`,

        },
        {
          id: 2,
          img: `${require('~/assets/images/Net2.png')}`,

        },
        {
          id: 3,
          img: `${require('~/assets/images/icons8-bootstrap-48.png')}`,

        },
        {
          id: 4,
          img: `${require('~/assets/images/icons8-vue-js-48.png')}`,

        },
        {
          id: 5,
          img: `${require('~/assets/images/icons8-wordpress-48.png')}`,

        },
      ]
    });
  },
};
</script>

<style scoped>
.icon {
  height: 48px;
  margin: 22px 0 0 22px;
}
</style>
