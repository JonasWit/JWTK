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
import {mapActions, mapState} from "vuex";

export default {
  name: "breadcrumbs",
  fetch() {
    let clientId = this.$route.params.client
    if (clientId) {
      return this.getClientData({clientId})
    }
  },
  computed: {
    ...mapState('legal-app-client-store', ['clientDataFromFetch']),
    crumbs() {
      const fullPath = this.$route.fullPath
      const params = fullPath.substring(1).split('/')
      const crumbs = []
      let path = ''

      params.forEach((param, index, {length}) => {
        path = `${path}/${param}`
        const match = this.$router.match(path)
        if (match.name !== 'index') {
          if (index === length - 1) {
            crumbs.push({
              text: this.pathName(match.name),
              disabled: true,
            })
          } else {
            crumbs.push(
              this.pathName(match.name, path)
            )
          }
        }
      })
      return crumbs
    },
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientData']),
    pathName(name, path) {
      if (name === null) {
        return {
          text: this.clientDataFromFetch.name,
          disabled: true,
          href: path + '/',
        }
      }
      if (name === 'legal-application') {
        return {
          text: 'Strona główna',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-clients') {
        return {
          text: 'Lista Klientów',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-clients-client-c-details') {
        return {
          text: 'Panel Klienta',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-clients-client-contacts') {
        return {
          text: 'Kontakty',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-clients-client-cases') {
        return {
          text: 'Sprawy',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-clients-client-notes') {
        return {
          text: 'Notatki',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-clients-client-financials') {
        return {
          text: 'Rozliczenia',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-clients-client-archived-cases') {
        return {
          text: 'Archiwum spraw',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-reminders') {
        return {
          text: 'Kalendarz',
          disabled: false,
          href: path + '/',
        }
      }
      if (name === 'legal-application-archive') {
        return {
          text: 'Archiwum Klientów',
          disabled: false,
          href: path + '/',
        }
      }
    }
  }
}
</script>

<style scoped>

</style>
