<template>
  <v-container fluid>
    <section>
      <div class="content text-center white--text d-flex align-center">
        <div>
          <h1>Nasza oferta</h1>
          <h5>
            Tworzymy zarówno statyczne strony internetowe jak również aplikacje webowe. Zamów u nas stronę lub aplikację
            a my zajmiemy się jej stworzeniem i utrzymywaniem jej przy życiu. Nie będziesz musiał się niczym
            przejmować! </h5>
        </div>
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
      <div class="content text-center white--text">
        <h1 class="mt-5">Cennik</h1>
        <h5 class="mx-7">
          Zapraszamy do kontaktu, aby poznać szczegółową wycenę usługi. </h5>
        <v-container class="my-6">
          <v-row align-content="center" justify="center">
            <v-col align-self="center" v-for=" plan in plans" :key="plan.id">
              <v-card elevation="" height="550px" width="350px" class="d-flex flex-column align-center mx-auto">
                <h2 class="text-center header">{{ plan.title }}</h2>
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
                <v-card-actions class="card-actions py-9">
                  <h2>
                    {{ plan.price }} </h2>
                </v-card-actions>
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
          title: 'Twoja Kancelaria',
          price: '100 PLN netto / miesiąc za Użytkownika',
          bullet: [
            ['Nowoczesne narzędzie stworzone do zarządzania pracą Kancelarii'],
            ['Skontaktuj się z nami, aby skorzystać z 14-dniowowego dostępu testowego'],
            ['Zapewniamy wsparcie techniczne w ciągu maksimum 4 godzin od zgłoszenia'],
          ],
        },
        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Strony Internetowe',
          price: 'od 600 PLN netto',
          bullet: [
            ['Statyczne strony internetowe - wizytówki'],
            ['Możliwość dodania dodatkowych funkcjonalności'],
            ['Możliwość zastosowania CMS'],
          ],
        },
        {
          id: 3,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Aplikacje Desktopowe',
          price: 'od 2000 PLN netto',
          bullet: [
            ['Bezpłatna wycena i konsultacja co do technologii odpowiedniej dla projektu'],
            ['Aplikacje desktopowe w technologii: .NET5+, WPF lub WinForms, możliwość integracji z bazą danych'],
          ],
        },
        {
          id: 4,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Aplikacje Webowe',
          price: 'od 2500 PLN netto',
          bullet: [
            ['Bezpłatna wycena i konsultacja co do technologii odpowiedniej dla projektu'],
            ['Aplikacje do obługi firmy, sklepy internetowe'],
            ['Możliwość wyboru technologii, w której stworzymy projekt. Zapewniamy darmową pomoc w dokonaniu właściwego wyboru.'],
            ['Technologie dla rozwiązań indywidualnych: Wordpress, Vue.js (Nuxt), Blazor WASM, .NET5+, PostgreSQL'],
          ],
        },
        {
          id: 5,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Utrzymanie Hostingu',
          price: 'od 300 PLN netto / miesiąc za aplikację',
          bullet: [
            ['Obsługa i utrzymanie stworzonej przez nas aplikacji lub statycznej strony internetowej w chmurze'],
            ['Możliwość wyboru lokalizacji serwera, Używamy VPS dostarczanych przez firmę Linode'],
            ['Monitorowanie stanu aplikacji'],
            ['Regularne instalowanie łatek systemu'],
          ],
        },
        {
          id: 6,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Utrzymanie Aplikacji',
          price: 'od 200 PLN netto / miesiąc za aplikację. Zmiany lub rozszeżenia do aplikacji wyceniane są indywidualnie',
          bullet: [
            ['Pomoc techniczna w przypadku błędów'],
            ['Wprowadzanie nowych funkcjonalności'],
            ['Zmiany istniejących funkcjonalności'],
            ['Monitorowanie stanu aplikacji'],
          ],
        },
      ],

    });
  },
};
</script>

<style scoped>

.card-actions {
  position: absolute;
  bottom: 0;

}

.icon {
  padding: 20px;
}

.header {
  letter-spacing: -.03em !important;
  font-weight: 800 !important;
  font-size: 25px;
  padding-top: 30px;
}

h1 {
  font-size: 65px;
  font-weight: 800 !important;
  line-height: 60px;
  padding: 10px 0px;
  letter-spacing: -.03em !important;
}

h5 {
  font-size: 25px;
  font-weight: 400 !important;
  padding: 10px 0px;

}

@media only screen and (max-width: 780px) {
  .content h1 {
    font-size: 30px;
    line-height: 30px;
  }

  .content h5 {
    font-size: 15px;
    padding: 2px 8px;
  }

  section .content {
    margin-top: 5px;

  }
}

section {
  position: relative;
  width: 100%;
  height: 45vh;
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
