
<template>
  <div class="d-flex align-center" :class="{'flex-row-reverse': reverse}">
    <div class="d-flex align-center">
      <v-avatar :size="size">
        <img v-if="imageUrl" :src="imageUrl">
        <v-icon v-else>mdi-account</v-icon>
      </v-avatar>
      <div class="mx-3" v-if="username">
        {{ username }}
      </div>
    </div>
    <v-spacer></v-spacer>
    <slot name="append"></slot>
  </div>
</template>

<script>
export default {
  name: "user-header",
  props: {
    imageUrl: {
      required: false,
      type: String
    },
    username: {
      required: false,
      type: String
    },
    size: {
      required: false,
      type: String,
      default: '42'
    },
    reverse: {
      required: false,
      type: Boolean,
      default: false
    },
    link: {
      required: false,
      type: Boolean,
      default: true
    }
  },
};
</script>

<style scoped>
</style>
