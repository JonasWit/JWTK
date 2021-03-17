export default ({app, store}, inject) => {
  inject('notifier', {
    showSuccessMessage(content) {
      store.commit('snackbar/showSuccessMessage', content);
    },
    showWarningMessage(content) {
      store.commit('snackbar/showWarningMessage', content);
    },
    showErrorMessage(content) {
      store.commit('snackbar/showErrorMessage', content);
    }
  });
}
