<template>
  <div>
    <v-card v-for="item in notesBasicList" :key="item.id">
      <v-card-title>{{ item.title }}</v-card-title>
      <v-card-subtitle>{{ item.created }}</v-card-subtitle>
      <v-card-subtitle>{{ item.id }}</v-card-subtitle>
    </v-card>
  </div>

</template>

<script>
import {mapActions, mapGetters} from "vuex";

export default {
  name: "cases-notes",
  props: {
    selectedCase: {
      required: true,
      default: null
    }
  },

  data: () => ({
    loading: false,


  }),

  async fetch() {

    return this.getNotesList()
  },

  computed: {
    ...mapGetters('legal-app-client-store', ['notesBasicList'])
  },

  methods: {
    ...mapActions('legal-app-client-store', ['getNotesListFromFetch']),

    async getNotesList() {

      try {
        if (this.loading) return;
        this.loading = true;

        let caseId = this.selectedCase.id
        return this.getNotesListFromFetch({caseId})
      } finally {
        this.loading = false;
      }


    }

  }
}
</script>

<style scoped>

</style>
