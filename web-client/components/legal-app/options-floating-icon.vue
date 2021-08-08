<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn color="primary" dark fab class="mr-8 options" v-on="{ ...tooltip, ...dialog }">
            <v-icon>
              mdi-cogs
            </v-icon>
          </v-btn>
        </template>
        <span>Ustawienia</span>
      </v-tooltip>
    </template>

    <v-card>
      <v-toolbar color="primary" dark>
        <v-toolbar-title>
          Ustawienia
        </v-toolbar-title>
      </v-toolbar>
      <v-container class="px-4" fluid>
        <v-switch
          v-model="alertsOn"
          label="Pokaż/ukryj podpowiedzi"
        ></v-switch>
      </v-container>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "options-floating-icon",
  data: () => ({
    dialog: false,
    alertsOn: true
  }),

}

</script>

<style scoped>

.options {
  position: fixed;
  z-index: 100;
  bottom: 110px;
  right: 15px
}


</style>
