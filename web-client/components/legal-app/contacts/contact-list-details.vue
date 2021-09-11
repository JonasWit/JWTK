<template>
  <v-dialog v-model="dialog" fullscreen hide-overlay>
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon large color="primary">mdi-arrow-right-bold-box</v-icon>
          </v-btn>
        </template>
        <span>Przejdź do szczegółów</span>
      </v-tooltip>
    </template>

    <v-card>
      <v-toolbar dark color="primary">
        <v-btn icon dark @click="dialog = false">
          <v-icon>mdi-close</v-icon>
        </v-btn>
        <v-toolbar-title>{{ selectedContact.title }}</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-title>{{ selectedContact.name }} {{ selectedContact.surname }}</v-toolbar-title>
      </v-toolbar>
      <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
        Zarządzaj kontaktami dla Klienta! Użyj zielonej ikonki "+", aby uzupełnić pierwszy kontakt.
        Następnie wybierz sekcję, którą chcesz uzupełnić.
      </v-alert>
      <v-tabs vertical>
        <v-tab class="d-flex justify-space-between" icons-and-text>
          <v-icon left>
            mdi-email
          </v-icon>
          Email
          <add-email-dialog :selected-contact="selectedContact" v-on:action-completed="actionDone"/>
        </v-tab>
        <v-tab class="d-flex justify-space-between">
          <v-icon left>
            mdi-phone
          </v-icon>
          Telefony
          <add-phone-numbers-dialog :selected-contact="selectedContact" v-on:action-completed="actionDone"/>
        </v-tab>
        <v-tab class="d-flex justify-space-between">
          <v-icon left>
            mdi-mail
          </v-icon>
          Adresy
          <add-address-dialog :selected-contact="selectedContact" v-on:action-completed="actionDone"/>
        </v-tab>
        <v-tab-item>
          <v-card flat>
            <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
              Nie posiadasz jeszcze żadnych adresów email? Użyj zielonej ikonki "+", aby dodać nowy email.
            </v-alert>
            <v-card-text v-for="item in emailsList" :key="item.id">
              <v-row class="d-flex flex-sm-column flex-md-row">
                <v-col>Nazwa: {{ item.comment }}</v-col>
                <v-col>Adres email: {{ item.email }}</v-col>
                <v-col>
                  <delete-email-dialog :selected-email="item" :selected-contact="selectedContact"
                                       v-on:action-completed="actionDone"/>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-tab-item>
        <v-tab-item>
          <v-card flat>
            <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
              Nie posiadasz jeszcze żadnych numerów telefonów? Użyj zielonej ikonki "+", aby dodać numery.
            </v-alert>
            <v-card-text v-for="item in phoneNumbersList" :key="item.id">
              <v-row class="d-flex flex-sm-column flex-md-row">
                <v-col>Nazwa: {{ item.comment }}</v-col>
                <v-col>Numer telefonu: {{ item.number }}</v-col>
                <v-col>
                  <delete-phone-number-dialog :selected-phone-number="item" :selected-contact="selectedContact"
                                              v-on:action-completed="actionDone"/>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-tab-item>
        <v-tab-item>
          <v-card flat>
            <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
              Nie posiadasz jeszcze żadnych adresów? Użyj zielonej ikonki "+", aby dodać adresy.
            </v-alert>
            <v-card-text v-for="item in addressesList" :key="item.id">
              <v-row class="d-flex flex-sm-column flex-md-row">
                <v-col>Nazwa: {{ item.comment }}</v-col>
                <v-col>Ulica i Nr: {{ item.street }} {{ item.building }}</v-col>
                <v-col>Miasto i kod: {{ item.city }} {{ item.postCode }}</v-col>
                <v-col>Państwo: {{ item.country }}</v-col>
                <v-col>
                  <delete-address-dialog :selected-address="item" :selected-contact="selectedContact"
                                         v-on:action-completed="actionDone"/>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-tab-item>
      </v-tabs>
    </v-card>
  </v-dialog>
</template>
<script>
import DeleteContactDialog from "~/components/legal-app/contacts/dialogs/delete-contact-dialog";
import AddEmailDialog from "~/components/legal-app/contacts/dialogs/add-email-dialog";
import {mapActions, mapMutations, mapState} from "vuex";
import DeleteEmailDialog from "@/components/legal-app/contacts/dialogs/delete-email-dialog";
import AddPhoneNumbersDialog from "@/components/legal-app/contacts/dialogs/add-phone-numbers-dialog";
import DeletePhoneNumberDialog from "@/components/legal-app/contacts/dialogs/delete-phone-number-dialog";
import AddAddressDialog from "@/components/legal-app/contacts/dialogs/add-address-dialog";
import DeleteAddressDialog from "@/components/legal-app/contacts/dialogs/delete-address-dialog";
import EditContactDialog from "@/components/legal-app/contacts/dialogs/edit-contact-dialog";
import {handleError} from "~/data/functions";
import ProgressBar from "@/components/legal-app/progress-bar";

export default {
  name: "contact-list-details",
  components: {
    ProgressBar,
    EditContactDialog,
    DeleteAddressDialog,
    AddAddressDialog,
    DeletePhoneNumberDialog, AddPhoneNumbersDialog, DeleteEmailDialog, AddEmailDialog, DeleteContactDialog
  },
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    },
  },
  data: () => ({
    emailsList: [],
    phoneNumbersList: [],
    addressesList: [],
    dialog: false,
  }),

  async fetch() {
    try {
      await this.updateContactLIst()
    } catch (error) {
      handleError(error);
    }
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['contactDetailsFromFetch']),
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['updateContactDetailsList']),
    ...mapActions('legal-app-client-store', ['getContactDetailsFromFetch', 'getContactsList']),
    async updateContactLIst() {
      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id;
        await this.getContactDetailsFromFetch({clientId, contactId});
        this.emailsList = this.contactDetailsFromFetch.emails
        this.phoneNumbersList = this.contactDetailsFromFetch.phoneNumbers
        this.addressesList = this.contactDetailsFromFetch.physicalAddresses
      } catch (error) {
        handleError(error)
      }
    },
    async actionDone() {
      try {
        await this.updateContactLIst();
      } catch (error) {
        handleError(error)
      }
    },
    deleteDone() {
      this.dialog = false;
    },
  }
};
</script>

<style scoped>

</style>
