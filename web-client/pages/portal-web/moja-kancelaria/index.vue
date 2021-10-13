<template>
  <layout>
    <template v-slot:content>
      <v-row class="mt-9 mb-9 d-flex ">
        <v-col cols="4">
          <v-icon x-large color="warning">
            mdi-alert-outline
          </v-icon>
          <h1 class="mt-4">Strona w budowie</h1>
          <div>
            Strona prezentująca aplikację Moja Kancelaria jest aktualnie w budowie.
          </div>
          <div class="mt-8">Obraz <a
            href="https://pixabay.com/pl/users/geralt-9301/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2422173">
            Gerd Altmann</a> z <a
            href="https://pixabay.com/pl/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2422173">
            Pixabay</a></div>
        </v-col>
        <v-col cols="8">
          <v-img :src="image">
          </v-img>
        </v-col>
      </v-row>
    </template>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";

export default {
  name: "index",
  components: {Layout},
  data: () => ({
    image: `${require('~/assets/images/legal-app/under-construction.png')}`
  })
}
</script>

<style scoped>

</style>
