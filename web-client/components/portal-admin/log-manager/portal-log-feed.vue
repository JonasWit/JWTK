<template>
  <div>
    <v-form ref="minLogDateForm">
      <v-row v-if="!loading">
        <v-col class="d-flex justify-center" cols="12" md="6">
          <v-date-picker :no-title="true" v-model="dates" range></v-date-picker>
        </v-col>
        <v-col class="d-flex justify-right" cols="12" md="6">
          <div class="d-flex flex-column">
            <v-checkbox class="mx-4" v-model="access" label="Access"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="exception" label="Exception"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="admin" label="Admin"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="personalData" label="Personal Data"></v-checkbox>
            <v-checkbox class="mt-0 mx-4" v-model="issue" label="Issue"></v-checkbox>
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
        <padmin-portal-log-item :log-item="l" v-for="l in logs" :key="`log-item-${l.id}`"/>
      </v-list>
    </div>
  </div>
</template>

<script>

export default {
  name: "portal-log-feed",
  data: () => ({
    dates: [],
    minDate: "",
    maxDate: "",
    access: true,
    exception: true,
    admin: true,
    personalData: true,
    issue: true,
    logs: [],
    searchConditionsProvided: false,
    cursor: 0,
    finished: false,
    loading: false,
  }),
  fetch() {
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
    onScroll() {
      if (this.finished || this.loading) return;
      const loadMore = document.body.offsetHeight - (window.pageYOffset + window.innerHeight) < 500;
      if (loadMore) {
        this.handleLogs();
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
      this.access = true;
      this.exception = true;
      this.admin = true;
      this.personalData = true;
      this.issue = true;
      this.finished = false;
      this.handleLogs();
    },
    refresh() {
      this.cursor = 0;
      this.logs = [];
      this.finished = false;
      this.$nuxt.refresh();
    },
    handleLogs() {
      console.warn('handle logs fired', this.query);

      return this.$axios.$get(`/api/portal-admin/log-admin/logs/split${this.query}`)
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
  }
};
</script>

<style scoped>

</style>
