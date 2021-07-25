<template>
  <v-container fluid>
    <section>
      <div class="content">
        <h1 class="text-center mt-5">Nasza oferta</h1>
        <h5 class="mx-7">
          Tworzymy zarówno statyczne strony internetowe jak również aplikacje webowe. Zamów u nas stronę lub aplikację a
          my zajmiemy się jej stworzeniem i utrzymywaniem jej przy życiu.
          Nie będziesz musiał się niczym przejmować!
        </h5>
      </div>
    </section>
    <v-row class="d-flex justify-space-between my-4">
      <v-card elevation="0" id="card" v-for="item in items" :key="item.id" width="350px" class="mx-auto my-2">
        <v-row align="center" justify="space-around">
          <img class="icon" :src="item.icon">
        </v-row>
        <v-card-title class="text-uppercase justify-center">{{ item.title }}</v-card-title>
        <v-card-text>{{ item.text }}</v-card-text>
      </v-card>
    </v-row>
    <section class="section-2">
      <div class="content">
        <h1 class="text-center mt-5">Cennik</h1>
        <h5 class="mx-7">
          Zapraszamy do kontaktu, aby poznać szczegółową wycenę usługi.
        </h5>
        <v-container class="my-6">
          <v-row align-content="center" justify="center">
            <v-col align-self="center" v-for=" plan in plans" :key="plan.id">
              <v-card elevation="" height="550px" width="350px">
                <v-card-text class="text-center font-weight-medium headline">{{ plan.title }}</v-card-text>
                <v-list disabled>
                  <v-list-item-group>
                    <v-list-item v-for="(bullet, i) in plan.bullet" :key="i">
                      <v-list-item-icon>
                        <v-icon small color="green">mdi-check</v-icon>
                      </v-list-item-icon>
                      <v-list-item-content>
                        {{ bullet[0] }}
                      </v-list-item-content>
                    </v-list-item>
                  </v-list-item-group>
                </v-list>
                <v-card-text class="text-center title mb-2">
                  {{ plan.price }}
                </v-card-text>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </div>
    </section>
  </v-container>
</template>

<script>

export default {
  name: "home-products",
  transition: 'slide-bottom',
  data: () => {
    return ({
      items: [
        {
          id: 1,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-website-100.png')}`,
          title: 'Strony i aplikcaje',
          text: 'Tworzymy zarówno statyczne strony internetowe jak również aplikacje webowe. ' +
            'Zamów u nas stronę lub aplikację a my zajmiemy się jej stowrzeniem i utrzymywaniem przy życiu. Nie będziesz musiał się niczym przejmować!'
        },

        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-scales-100.png')}`,
          title: 'Gotowe produkty',
          text: 'Jeśli prowadzisz kancelarię prawną, zapraszamy do zapoznania się z ofertą aplikacji Twoja Kancelaria.'
        },


        {
          id: 3,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-admin-settings-male-100.png')}`,
          title: 'Indywidualne rozwiązania',
          text: 'Szukasz innych rozwiązań? Sprawdź co jeszcze możemy dla Ciebie zrobić. Chętnie poznamy Twój biznes i stworzymy propozycję dostosowaną do Twoich potrzeb.'
        },

      ],
      plans: [
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
.icon {
  padding: 20px;
}

h1 {
  font-size: 3.5vw;
  font-weight: 800 !important;
  line-height: 60px;
  padding: 10px 0px;
  letter-spacing: -.03em !important;
}

h5 {
  font-size: 1.5vw;
  font-weight: 400 !important;
  padding: 10px 0px;


}

section {
  position: relative;
  width: 100%;
  height: 40vh;
  display: flex;
  justify-content: center;
  /*align-items: center;*/
  overflow: hidden;
  /*background-color: azure;*/
  /*background: linear-gradient(45deg, midnightblue, #000000)*/
}

section .content {
  position: relative;
  z-index: 1;
  margin-top: 25px;

}

section:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  /*background: black;*/
  background: linear-gradient(45deg, cornflowerblue, #000000);
  border-radius: 0 0 50% 50%/0 0 100% 100%;
  transform: scaleX(1.5);
}

.section-2 {
  width: 100%;
  height: 100%;


}

.section-2:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  /*background: black;*/
  background: linear-gradient(45deg, cornflowerblue, #000000);
  border-radius: 0 !important;
  transform: scaleX(1.5);
}

#card {
  background: transparent !important;
}

</style>
