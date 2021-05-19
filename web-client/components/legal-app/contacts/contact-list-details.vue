<template>
  <v-tabs vertical>
    <v-tab>
      <v-icon left>
        mdi-email
      </v-icon>
      Email
    </v-tab>
    <v-tab-item>
      <v-card flat>
        <v-card-text v-for="item in emailsList" :key="item.id">
          <p>{{ item.id }}
          </p>
        </v-card-text>
      </v-card>
    </v-tab-item>
  </v-tabs>
</template>

<script>
export default {
  name: "contact-list-details",
  middleware: ['legal-app-permission', 'client', 'authenticated'],
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    emailsFromFetch: [],
    emailsList: []
  }),

  async fetch() {
    this.emailsFromFetch = await this.$axios.$get(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contact/${this.selectedContact.id}/emails`)
    console.warn('emails fetched from server', this.emailsFromFetch)
    this.emailsList = this.emailsFromFetch

  },
}
</script>

<style scoped>

</style>
