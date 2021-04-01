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
          User: {{ logItem.userEmail }}
        </div>
        <div class="my-4 subtitle-1">
          Description: {{ logItem.description }}
        </div>
        <div v-if="logItem.exceptionMessage" class="my-4 subtitle-1">
          Exception: {{ logItem.exceptionMessage }}
        </div>
        <div v-if="logItem.exceptionStackTrace" class="my-4 subtitle-1">
          Exception: {{ logItem.exceptionStackTrace }}
        </div>

      </v-card-text>
      <v-divider></v-divider>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "log-item-details",
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
