<template>
  <v-container>

    <v-row>
      <v-card v-for="item in items" :key="item.id" width="340px" class="mx-auto my-8" elevation="15">
        <v-img :src="item.img" class="white--text align-center" gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
               height="120px">
          <v-card-text class="text-center font-weight-medium headline">{{ item.title }}</v-card-text>
          <v-card width="250px" height="80px" class="price text-center white--text title mb-2">{{
              item.price
            }}
          </v-card>
        </v-img>
        <v-list disabled>
          <v-list-item-group>
            <v-list-item v-for="(bullet, i) in item.bullet" :key="i">
              <v-list-item-icon>
                <v-icon small color="green">mdi-check</v-icon>
              </v-list-item-icon>
              <v-list-item-content>
                {{ bullet[0] }}
              </v-list-item-content>
            </v-list-item>
          </v-list-item-group>
        </v-list>
      </v-card>
    </v-row>

  </v-container>
</template>

<script>
export default {
  name: "home-pricing",

  data: () => {
    return ({
      items: [
        {
          id: 1,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Aplikacje Webowe oraz Strony Internetowe',
          price: 'od 1200 PLN netto',
          bullet: [
            ['Bezpłatna wycena i konsultacja co do technologii odpowiedniej dla projektu'],
            ['Możliwość wyboru technologii, w której stworzymy projekt'],
            ['Technologie front-end dla rozwiązań indywidualnych: Wordpress, Vue.js, Blazor WASM'],
            ['Technologie back-end dla rozwiązań indywidualnych: .NET5+'],
            ['Hosting']
          ],
        },
        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Twoja Kancelaria',
          price: 'od 100 PLN netto/mc za użytkownika',
          bullet: [
            ['Skontaktuj się z nami, aby skorzystać z 14-dniowowego dostępu testowego'],
            ['Zapewniamy wsparcie techniczne w ciągu maksimum 4 godzin od zgłoszenia'],
          ],
        },
        {
          id: 3,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Aplikacje Desktopowe',
          price: 'Wycena indywidualna',
          bullet: [
            ['Bezpłatna wycena i konsultacja co do technologii odpowiedniej dla projektu'],
            ['Aplikacje desktopowe w technologii: .NET5+, WPF lub WinForms, możliwość integracji z bazą danych'],
          ],
        },
      ],
    });
  },
};
</script>

<style scoped>
.price {
  position: absolute;
  left: 50px;
  background-color: crimson;
}


</style>
