export default function ({$axios, store, error: nuxtError}) {
  $axios.setHeader('X-Requested-With', 'XMLHttpRequest');
  $axios.onRequest(config => {
    config.withCredentials = true;
  });
  // $axios.onResponse(res => {
  //   console.log(`Response URL: ${res?.config?.url}, Response Object: `, res);
  //   res.data.status = res.status;
  //   return res;
  // });
  // $axios.onError(error => {
  //   if (error.response && error.response.status && error.response.data) {
  //     const {status, data} = error.response;
  //     if ((status | 0) === 400) {
  //       store.dispatch('popup/error', data);
  //       return {error};
  //     }
  //   }
  // });
}
