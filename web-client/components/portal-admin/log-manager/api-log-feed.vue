<template>
  <div>
    <v-form ref="minLogDateForm">
      <v-row>
        <v-col class="d-flex justify-center" cols="12" md="6">
          <v-date-picker :no-title="true" v-model="dates" range></v-date-picker>
        </v-col>
        <v-col class="d-flex justify-right" cols="12" md="6">
          <div class="d-flex flex-column">
            <v-checkbox class="mx-4" v-model="information" label="Information"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="critical" label="Critical"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="debug" label="Debug"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="error" label="Error"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="none" label="None"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="trace" label="Trace"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="warning" label="Warning"></v-checkbox>
          </div>
        </v-col>
      </v-row>
    </v-form>

    <div class="d-flex justify-center mb-3">
      <v-btn text depressed class="mx-1" @click="search">Search</v-btn>
      <v-btn text depressed class="mx-1" @click="clear">Clear</v-btn>
    </div>
    <div class="d-flex justify-center">
      <v-btn text depressed color="warning" class="mx-1" @click="refresh">Refresh Logs</v-btn>
    </div>

    <div v-scroll="onScroll">
      <v-list>
        <api-log-item :log-item="l" v-for="l in logs" :key="`log-item-${l.id}`"/>
      </v-list>
    </div>
  </div>
</template>

<script>

import ApiLogItem from "@/components/portal-admin/log-manager/api-log-item";
import {getAPILogs} from "@/data/endpoints/admin/admin-panel-endpoints";

export default {
  name: "api-log-feed",
  components: {ApiLogItem},
  data: () => ({
    dates: [],
    minDate: "",
    maxDate: "",
    information: true,
    critical: true,
    debug: true,
    error: true,
    none: true,
    trace: true,
    warning: true,
    logs: [],
    searchConditionsProvided: false,
    cursor: 0,
    finished: false,
    loading: false,
  }),
  fetch() {
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
    }
  },
  computed: {
    query() {
      if (this.searchConditionsProvided) {
        return `/dates?from=${this.minDate}&to=${this.maxDate}&cursor=${this.cursor}&take=10&information=${this.information}&critical=${this.critical}&debug=${this.debug}&error=${this.error}&none=${this.none}&trace=${this.trace}&warning=${this.warning}`;
      } else {
        return `?cursor=${this.cursor}&take=10`;
      }
    },
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    },
  },
  methods: {
    refresh() {
      this.cursor = 0;
      this.logs = [];
      this.finished = false;
      this.$nuxt.refresh();
    },
    onScroll() {
      if (this.finished || this.loading) return;
      const loadMore = document.body.offsetHeight - (window.pageYOffset + window.innerHeight) < 500;
      if (loadMore) {
        return this.handleLogs();
      }
    },
    search() {
      if (this.loading === true) return;
      this.loading = true;
      this.logs = [];
      this.searchConditionsProvided = true;
      this.cursor = 0;
      this.handleLogs();

      this.loading = false;
    },
    clear() {
      this.searchConditionsProvided = false;
      this.cursor = 0;
      this.logs = [];
      this.dates = [];
      this.information = true;
      this.critical = true;
      this.debug = true;
      this.error = true;
      this.none = true;
      this.trace = true;
      this.warning = true;
      this.finished = false;
      this.handleLogs();
    },
    async handleLogs() {
      if (this.loading) return;
      this.loading = true;

      try {
        let response = await this.$axios.$get(getAPILogs(this.query));
        if (response.length === 0) {
          this.finished = true;
        } else {
          response.forEach(x => {
            if (!this.logs.some(y => y.id === x.id)) {
              this.logs.push(x);
            }
          });
          this.cursor += 10;
        }
      } catch (e) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.loading = false;
      }
    }
  }
};
</script>

<style scoped>

</style>
