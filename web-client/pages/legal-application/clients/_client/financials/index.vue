<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3">
        <v-toolbar-title class="mr-3">
          Financials
        </v-toolbar-title>
      </v-toolbar>
      <v-card tile>
        <v-row>
          <v-col class="mx-2">
            <v-list class="d-flex justify-space-between">
              <v-list-item-content>
                <v-list-item-subtitle></v-list-item-subtitle>
                <v-list-item-subtitle>
                </v-list-item-subtitle>
              </v-list-item-content>
            </v-list>
          </v-col>
          <v-col class="mx-2">
            <v-list class="d-flex justify-space-between">
              <v-list-item-content>
                <v-list-item-subtitle></v-list-item-subtitle>
                <v-list-item-subtitle></v-list-item-subtitle>
              </v-list-item-content>
            </v-list>
          </v-col>
          <v-col class="mx-2">
            <v-list class="d-flex justify-md-end justify-sm-space-between">
              <v-list-item>delete</v-list-item>
              <v-list-item>edit</v-list-item>
            </v-list>
          </v-col>
        </v-row>
      </v-card>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";

export default {
  name: "index",
  components: {Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],

  data: () => ({
    listOfRecordedWork: [],
    dates: [],
    minDate: "",
    maxDate: "",
    searchConditionsProvided: false,
    finished: false,
    loading: false,
  }),

  async fetch() {
    console.warn('fetch fired');
    return this.handleLogs();
  },

  watch: {
    dates(dates) {
      if (dates.length === 0) {
        this.minDate = new Date();
        this.maxDate = new Date();
      }
      if (dates.length === 1) {
        this.minDate = dates[0];
        this.maxDate = dates[0];
      }
      if (dates.length === 2) {
        let fromDate = dates[0].replace(/-/g, "");
        let toDate = dates[1].replace(/-/g, "");

        let firstDateInt = parseInt(fromDate);
        let secondDateInt = parseInt(toDate);

        if (firstDateInt > secondDateInt) {
          this.minDate = dates[1];
          this.maxDate = dates[0];
        } else {
          this.minDate = dates[0];
          this.maxDate = dates[1];
        }
      }

      console.log('fromDate', this.minDate);
      console.log('toDate', this.maxDate);
    }
  },
  computed: {
    query() {
      if (this.searchConditionsProvided) {
        return `/dates?from=${this.minDate}&to=${this.maxDate}&cursor=${this.cursor}&take=10&access=${this.access}&exception=${this.exception}&admin=${this.admin}&personalData=${this.personalData}&issue=${this.issue}`;
      } else {
        return `?cursor=${this.cursor}&take=10`;
      }
    },
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    },
  },

  methods: {
    handleLogs() {
      if (this.loading) return;
      this.loading = true;

      console.warn('handle logs fired', this.query);
      return this.$axios.$get(`/api/legal-app-clients-finance/client/${this.$route.params.client}/finance-records${this.query}`)
        .then(logs => {
          console.log('logs', logs);
          if (logs.length === 0) {
            this.finished = true;
          } else {
            logs.forEach(x => {
              if (!this.logs.some(y => y.id === x.id)) {
                this.logs.push(x);
              }
            });
            this.cursor += 10;
          }
        })
        .finally(() => this.loading = false);
    }


  },


}
</script>

<style scoped>

</style>
