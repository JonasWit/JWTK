<template>
  <div class="mb-2 py-1">
    <v-card>
      <div class="d-flex align-center">
        <v-card-title>{{ logItem.message }}</v-card-title>
        <v-spacer/>
        <v-card-title :class="getTypeColor(logItem.logType)">{{ logItem.logType }}</v-card-title>
      </div>
      <v-card-subtitle v-if="logItem.exceptionDetails" class="py-0">Details: {{
          logItem.exceptionDetails
        }}
      </v-card-subtitle>
      <v-card-subtitle class="py-0">Created: {{ logItem.created }}</v-card-subtitle>
      <v-card-subtitle class="py-0">User Id: {{ logItem.userId }}</v-card-subtitle>
      <v-card-subtitle class="py-0">User Name: {{ logItem.username }}</v-card-subtitle>
    </v-card>
  </div>
</template>

<script>
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
      if (type === "Access") {
        return "warning--text";
      } else if (type === "PortalAdminAction") {
        return "warning--text";
      } else {
        return "success--text";
      }
    }
  }
};
</script>

<style scoped>

</style>
