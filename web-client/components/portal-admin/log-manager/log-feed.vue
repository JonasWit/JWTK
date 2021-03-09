<template>
  <div v-scroll="onScroll">
    <padmin-log-item :log-item="l" v-for="l in logs" :key="`lon-item-${l.id}`"/>
  </div>
</template>

<script>

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
    cursor: 0,
    finished: false,
    loading: false
  }),
  created() {
    this.handleLogs();
  },
  computed: {
    query() {
      return `?cursor=${this.cursor}&take=10`;
    }
  },
  methods: {
    onScroll() {
      if (this.finished || this.loading) return;
      const loadMore = document.body.offsetHeight - (window.pageYOffset + window.innerHeight) < 500;

      if (loadMore) {
        this.handleLogs();
      }
    },
    handleLogs() {
      this.loading = true;
      console.log(this.query);
      this.loadLogs(this.query)
        .then(logs => {
          console.log(logs);
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
