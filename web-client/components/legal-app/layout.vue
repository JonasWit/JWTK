<template>
  <v-container>
    <v-row no-gutters>
      <v-col class="my-5">
        <slot name="content">
        </slot>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>


export default {
  name: "layout",

};
</script>

<style scoped>

</style>
