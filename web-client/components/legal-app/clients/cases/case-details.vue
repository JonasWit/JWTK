<template>
  <div>
    <v-row justify="center">
      <v-dialog v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
        <template v-slot:activator="{ on, attrs }">
          <v-btn color="primary" dark v-bind="attrs" v-on="on">
            Otwórz
          </v-btn>
        </template>
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
          <v-row class="mx-2 my-2 d-flex align-center">
            <v-card-title>Opis sprawy</v-card-title>
            <v-card-title> Sygnatura: {{ clientCaseDetails.signature }}</v-card-title>
            <edit-case-dialog :case-for-action="clientCaseDetails" v-on:action-completed="editDone"/>
            <delete-case :case-for-action="clientCaseDetails" v-on:action-completed="deleteDone"/>
            <!--            <edit-note-dialog :note-for-action="noteDetails" v-on:action-completed="editDone"/>-->
            <!--            <delete-note-dialog :note-for-action="noteDetails" v-on:delete-completed="deleteDone"/>-->
            <!--            <v-checkbox v-model="checkbox" :label="labelCondition(noteDetails.public)" :value="noteDetails.public"-->
            <!--                        value disabled></v-checkbox>-->
          </v-row>
          <v-card-text>
            {{ clientCaseDetails.description }}
          </v-card-text>
          <v-row class="mx-2 mb-4 d-flex align-center">
            <v-card-subtitle><span class="font-weight-bold">Edytowano:</span>
              {{ formatDateWithHours(clientCaseDetails.updated) }}
            </v-card-subtitle>
            <v-card-subtitle><span class="font-weight-bold">Edytowane przez:</span> {{ clientCaseDetails.updatedBy }}
            </v-card-subtitle>
          </v-row>
          <v-divider></v-divider>

        </v-card>
      </v-dialog>
    </v-row>

  </div>


</template>

<script>
import {mapActions, mapGetters} from "vuex";
import {formatDateWithHours} from "@/data/date-extensions";
import EditCaseDialog from "@/components/legal-app/clients/cases/dialogs/edit-case-dialog";
import DeleteCase from "@/components/legal-app/clients/cases/dialogs/delete-case";

export default {
  name: "case-details",
  components: {DeleteCase, EditCaseDialog},
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
    dialog: false,
    caseForAction: null,
  }),
  watch: {
    dialog(visible) {
      if (visible) {
        let caseId = this.selectedCase.id
        this.getCaseDetails({caseId});
      }
    }
  },
  computed: {
    ...mapGetters('legal-app-client-store', ['clientCaseDetails'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails']),
    formatDateWithHours(date) {
      return formatDateWithHours(date)
    },
    async editDone() {
      let caseId = this.selectedCase.id
      await this.getCaseDetails(caseId);
    },
    async deleteDone() {
      this.dialog = false;
    },
  }
}
</script>

<style scoped>

</style>
