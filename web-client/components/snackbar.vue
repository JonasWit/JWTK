<template>
  <v-snackbar v-model="show" timeout="2000">
    <v-icon :color="color">{{ icon }}</v-icon>
    {{ message }}
  </v-snackbar>
</template>

<script>
export default {
  data: () => ({
    show: false,
    message: '',
    color: '',
    icon: ''
  }),
  created() {
    this.$store.subscribe((mutation, state) => {
      if (mutation.type === 'snackbar/showMessage' ||
        mutation.type === 'snackbar/showSuccessMessage' ||
        mutation.type === 'snackbar/showWarningMessage' ||
        mutation.type === 'snackbar/showErrorMessage') {
        this.message = state.snackbar.content;
        this.color = state.snackbar.color;
        this.icon = state.snackbar.icon;
        this.show = true;
      }
    });
  }
};
</script>
