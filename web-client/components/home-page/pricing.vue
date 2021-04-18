<template>
  <v-container>
    <v-row>

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
          title: 'Strony internetowe',
          price: 'od 1200 PLN brutto',
          bullet: [
            ['strona tworzona na podstawie originalnego szablonu'],
            ['formularz kontaktowy'],
            ['System zarządzania treścią'],
            ['Rejestracja domeny'],
            ['Hosting']
          ],
        },
        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Twoja Kancelaria',
          price: 'Abonament',
          bullet: [
            ['skontaktuj się z nami, aby skorzystać z 7-dniowej wersji próbnej'],
            ['jeśli spodoba Ci się ten produkt, wówczas przedstawimy Ci plany abonamentowe dostowane do Twoich potrzeb'],
            ['w każdej chwili możesz skorzystać z naszego wsparcia technicznego'],
          ],
        },
        {
          id: 3,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Projekty indywidualne',
          price: 'Wycena indywidualna',
          bullet: [
            ['opowiedz nam o swojej działalności, a my podpowiemy jaka aplikacja może usprawnić działanie Twojego biznesu'],
            ['masz pomysł na sklep, aplikację webową lub desktopową - zgłoś się do nas!'],
            ['jeśli chcesz dowiedzieć się więcej na temat projektów indywidualnych- zapraszamy do kontaktu'],
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
