export default function ({$axios, store}) {
  $axios.setHeader('X-Requested-With', 'XMLHttpRequest');
  $axios.onRequest(config => {
    config.withCredentials = true;
  });

  $axios.onError(error => {
    console.log("popup function fired");
    if (error.response && error.response.status && error.response.data) {
      const {status, data} = error.response;
      console.log(error);
      if ((status | 0) === 400) {
        store.dispatch('popup/error', data);
        return {error};
      }
      if ((status | 0) === 401) {
        store.dispatch('popup/error', data);
        return {error};
      }
    }
  });
}
