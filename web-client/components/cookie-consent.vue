<template>
  <v-overlay v-if="isOpen" class="cookie">
    <v-card>
      <v-alert prominent type="warning" border="bottom" colored-border class="px-3">
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
            <v-btn color="success" class="ma-2" @click="accept">{{ buttonTextAccept }}</v-btn>
            <v-btn color="error" class="ma-2" text @click="deny">{{ buttonTextDeny }}</v-btn>
          </v-col>
        </v-row>
      </v-alert>
    </v-card>
  </v-overlay>
</template>
<script>
export default {
  name: "CookieMessage",
  props: {
    buttonTextAccept: {
      type: String,
      default: "Akceptuję"
    },
    buttonTextDeny: {
      type: String,
      default: "Odmawiam"
    },
    position: {
      type: String,
      default: "top"
    }
  },
  data() {
    return {
      isOpen: false
    };
  },
  computed: {
    containerPosition() {
      return `cookie--${this.position}`;
    }
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
    deny() {
      if (process.browser) {
        this.isOpen = false;
        localStorage.setItem("GDPR:accepted", false);
      }
    }
  }
};
</script>

<style lang="sass" scoped>
.cookie
  z-index: 1
  position: fixed
  bottom: 0

  &__link
    color: #ffffff
    text-decoration: underline
    transition: all .25s

    &:hover
      text-decoration: none
</style>
