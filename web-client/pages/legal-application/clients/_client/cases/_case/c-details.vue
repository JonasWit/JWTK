<template>
  <layout>
    <template v-slot:content>
      <v-container>
        <v-row>
          <div class="d-flex justify-space-between pa-3" v-for="item in items" :key="item.id">
            <if-auth v-if="item.adminAccess">
              <template v-slot:allowed="{portalAdmin, clientAdmin}">
                <v-col v-if="(portalAdmin || clientAdmin)">
                  <v-hover v-slot="{ hover }">
                    <nuxt-link class="nav-item" :to="item.route">
                      <v-card class="mx-auto index-card" width="340" outlined :elevation="hover ? 12 : 2">
                        <v-row class="d-flex ma-3" align="center">
                          <v-icon>{{ item.icon }}</v-icon>
                          <v-card-title class="text-uppercase justify-center">{{ item.name }}</v-card-title>
                        </v-row>
                        <v-card-text class="font-weight-light">{{ item.text }}</v-card-text>
                      </v-card>
                    </nuxt-link>
                  </v-hover>
                </v-col>
              </template>
            </if-auth>
            <v-col v-else>
              <v-hover v-slot="{ hover }">
                <nuxt-link class="nav-item" :to="item.route">
                  <v-card class="mx-auto index-card" width="340" outlined :elevation="hover ? 12 : 2">
                    <v-row class="d-flex ma-3" align="center">
                      <v-icon>{{ item.icon }}</v-icon>
                      <v-card-title class="text-uppercase justify-center">{{ item.name }}</v-card-title>
                    </v-row>
                    <v-card-text class="font-weight-light">{{ item.text }}</v-card-text>
                  </v-card>
                </nuxt-link>
              </v-hover>
            </v-col>
          </div>
        </v-row>
      </v-container>
      <progress-bar v-if="loader"/>
    </template>
  </layout>
</template>
<script>
import Layout from "@/components/legal-app/layout";
import {mapActions, mapState} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "c-details",
  components: {ProgressBar, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],

  data: () => ({
    client: null,
    case: null,
    loader: false
  }),
  async fetch() {
    this.loader = true
    try {
      this.client = await this.$axios.$get(`/api/legal-app-clients/client/${this.$route.params.client}`);
      this.case = await this.$axios.$get(`/api/legal-app-cases/case/${this.$route.params.case}`);
      let caseId = this.$route.params.case;
      await this.getCaseDetails({caseId});
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false
    }

  },
  async asyncData({params}) {
    return {
      items: [
        {
          id: '1',
          route: `/legal-application/clients/${params.client}/cases/${params.case}/details`,
          name: 'Szczegóły',
          text: 'Szczegóły sprawy',
          icon: 'mdi-card-account-mail-outline',
          adminAccess: false
        },
        {
          id: '2',
          route: `/legal-application/clients/${params.client}/cases/${params.case}/notes`,
          name: 'Notatki',
          text: 'Notatki dla sprawy',
          icon: 'mdi-card-account-mail-outline',
          adminAccess: false
        },
        {
          id: '3',
          route: `/legal-application/clients/${params.client}/cases/${params.case}/deadlines`,
          name: 'Terminy',
          text: 'Terminy dla sprawy',
          icon: 'mdi-card-account-mail-outline',
          adminAccess: false
        },
        {
          id: '4',
          route: `/legal-application/clients/${params.client}/cases/${params.case}/accesses`,
          name: 'Dostępy',
          text: 'Dostępy do sprawy',
          icon: 'mdi-card-account-mail-outline',
          adminAccess: true
        },
      ]
    };
  },
  computed: {
    ...mapState('legal-app-client-store', ['clientCaseDetails'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails'])
  }

};
</script>

<style scoped>

</style>
