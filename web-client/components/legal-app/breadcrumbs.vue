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
  data: () => ({
    crumbs: [],
  }),
  async fetch() {
    let clientId = this.$route.params.client;
    let caseId = this.$route.params.case;
    if (clientId) {
      await this.getClientData({clientId});
    }
    if (caseId) {
      await this.getCaseDetails({caseId});
    }
    this.crumbs = this.refreshCrumbs();
  },
  computed: {
    ...mapState('legal-app-client-store', ['clientDataFromFetch', 'clientCaseDetails']),
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientData', 'getCaseDetails']),
    refreshCrumbs() {
      const fullPath = this.$route.fullPath;
      const params = fullPath.substring(1).split('/');
      const crumbs = [];
      let path = '';

      params.forEach((param, index, {length}) => {
        path = `${path}/${param}`;
        const match = this.$router.match(path);
        if (match.name !== 'index') {
          if (index === length - 1) {
            crumbs.push({
              text: this.pathName(match.name),
              disabled: true,
            });
          } else {
            crumbs.push(
              this.pathName(match.name, path)
            );
          }
        }
      });
      return crumbs;
    },
    pathName(name, path) {
      let clientId = this.$route.params.client;
      let caseId = this.$route.params.case;

      if (path === `/legal-application/clients/${clientId}` && name === null) {
        return {
          text: this.clientDataFromFetch.name,
          disabled: true,
          href: path + '/',
        };
      }
      if (name === 'legal-application') {
        return {
          text: 'Strona główna',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients') {
        return {
          text: 'Lista Klientów',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-c-details') {
        return {
          text: 'Panel Klienta',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-contacts') {
        return {
          text: 'Kontakty',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-cases') {
        return {
          text: 'Sprawy',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-notes') {
        return {
          text: 'Notatki',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-financials') {
        return {
          text: 'Rozliczenia',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-archived-cases') {
        return {
          text: 'Archiwum spraw',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-reminders') {
        return {
          text: 'Kalendarz',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-archive') {
        return {
          text: 'Archiwum Klientów',
          disabled: false,
          href: path + '/',
        };
      }
      if (path === `/legal-application/clients/${clientId}/cases/${caseId}` && name == null) {
        return {
          text: this.clientCaseDetails.name,
          disabled: true,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-cases-case-deadlines') {
        return {
          text: 'Terminy procesowe',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-cases-case-details') {
        return {
          text: 'Szczegóły sprawy',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-cases-case-notes') {
        return {
          text: 'Notatki',
          disabled: false,
          href: path + '/',
        };
      }
      if (name === 'legal-application-clients-client-cases-case-accesses') {
        return {
          text: 'Dostępy',
          disabled: false,
          href: path + '/',
        };
      }

    },

  }
};
</script>

<style scoped>

</style>
