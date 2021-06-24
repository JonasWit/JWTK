const initState = () => ({});

export const state = initState;

export const actions = {
  error(data) {
    this.$notifier.showErrorMessage(data);
    console.error('Error from API => ', data);
  },
  success(data) {
    this.$notifier.showSuccessMessage(data);
    console.log('Success from API => ', data);
  },
  warning(data) {
    this.$notifier.showWarningMessage(data);
    console.warn('Warning from API => ', data);
  },
};
