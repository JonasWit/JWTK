<template>
  <div>
    <v-row justify="center">
      <v-card v-if="clientCaseDetails">
        <v-toolbar dark color="primary">
          <v-btn icon dark @click="dialog = false">
            <v-icon>mdi-close</v-icon>
          </v-btn>
          <v-toolbar-title>
            Tytuł sprawy: {{ clientCaseDetails.name }}
          </v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-title>
            Kategoria: {{ clientCaseDetails.group }}
          </v-toolbar-title>
        </v-toolbar>
        <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">
          Jeśli chcesz zmodyfikować treść notatki, użyj guzika "EDYTUJ". W nowym okienku będziesz mógł dokonać zmian.
          Jeśli już nie potrzebujesz tej notatki, użyj guzika "USUŃ" i potwierdź operację.
        </v-alert>
        <v-subheader class="mx-2"><span
          class="font-weight-bold">Utworzono: </span> {{ formatDateWithHours(clientCaseDetails.created) }}
        </v-subheader>
        <v-subheader class="mx-2"><span class="font-weight-bold">Utworzone przez: </span>
          {{ clientCaseDetails.createdBy }}
        </v-subheader>
        <v-tabs v-model="tab" background-color="primary accent-4" centered dark icons-and-text>
          <v-tabs-slider></v-tabs-slider>
          <v-tab href="#tab-1">
            informacje podstawowe
            <v-icon>mdi-briefcase-account</v-icon>
          </v-tab>
          <v-tab href="#tab-2" v-if="clientAdmin">
            panel dostępów
            <v-icon>mdi-folder-lock</v-icon>
          </v-tab>
        </v-tabs>
        <v-tabs-items v-model="tab">
          <v-tab-item :value="'tab-1'">
            <v-card flat>
              <v-row class="mx-2 my-2 d-flex align-center">
                <v-card-title>Opis sprawy</v-card-title>
                <v-card-title> Sygnatura: {{ clientCaseDetails.signature }}</v-card-title>
                <edit-case-dialog :case-for-action="clientCaseDetails" v-on:action-completed="editDone"/>
                <delete-case :case-for-action="clientCaseDetails" v-on:delete-completed="deleteDone"/>
                <archive-case :case-for-action="clientCaseDetails" v-on:delete-completed="deleteDone"/>
              </v-row>
              <v-card-text>
                {{ clientCaseDetails.description }}
              </v-card-text>
              <v-row class="mx-2 mb-4 d-flex align-center">
                <v-card-subtitle><span class="font-weight-bold">Edytowano:</span>
                  {{ formatDateWithHours(clientCaseDetails.updated) }}
                </v-card-subtitle>
                <v-card-subtitle><span class="font-weight-bold">Edytowane przez:</span> {{
                    clientCaseDetails.updatedBy
                  }}
                </v-card-subtitle>
              </v-row>
            </v-card>
          </v-tab-item>
          <v-tab-item :value="'tab-2'" v-if="clientAdmin">
            <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">
              W panelu dostępów możesz nadać lub usunąć dostęp do sprawy. Użyj opcji "Nadaj dostęp",
              aby zobaczyć listę użytkowników, którzy mogą uzyskać dostęp do wybranej sprawy.
            </v-alert>
            <v-card flat>
              <v-card-text>

              </v-card-text>
            </v-card>
          </v-tab-item>
        </v-tabs-items>
        <v-divider></v-divider>
      </v-card>

    </v-row>
  </div>
</template>
<script>
import {mapActions, mapGetters, mapState} from "vuex";
import {formatDateWithHours} from "@/data/date-extensions";
import EditCaseDialog from "@/components/legal-app/clients/cases/dialogs/edit-case-dialog";
import DeleteCase from "@/components/legal-app/clients/cases/dialogs/delete-case";
import ArchiveCase from "@/components/legal-app/clients/cases/dialogs/archive-case";

export default {
  name: "case-details",
  components: {ArchiveCase, DeleteCase, EditCaseDialog},
  props: {
    selectedCase: {
      type: Object,
      required: true
    },
    clientItem: {
      required: true
    }
  },
  data: () => ({
    // dialog: false,
    caseForAction: null,
    tab: null,
  }),
  // watch: {
  //   dialog(visible) {
  //     if (visible) {
  //       let caseId = this.selectedCase.id
  //       console.warn('watcher fired', caseId)
  //       this.getCaseDetails({caseId});
  //     }
  //   }
  // },
  computed: {
    ...mapState('legal-app-client-store', ['clientCaseDetails']),
    ...mapGetters('auth', ['clientAdmin']),
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails']),
    ...mapActions('legal-app-client-store', ['getListOfGroupedCases']),

    formatDateWithHours(date) {
      return formatDateWithHours(date)
    },
    async editDone() {
      let clientId = this.$route.params.client;
      await this.getListOfGroupedCases({clientId});
    },
    async deleteDone() {
      let clientId = this.$route.params.client;
      await this.getListOfGroupedCases({clientId});

    },
  }
}
</script>

<style scoped>

</style>
