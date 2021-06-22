<template>
  <layout>
    <template v-slot:content>
      <h1>Notatki</h1>
      <v-expansion-panels focusable>
        <v-expansion-panel v-for="item in groupedNotes" :key="item[0].created" class="expansion">
          <v-expansion-panel-header class="text-uppercase">{{ formatDateToMonth(item[0].created) }}
          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <v-card v-for="object in item" :key="object.id" class="d-flex justify-space-between">
              <v-card-subtitle>
                {{ object.title }}
              </v-card-subtitle>
            </v-card>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>


    </template>
  </layout>

</template>

<script>

import Layout from "@/components/legal-app/layout";
import {formatDateToMonth} from "@/data/date-extensions";
import {groupByKey} from "@/data/functions";

export default {
  name: "index",
  components: {Layout},
  data: () => ({
    fullNotesList: [],
    groupedNotes: [],
  }),

  async fetch() {
    try {
      await this.getClientsNotes()
    } finally {
      this.groupByKey()
    }


  },

  methods: {
    async getClientsNotes() {
      try {
        let notesList = await this.$axios.$get(`/api/legal-app-clients-notes/client/${this.$route.params.client}/notes/titles-list`)
        this.fullNotesList = notesList
        console.warn('NotesList from API:', notesList)
      } catch (e) {
        console.log('error from API: client notes fetch', e)
      } finally {
        this.fullNotesList.forEach(x => {
          x.caseCreatedDate = this.formatDateToMonth(x.created);

        });
      }

    },

    formatDateToMonth(date) {
      return formatDateToMonth(date)
    },

    groupByKey() {
      let input = this.fullNotesList;
      let key = 'caseCreatedDate';
      const groups = groupByKey(input, key)
      this.groupedNotes = groups
      console.log('group by category fired', groups)

    },
  }
}
</script>

<style scoped>

</style>
