const initState = () => ({});

export const state = initState;

export const actions = {
  error(message) {
    this.$notifier.showErrorMessage(message);
    console.error('Error from API => ', message);
  },
  success(message) {
    this.$notifier.showSuccessMessage(message);
    console.log('Success from API => ', message);
  },
  warning(message) {
    this.$notifier.showWarningMessage(message);
    console.warn('Warning from API => ', message);
  },
};
