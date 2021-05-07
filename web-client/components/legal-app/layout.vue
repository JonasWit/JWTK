<template>
  <v-container fluid>
    <v-row no-gutters>
      <v-col cols="2">
        <legalapp-navigation-drawer/>
      </v-col>
      <v-col cols="9">
        <slot name="content">
        </slot>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
export default {
  name: "layout"
};
</script>

<style scoped>

</style>
