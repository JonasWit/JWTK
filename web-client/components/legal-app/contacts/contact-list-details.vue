<template>
  <v-tabs vertical>
    <v-tab>
      <v-icon left>
        mdi-email
      </v-icon>
      Option 1
    </v-tab>
    <v-tab>
      <v-icon left>
        mdi-account
      </v-icon>
      Option 1
    </v-tab>
    <v-tab>
      <v-icon left>
        mdi-phone
      </v-icon>
      Option 1
    </v-tab>
    <v-tab-item>
      <v-card flat>
        <v-card-text>
          <p>Emails
          </p>
        </v-card-text>
      </v-card>
    </v-tab-item>
    <v-tab-item>
      <v-card flat>
        <v-card-text>
          <p>PLACEHOLDER 2
          </p>
        </v-card-text>
      </v-card>
    </v-tab-item>
    <v-tab-item>
      <v-card flat>
        <v-card-text>
          <p>PLACEHOLDER 3
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
  data: () => ({}),

  async fetch() {


  },
}
</script>

<style scoped>

</style>
