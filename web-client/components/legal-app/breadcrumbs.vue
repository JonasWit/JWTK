<template>
  <div class="d-inline-flex items-center my-7">
    <v-tooltip bottom>
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="error" text small depressed v-bind="attrs" v-on="on" @click="$router.back()">
          <v-icon>mdi-arrow-left</v-icon>
        </v-btn>
      </template>
      <span>Powrót do poprzedniej strony</span>
    </v-tooltip>
    <v-breadcrumbs class="py-0" :items="crumbs"></v-breadcrumbs>
  </div>
</template>

<script>
export default {
  name: "breadcrumbs",
  computed: {
    crumbs() {
      const fullPath = this.$route.fullPath
      console.log('full path', fullPath)
      const params = fullPath.substring(1).split('/')
      console.log('params', params)
      const crumbs = []
      let path = ''

      params.forEach((param, index, {length}) => {
        path = `${path}/${param}`
        console.log('path', path)
        const match = this.$router.match(path)
        if (match.name !== 'index') {
          console.log('match path', match.name)
          if (index === length - 1) {
            crumbs.push({
              text: this.pathName(match.name),
              disabled: true,
            })
          } else {
            crumbs.push({
              text: this.pathName(match.name),
              disabled: false,
              href: path + '/',
            })
          }
        }
      })
      return crumbs
    },
  },

  methods: {
    pathName(name) {
      if (name === 'legal-application') {
        return 'Strona główna'
      }
      if (name === 'legal-application-clients') {
        return 'Lista Klientów'
      }
      if (name === 'legal-application-clients-client-c-details') {
        return 'Panel Klienta'
      }
      if (name === 'legal-application-clients-client-contacts') {
        return 'Kontakty'
      }
      if (name === 'legal-application-clients-client-cases') {
        return 'Sprawy'
      }
      if (name === 'legal-application-clients-client-notes') {
        return 'Notatki'
      }
      if (name === 'legal-application-clients-client-financials') {
        return 'Rozliczenia'
      }
      if (name === 'legal-application-clients-client-archived-cases') {
        return 'Archiwum spraw'
      }
      if (name === 'legal-application-reminders') {
        return 'Kalendarz'
      }
      if (name === 'legal-application-archive') {
        return 'Archiwum Klientów'
      }

    }

  }
}
</script>

<style scoped>

</style>
