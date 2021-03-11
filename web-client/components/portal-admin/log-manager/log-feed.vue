<template>
  <div>
    <v-form ref="minLogDateForm" v-model="validation.valid">
      <v-menu ref="menuMin" transition="scale-transition" offset-y min-width="auto" :close-on-content-click="false"
              :return-value.sync="form.minDate">
        <template v-slot:activator="{ on, attrs }">
          <v-text-field class="ma-3" :rules="validation.minDate" readonly v-bind="attrs" v-on="on"
                        v-model="form.minDate" label="From" prepend-icon="mdi-calendar"></v-text-field>
        </template>
        <v-date-picker :max="todayDate" v-model="form.minDate" no-title scrollable>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="menuMin = false">
            Cancel
          </v-btn>
          <v-btn text color="primary" @click="$refs.menuMin.save(form.minDate)">
            OK
          </v-btn>
        </v-date-picker>
      </v-menu>
      <v-menu ref="menuMax" transition="scale-transition" offset-y min-width="auto" :close-on-content-click="false"
              :return-value.sync="form.maxDate">
        <template v-slot:activator="{ on, attrs }">
          <v-text-field class="ma-3" :rules="validation.maxDate" readonly v-bind="attrs" v-on="on"
                        v-model="form.maxDate" label="To" prepend-icon="mdi-calendar"></v-text-field>
        </template>
        <v-date-picker :max="todayDate" v-model="form.maxDate" no-title scrollable>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="menuMax = false">
            Cancel
          </v-btn>
          <v-btn text color="primary" @click="$refs.menuMax.save(form.maxDate)">
            OK
          </v-btn>
        </v-date-picker>
      </v-menu>
    </v-form>
    <div class="d-flex justify-center mb-3">
      <v-btn text depressed class="mx-1" @click="search">Search</v-btn>
      <v-btn text depressed class="mx-1" @click="clear">Clear</v-btn>
    </div>
    <div class="d-flex justify-center">
      <v-btn text depressed color="warning" class="mx-1" @click="refresh">Refresh Logs</v-btn>
    </div>

    <div v-scroll="onScroll">
      <padmin-log-item :log-item="l" v-for="l in logs" :key="`log-item-${l.id}`"/>
    </div>
  </div>
</template>

<script>

const initForm = () => ({
  minDate: null,
  maxDate: null
});

export default {
  name: "log-feed",
  props: {
    loadLogs: {
      type: Function,
      required: true
    }
  },
  data: () => ({
    logs: [],
    searchConditionsProvided: false,
    cursor: 0,
    finished: false,
    loading: false,
    form: initForm,
    validation: {
      valid: false,
      minDate: [
        v => !!v || "Min date is required!",
      ],
      maxDate: [
        v => !!v || "Max date is required!",
      ],
    },
  }),
  created() {
    this.handleLogs();
  },
  computed: {
    query() {
      if (this.searchConditionsProvided) {
        return `/dates?from=${this.form.minDate}&to=${this.form.maxDate}&cursor=${this.cursor}&take=10`;
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
      if (!this.$refs.minLogDateForm.validate()) return;
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
      this.form = initForm();
      this.handleLogs();
    },
    refresh() {
      this.cursor = 0;
      this.logs = [];
      this.handleLogs();
    },
    handleLogs() {
      this.loading = true;

      this.loadLogs(this.query)
        .then(logs => {
          if (logs.length === 0) {
            this.finished = true;
          } else {
            logs.forEach(x => this.logs.push(x));
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
