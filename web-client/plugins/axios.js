export default function ({$axios, store}) {
  $axios.setHeader('X-Requested-With', 'XMLHttpRequest');
  $axios.onRequest(config => {
    config.withCredentials = true;
  });

  $axios.onError(error => {
    console.log("popup function fired");
    if (error.response && error.response.status && error.response.data) {
      console.log("popup function started");
      const {status, data} = error.response;
      if ((status | 0) === 400) {
        console.log("popup function dispatched");
        store.dispatch('popup/error', data);
        return {error};
      }
    }
  });
}
