<template>

  <v-row class="d-flex justify-space-around my-1" width="100vw">
    <div v-for="item in items" :key="item.id">
      <a :href="item.url" target="_blank"><img class="icon" :src="item.img"/></a>
    </div>
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
          url: 'https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor',
          img: `${require('~/assets/images/Blazor2.png')}`,

        },
        {
          id: 2,
          url: 'https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet',
          img: `${require('~/assets/images/Net2.png')}`,

        },
        {
          id: 3,
          url: 'https://getbootstrap.com/',
          img: `${require('~/assets/images/icons8-bootstrap-48.png')}`,

        },
        {
          id: 4,
          url: 'https://vuejs.org/',
          img: `${require('~/assets/images/icons8-vue-js-48.png')}`,

        },
        {
          id: 5,
          url: 'https://vuetifyjs.com/en/',
          img: `${require('~/assets/images/vutify.png')}`,

        },


        {
          id: 6,
          url: 'https://wordpress.com/',
          img: `${require('~/assets/images/icons8-wordpress-48.png')}`,

        },
      ]
    });
  },
};
</script>

<style scoped>
.icon {
  height: 36px;
  margin: 22px 0 0 22px;
}
</style>
