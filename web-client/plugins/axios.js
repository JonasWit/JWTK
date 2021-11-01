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
  $axios.onError(error => {
    if (error.response && error.response.status) {
      const {status} = error.response;
      if (status === 401) {
        store.dispatch('auth/clearLogin');
      }
    }
  });
}
