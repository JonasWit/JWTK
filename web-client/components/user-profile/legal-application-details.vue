<template>
  <div>
    <v-card class="mb-2">
      <v-card-title class="d-flex justify-center">Zarządznie Kancelarią</v-card-title>
      <v-toolbar>
        <v-tabs v-model="legalAppTab" centered>
          <v-tab v-for="(t, i) in legalAppTabs" :key="`legal-app-admin-tab-${i}`">
            {{ t.title }}
          </v-tab>
        </v-tabs>
      </v-toolbar>
      <v-tabs-items v-model="legalAppTab">
        <v-tab-item v-for="(t, i) in legalAppTabs" :key="`legal-app-admin-tab-body-${i}`">
          <component class="ma-3" :is="t.component"/>
        </v-tab-item>
      </v-tabs-items>
    </v-card>
  </div>
</template>

<script>

import LegalAppData from "@/components/user-profile/legal-app-access-details/legal-app-data-manager-panel";
import LegalAppUsers from "@/components/user-profile/legal-app-access-details/legal-app-users";

export default {
  name: "legal-application-details",
  data: () => ({
    legalAppTab: 0,
    legalAppTabs: [
      {title: "Użytkownicy", component: LegalAppUsers},
      {title: "Dane", component: LegalAppData},
    ]
  }),
  methods: {}
};
</script>

<style scoped></style>
