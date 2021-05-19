<template>
  <v-tabs vertical>
    <v-tab>
      <v-icon left>
        mdi-email
      </v-icon>
      Email
      <add-email-dialog :selected-contact="selectedContact"/>
    </v-tab>
    <v-tab>
      <v-icon left>
        mdi-phone
      </v-icon>
      Telefony
    </v-tab>
    <v-tab>
      <v-icon left>
        mdi-mail
      </v-icon>
      Adresy
    </v-tab>
    <v-tab>
      <v-icon left>
        mdi-cog-outline
      </v-icon>
      Opcje
    </v-tab>
    <v-tab-item>
      <v-card flat>
        <v-card-text v-for="item in emailsList" :key="item.id">
          <v-row>

            <v-col>{{ item.email }}</v-col>
            <v-col>{{ item.comment }}</v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-tab-item>
    <v-tab-item>
      <v-card flat>
        <v-card-text v-for="item in phoneNumbersList" :key="item.id">
          <v-row>
            <v-col>{{ item.number }}</v-col>
            <v-col>{{ item.comment }}</v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-tab-item>
    <v-tab-item>
      <v-card flat>
        <v-card-text v-for="item in addressesList" :key="item.id">
          <v-row>
            <v-col>{{ item.street }}</v-col>
            <v-col>{{ item.comment }}</v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-tab-item>
    <v-tab-item>
      <v-card flat>
        <v-card-text>
          <v-row>
            <delete-contact-dialog :selected-contact="selectedContact"/>

          </v-row>
        </v-card-text>
      </v-card>
    </v-tab-item>
  </v-tabs>
</template>

<script>


import DeleteContactDialog from "~/components/legal-app/contacts/dialogs/delete-contact-dialog";
import AddEmailDialog from "~/components/legal-app/contacts/dialogs/add-email-dialog";
import {mapActions, mapGetters, mapMutations, mapState} from "vuex";

export default {
  name: "contact-list-details",
  components: {AddEmailDialog, DeleteContactDialog},
  middleware: ['legal-app-permission', 'client', 'authenticated'],
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    }
  },

  async fetch() {
    let clientId = this.$route.params.client;
    let contactId = this.selectedContact.id;
    this.getContactDetailsFromFetch({clientId, contactId})
    console.warn('contact-list-details -- fetch from store completed', this.contactDetailsFromFetch)
  },
  computed: {
    ...mapState('legal-app-client-store', ['emailsList', 'phoneNumbersList', 'addressesList']),
    ...mapGetters('legal-app-client-store', ['emailsList', 'phoneNumbersList', 'addressesList'])
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['updateContactDetailsList']),
    ...mapActions('legal-app-client-store', ['getContactDetailsFromFetch'])

  }
}
</script>

<style scoped>

</style>
