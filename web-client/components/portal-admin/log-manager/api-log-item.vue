<template>
  <v-list-item :key="`log-record-${logItem.id}`">
    <v-list-item-icon class="mx-0">
      <padmin-api-log-item-details :log-item="logItem"/>
    </v-list-item-icon>
    <v-list-item-content>
      <v-list-item-subtitle :class="getTypeColor(logItem.logLevel)">{{ logItem.logLevel }}</v-list-item-subtitle>
      <v-list-item-subtitle>{{ logItem.state }}</v-list-item-subtitle>
    </v-list-item-content>
    <v-spacer/>
    <v-list-item-content>
      <v-list-item-subtitle>Created: {{ logItem.created }}</v-list-item-subtitle>
    </v-list-item-content>
  </v-list-item>
</template>

<script>
import {SERVER_LOG_TYPES} from "@/data/enums";

export default {
  name: "api-log-item",
  props: {
    logItem: {
      type: Object,
      required: true
    }
  },
  methods: {
    getTypeColor(type) {
      if (type === SERVER_LOG_TYPES.WARNING) {
        return "warning--text";
      } else if (type === SERVER_LOG_TYPES.CRITICAL) {
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
