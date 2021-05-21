<template>
  <v-tabs vertical>
    <v-tab class="d-flex justify-space-between">
      <v-icon left>
        mdi-email
      </v-icon>
      Email
      <add-email-dialog :selected-contact="selectedContact"/>
    </v-tab>
    <v-tab class="d-flex justify-space-between">
      <v-icon left>
        mdi-phone
      </v-icon>
      Telefony
      <add-phone-numbers-dialog :selected-contact="selectedContact"/>
    </v-tab>
    <v-tab class="d-flex justify-space-between">
      <v-icon left>
        mdi-mail
      </v-icon>
      Adresy
      <add-address-dialog :selected-contact="selectedContact"/>
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
            <v-col>Nazwa: {{ item.comment }}</v-col>
            <v-col>Adres email: {{ item.email }}</v-col>
            <v-col>
              <delete-email-dialog :selected-email="item" :selected-contact="selectedContact"/>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-tab-item>
    <v-tab-item>
      <v-card flat>
        <v-card-text v-for="item in phoneNumbersList" :key="item.id">
          <v-row>
            <v-col>Nazwa: {{ item.comment }}</v-col>
            <v-col>Numer telefonu: {{ item.number }}</v-col>
            <v-col>
              <delete-phone-number-dialog :selected-phone-number="item" :selected-contact="selectedContact"/>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-tab-item>
    <v-tab-item>
      <v-card flat>
        <v-card-text v-for="item in addressesList" :key="item.id">
          <v-row>
            <v-col>Nazwa: {{ item.comment }}</v-col>
            <v-col>Ulica: {{ item.street }}</v-col>
            <v-col>Nr: {{ item.building }}</v-col>
            <v-col>Miasto: {{ item.city }}</v-col>
            <v-col>Kod: {{ item.postCode }}</v-col>
            <v-col>Państwo: {{ item.country }}</v-col>
            <v-col>
              <delete-address-dialog :selected-address="item" :selected-contact="selectedContact"/>
            </v-col>
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
import DeleteEmailDialog from "@/components/legal-app/contacts/dialogs/delete-email-dialog";
import AddPhoneNumbersDialog from "@/components/legal-app/contacts/dialogs/add-phone-numbers-dialog";
import DeletePhoneNumberDialog from "@/components/legal-app/contacts/dialogs/delete-phone-number-dialog";
import AddAddressDialog from "@/components/legal-app/contacts/dialogs/add-address-dialog";
import DeleteAddressDialog from "@/components/legal-app/contacts/dialogs/delete-address-dialog";

export default {
  name: "contact-list-details",
  components: {
    DeleteAddressDialog,
    AddAddressDialog,
    DeletePhoneNumberDialog, AddPhoneNumbersDialog, DeleteEmailDialog, AddEmailDialog, DeleteContactDialog
  },
  middleware: ['legal-app-permission', 'client', 'authenticated'],
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    },


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
