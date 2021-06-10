<template>
  <v-text-field v-model="valueData" required
                :label="filedLabel"></v-text-field>
</template>

<script>
export default {
  name: "text-field-wrapper",
  props: {
    value: {
      type: String,
      default: '',
      required: true
    },
    mapOperation: {
      type: Function,
      default: null,
      required: true
    },
    filedLabel: {
      type: String,
      default: '',
      required: true
    },
    validation: {
      type: Array,
      default: null,
      required: true
    }

  },
  data: () => ({
    valueData: ''
  }),

  mounted() {
    this.valueData = this.value
  },
  watch: {
    valueData(value) {
      this.valueData = this.mapOperation(value)
      console.warn("mapping", this.valueData)
      this.output();
    }
  },
  methods: {
    output: function () {
      this.$emit('input', this.valueData);
    }
  }
}
</script>

<style scoped>

</style>
