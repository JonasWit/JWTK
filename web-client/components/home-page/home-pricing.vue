<template>
  <v-container>
    <h1>Cennik</h1>
    <v-row class="d-flex justify-space-between my-4">
      <v-col md="3">
        <v-parallax dark height="1050" :src="img" class="module">
          <header>
            <h1 class="font-weight-bold">
              Cennik</h1>
          </header>
        </v-parallax>
      </v-col>
      <v-col md="8">
        <v-row class="d-flex justify-space-between mb-5">
          <p></p>
        </v-row>
        <v-divider></v-divider>
        <v-row class="my-5">
          <v-card id="card" v-for="item in items" :key="item.id" width="300px" class="mx-auto my-8">

            <v-card-title>{{ item.title }}</v-card-title>
            <v-card-subtitle>{{ item.price }}</v-card-subtitle>
            <v-list disabled>
              <v-list-item-group>
                <v-list-item v-for="(item, i) in items.include" :key="i">
                  <v-list-item-icon>
                    <v-icon>{{ item.include.icon }}</v-icon>
                  </v-list-item-icon>
                  <v-list-item-content>
                    <v-list-item-title>{{ item.include.bullet }}</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
              </v-list-item-group>
            </v-list>

          </v-card>
        </v-row>
        <v-divider></v-divider>
        <v-row>
          <home-contact/>
        </v-row>
      </v-col>
    </v-row>

  </v-container>
</template>

<script>
import HomeContact from "@/components/home-page/home-contact";

export default {
  name: "home-pricing",
  components: {HomeContact},
  data: () => {
    return ({
      img: `${require('~/assets/images/home-prizing.jpg')}`,

      items: [
        {
          id: 1,
          title: 'Strony internetowe',
          price: 'od 800 PLN',
          include: [
            {
              id: 1,
              icon: 'mdi-done',
              bullet: ['strona tworzona na podstawie originalnego szablonu',
                'system zarządzania treścią (CMS)',
                'optymalizacja treści, aby była bardziej przyjazna dla wyszukiwarek (SEO)',
                'Rejestracja domeny',
                'Hosting'],
            }],
        },
        {
          id: 1,
          title: 'Twoja Kancelaria',
          price: 'od 800 PLN',
          include: [
            {
              id: 1,
              icon: 'mdi-done',
              bullet: ['strona tworzona na podstawie originalnego szablonu',
                'system zarządzania treścią (CMS)',
                'optymalizacja treści, aby była bardziej przyjazna dla wyszukiwarek (SEO)',
                'Rejestracja domeny',
                'Hosting'],
            }],
        },
        {
          id: 1,
          title: 'Projekty indywidualne',
          price: 'od 800 PLN',
          include: [
            {
              id: 1,
              icon: 'mdi-done',
              bullet: ['strona tworzona na podstawie originalnego szablonu',
                'system zarządzania treścią (CMS)',
                'optymalizacja treści, aby była bardziej przyjazna dla wyszukiwarek (SEO)',
                'Rejestracja domeny',
                'Hosting'],
            }],
        },
      ],
    });
  },
};
</script>

<style scoped>

</style>
