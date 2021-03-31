<template>
  <div class="mb-2 py-1">
    <v-card>
      <div class="d-flex align-center">
        <v-card-title>{{ logItem.message }}</v-card-title>
        <v-spacer/>
        <v-card-title :class="getTypeColor(logItem.logType)">{{ logItem.logType }}</v-card-title>
      </div>
      <v-card-subtitle v-if="logItem.description" class="py-0">Details: {{
          logItem.description
        }}
      </v-card-subtitle>
      <v-card-subtitle class="py-0">Created: {{ logItem.created }}</v-card-subtitle>
      <v-card-subtitle class="py-0">User Email: {{ logItem.userEmail }}</v-card-subtitle>
    </v-card>
  </div>
</template>

<script>
import {LOG_TYPES} from "@/data/enums";

export default {
  name: "log-item",
  props: {
    logItem: {
      type: Object,
      required: true
    }
  },
  methods: {
    getTypeColor(type) {
      if (type === LOG_TYPES.ISSUE) {
        return "warning--text";
      } else if (type === LOG_TYPES.EXCEPTION) {
        return "error--text";
      } else {
        return "success--text";
      }
    }
  }
};
</script>

<style scoped>

</style>
