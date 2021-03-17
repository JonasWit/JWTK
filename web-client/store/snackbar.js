export const state = () => ({
  content: '',
  color: '',
  icon: ''
});

export const mutations = {
  showSuccessMessage(state, payload) {
    state.content = payload;
    state.color = 'success';
    state.icon = 'mdi-check';
  },
  showWarningMessage(state, payload) {
    state.content = payload;
    state.color = 'warning';
    state.icon = 'mdi-alert-circle';
  },
  showErrorMessage(state, payload) {
    state.content = payload;
    state.color = 'error';
    state.icon = 'mdi-alert-circle';
  }
};
