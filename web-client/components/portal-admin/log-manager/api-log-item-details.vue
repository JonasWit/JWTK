<template>
  <v-dialog v-model="dialog" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-3" icon v-bind="attrs" v-on="on">
        <v-icon medium color="success">mdi-dots-horizontal-circle-outline</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title>Log Record Detials
        <v-spacer></v-spacer>
        <v-btn icon @click="dialog = false">
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-card-title>
      <v-card-text class="my-1">
        <div class="my-4 subtitle-1">
          Level: {{ logItem.logLevel }}
        </div>
        <div v-if="logItem.state" class="my-4 subtitle-1">
          State: {{ logItem.state }}
        </div>
        <div v-if="logItem.eventName" class="my-4 subtitle-1">
          Event: {{ logItem.eventName }}
        </div>
        <div v-if="logItem.exceptionMessage" class="my-4 subtitle-1">
          Exception: {{ logItem.exceptionMessage }}
        </div>
        <div v-if="logItem.stackTrace" class="my-4 subtitle-1">
          Exception: {{ logItem.stackTrace }}
        </div>
      </v-card-text>
      <v-divider></v-divider>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "api-log-item-details",
  props: {
    logItem: {
      type: Object,
      required: true
    }
  },
  data: () => ({
    dialog: false,
  }),
};
</script>

<style scoped>

</style>
