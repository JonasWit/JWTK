<template>
  <v-overlay v-if="isOpen">
    <v-alert prominent type="warning" border="bottom" colored-border class="px-3 cookie">
      <v-row align="center">
        <v-col>
          <slot>
            Używamy ciasteczek m.in. w celach: świadczenia usług, statystyk. Korzystanie
            z witryny bez zmiany ustawień Twojej przeglądarki oznacza, że będą one umieszczane w Twoim urządzeniu
            końcowym. Pamiętaj, że zawsze możesz zmienić te ustawienia. Szczegóły znajdziesz w
            <nuxt-link class="cookie__link" to="/portal-web/privacy-policy">Polityce Prywatności.</nuxt-link>
          </slot>
        </v-col>
        <v-col class="shrink d-flex justify-end" cols="12" md="3">
          <v-btn color="success" class="ma-2" @click="accept">Akceptuję</v-btn>
        </v-col>
      </v-row>
    </v-alert>
  </v-overlay>
</template>
<script>
export default {
  name: "CookieMessage",
  data() {
    return {
      isOpen: false
    };
  },
  created() {
    if (!this.getGDPR() === true) {
      this.isOpen = true;
    }
  },
  methods: {
    getGDPR() {
      if (process.browser) {
        return localStorage.getItem("GDPR:accepted", true);
      }
    },
    accept() {
      if (process.browser) {
        this.isOpen = false;
        localStorage.setItem("GDPR:accepted", true);
      }
    },
  }
};
</script>

<style scoped>
.cookie {
  position: fixed;
  margin: auto;
  bottom: 0;
  left: 0


}

.cookie__link:link {
  text-decoration: none;
}

.cookie__link:hover {
  text-decoration: underline;
}


</style>
