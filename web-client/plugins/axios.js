export default function ({$axios, store}) {
  $axios.setHeader('X-Requested-With', 'XMLHttpRequest');
  $axios.onRequest(config => {
    config.withCredentials = true;
  });

  $axios.onError(error => {
    if (error.response && error.response.status && error.response.data) {
      console.warn('Axios Error:', error);
      const {status, data} = error.response;
      if (status) {
        store.dispatch('axios-manager/error', data);
        return {error};
      }
    }
  });
}
