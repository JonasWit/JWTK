<template>
  <v-container>
    <v-parallax
      dark
      :src="img"
    >
      <v-row
        align="center"
        justify="center"
      >
        <v-col
          class="text-center"
          cols="12"
        >
          <h1 class="display-1 font-weight-thin mb-4">
            Twoja Kancelaria
          </h1>
          <h4 class="subheading">
            Aplikacja została stworzona z myślą o kancelariach
            prawnych. W łatwy i intuicyjny sposób pozwala usprawnić pracę kancelarii, łącząc funkcję kalendarza, bazy
            kontaktów oraz listy zadań na dany
            dzień.
          </h4>
        </v-col>
      </v-row>
    </v-parallax>

    <v-row class="my-5">
      <v-card id="card" v-for="item in items" :key="item.id" width="350px" class="mx-auto my-8">
        <v-icon large color="white">{{ item.icon }}</v-icon>
        <v-card-text>{{ item.text }}</v-card-text>
      </v-card>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "home-products",
  data: () => {
    return ({
      img: `${require('~/assets/images/background.jpg')}`,

      items: [
        {
          id: 1,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: 'mdi-shield',
          text: 'Aplikacja stanowi bazę Twoich klinetów. Intuicyjny panel pozwla na łatwe dodawanie lub modyfikowanie Klientów. \n Zrezygnuj z utrzymywania danych kontaktowych w notesie! \n Twoja Kancelaria w łatwy sposób pomoże uporządkować listę Twoich klinetów oraz powiązanych osób kontaktowych.'
        },
        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: 'mdi-shield',
          text: 'Skorzystaj z Kalendarza, aby sprawdzić terminy i przypomnienia dodane do konkretnych spraw. \n Masz zbyt dużo spraw i trudno Ci zapanować nad priorytetami? Aplikacja zrobi to za Ciebie! Każde przypomnienie dodane do sprawy ma określony prioytet, który jest wyraźnie widoczny w ogólnym widoku kalendarza.'
        },
        {
          id: 3,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: 'mdi-shield',
          text: 'Jesteś ciekaw czy praca dla konkretnych klientów jest rzeczywiście opłacałna? Z Twoją Kancelarią określenie zysków i kosztów, wynikających z współpracy z klientami jest bardzo proste. \n Stworzyliśmy osobny panel dedykowany statystykom, dzięki któremu możesz sprawdzić czy praca dla konkretnych klientów przynosi Ci oczekiwane zyski.'
        },
        {
          id: 4,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: 'mdi-shield',
          text: 'Rodzaj dostępu, funkcje i informacje do jakich mają dostęp użytkownicy jest określany przez tak zwanego\n' +
            '            "Użytkownika Głównego". Dzięki temu możesz mieć pewność, że Twoi pracownicy widzą tylko te dane, które są im\n' +
            '            potrzebne do prowadzenia określonych spraw lub wsparcia administracyjnego pracy kancelarii.'
        },
      ]
    });
  },
};
</script>

<style>
header {
  background-color: rgba(0, 0, 0, 0.4);
}

header h1, h4 {
  padding: 10px;
}
</style>
