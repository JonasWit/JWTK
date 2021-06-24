<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="mb-4 white--text" color="primary">
        <v-toolbar-title>Moje notatki</v-toolbar-title>
        <v-spacer></v-spacer>
        <add-note/>

      </v-toolbar>
      <v-expansion-panels focusable>
        <v-expansion-panel v-for="item in clientNotesList" :key="item[0].created" class="expansion">
          <v-expansion-panel-header class="text-uppercase">{{ formatDateToMonth(item[0].created) }}
          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <v-card v-for="object in item" :key="object.id" class="my-4">
              <v-row class="d-flex align-center">
                <v-col cols="4">
                  <v-card-title>
                    Dodano: {{ formatDate(object.created) }}
                  </v-card-title>
                  <v-card-subtitle>
                    Zmieniono: {{ formatDate(object.updated) }}
                  </v-card-subtitle>
                </v-col>
                <v-col cols="5">
                  <v-card-title>
                    Tytuł: {{ object.title }}
                  </v-card-title>
                </v-col>
                <v-col cols="3">
                  <notes-details :selected-note="object"/>
                </v-col>
              </v-row>
            </v-card>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </template>
  </layout>
</template>

<script>

import Layout from "@/components/legal-app/layout";
import {formatDate, formatDateToMonth} from "@/data/date-extensions";
import NotesDetails from "@/components/legal-app/notes/notes-details";
import AddNote from "@/components/legal-app/notes/add-note";
import {mapActions, mapState} from "vuex";

export default {
  name: "index",
  components: {AddNote, NotesDetails, Layout},
  data: () => ({}),
  fetch() {
    return this.getClientsNotes(this.$route.params.client);
  },
  computed: {
    ...mapState('legal-app-client-store', ['clientNotesList'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    formatDateToMonth(date) {
      return formatDateToMonth(date);
    },
    formatDate(date) {
      return formatDate(date);
    },
  }
};
</script>

<style scoped>

</style>
