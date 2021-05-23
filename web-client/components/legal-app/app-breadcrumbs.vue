<template>
  <div class="my-6">
    <v-row>
      <v-btn icon large @click="$router.back()">
        <v-icon>mdi-chevron-left</v-icon>
      </v-btn>
      <v-breadcrumbs v-for="(crumb, index) in crumbs" :key="index">
        <nuxt-link :to="crumb.path" class="crumb-link">
          {{ crumb.name }} /
        </nuxt-link>
      </v-breadcrumbs>
    </v-row>
  </div>
</template>

<script>

export default {
  name: "app-breadcrumbs",
  computed: {


    crumbs() {

      const fullPath = this.$route.fullPath
      const params = fullPath.substring(1).split('/')
      const crumbs = []

      let path = ''

      params.forEach((param) => {
        path = `${path}/${param}`
        const match = this.$router.match(path)

        if (match.name !== null) {
          crumbs.push(match)
          console.warn(crumbs, 'lista crumbsów')
        }
      })

      return crumbs
    }

  },
  methods: {}
}
</script>

<style scoped>
.crumb-link {
  text-decoration: none;
}
</style>
