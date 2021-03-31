<template>
  <v-list-item @click="" :key="`log-record-${logItem.id}`">
    <v-list-item-content>
      <v-list-item-title>{{ logItem.description }}</v-list-item-title>
      <v-list-item-subtitle :class="getTypeColor(logItem.logType)">{{ logItem.logType }}</v-list-item-subtitle>
    </v-list-item-content>
    <v-spacer/>
    <v-list-item-content>
      <v-list-item-subtitle>Created: {{ logItem.created }}</v-list-item-subtitle>
      <v-list-item-subtitle v-if="logItem.description" class="py-0">Details: {{
          logItem.description
        }}
      </v-list-item-subtitle>
      <v-list-item-subtitle class="py-0">User Email: {{ logItem.userEmail }}</v-list-item-subtitle>
    </v-list-item-content>
  </v-list-item>
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
