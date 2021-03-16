export default function ({$axios, store}) {
  $axios.setHeader('X-Requested-With', 'XMLHttpRequest');
  $axios.onRequest(config => {
    config.withCredentials = true;
  });

  $axios.onError(error => {
    if (error.response && error.response.status) {
      if ((status | 0) === 400) {
        if (error.response.data) {
          const {data} = error.response;
          console.log('popup should open');
          store.dispatch('popup/error', data);
          return {error};
        }
      }
      if ((status | 0) === 401 || (status | 0) === 403) {
        console.log('popup should open');
        store.dispatch('popup/error', "Brak dostępu - zaloguj się pomownie!");
        return {error};
      }

    }
  });
}
