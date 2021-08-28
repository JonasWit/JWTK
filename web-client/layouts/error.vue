<template>
  <v-app dark>
    <v-container>
      <div v-if="error.statusCode === 404" class="d-flex flex-column justify-center align-center">
        <h1 class="mt-16 er-text">404</h1>
        <h1 class="er-text-2">{{ pageNotFound }}</h1>
      </div>
      <div v-else>
        <h1 class="er-text-3 mt-16 text-center">
          {{ otherError }}
        </h1>
      </div>
      <div class="d-flex flex-column justify-center align-center mt-8">
        <NuxtLink to="/">
          <v-btn color="primary"><h3>Powrót do strony głównej</h3></v-btn>
        </NuxtLink>
      </div>
    </v-container>

  </v-app>
</template>

<script>
export default {
  layout: 'empty',
  props: {
    error: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      pageNotFound: 'Niestety pod tym adresem nie znajdziesz żadnych treści.',
      otherError: 'Wystąpił błąd, proszę spróbować później. Jeśli błąd będzie się powtarzał, skontaktuj się z administratorem.',

    }
  },
  head() {
    const title =
      this.error.statusCode === 404 ? this.pageNotFound : this.otherError
    return {
      title
    }
  }
}
</script>

<style scoped>
.er-text {
  letter-spacing: 20px;
  font-size: 10rem;
  color: dodgerblue;

}

.er-text-2 {
  letter-spacing: 2px;
  color: dodgerblue;

}

.er-text-3 {
  color: dodgerblue;

}
</style>
