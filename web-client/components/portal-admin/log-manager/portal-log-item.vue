<template>
  <v-list-item :key="`log-record-${logItem.id}`">
    <v-list-item-icon class="mx-0">
      <portal-log-item-details :log-item="logItem"/>
    </v-list-item-icon>
    <v-list-item-content>
      <v-list-item-title :class="getTypeColor(logItem.logType)">{{ logItem.logType }}</v-list-item-title>
      <v-list-item-subtitle v-if="logItem.description" class="py-0">Details: {{
          logItem.description
        }}
      </v-list-item-subtitle>
    </v-list-item-content>
    <v-spacer/>
    <v-list-item-content>
      <v-list-item-subtitle>Created: {{ logItem.created }}</v-list-item-subtitle>
      <v-list-item-subtitle class="py-0">User Email: {{ logItem.userEmail }}</v-list-item-subtitle>
    </v-list-item-content>
  </v-list-item>
</template>

<script>
import {PORTAL_LOG_TYPES} from "@/data/enums";
import PortalLogItemDetails from "@/components/portal-admin/log-manager/portal-log-item-details";

export default {
  name: "portal-log-item",
  components: {PortalLogItemDetails},
  props: {
    logItem: {
      type: Object,
      required: true
    }
  },
  methods: {
    getTypeColor(type) {
      if (type === PORTAL_LOG_TYPES.ISSUE) {
        return "warning--text";
      } else if (type === PORTAL_LOG_TYPES.EXCEPTION) {
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
