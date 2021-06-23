<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="mb-4">
        <v-toolbar-title>Moje notatki</v-toolbar-title>
        <v-spacer></v-spacer>
        <add-note/>

      </v-toolbar>
      <v-expansion-panels focusable>
        <v-expansion-panel v-for="item in groupedNotes" :key="item[0].created" class="expansion">
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
import {groupByKey} from "@/data/functions";
import NotesDetails from "@/components/legal-app/notes/notes-details";
import AddNote from "@/components/legal-app/notes/add-note";

export default {
  name: "index",
  components: {AddNote, NotesDetails, Layout},
  data: () => ({
    fullNotesList: [],
    groupedNotes: [],
  }),

  async fetch() {
    try {
      await this.getClientsNotes();
    } finally {
      this.groupByKey();
    }
  },

  methods: {
    async getClientsNotes() {
      try {
        let notesList = await this.$axios.$get(`/api/legal-app-clients-notes/client/${this.$route.params.client}/notes/titles-list`);
        this.fullNotesList = notesList;
        console.warn('NotesList from API:', notesList);
      } catch (e) {
        console.log('error from API: client notes fetch', e);
      } finally {
        this.fullNotesList.forEach(x => {
          x.caseCreatedDate = this.formatDateToMonth(x.created);

        });
      }

    },

    formatDateToMonth(date) {
      return formatDateToMonth(date);
    },

    formatDate(date) {
      return formatDate(date);
    },

    groupByKey() {
      let input = this.fullNotesList;
      let key = 'caseCreatedDate';
      const groups = groupByKey(input, key);
      this.groupedNotes = groups;
      console.log('group by category fired', groups);

    },


  }
};
</script>

<style scoped>

</style>
