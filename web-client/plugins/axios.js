﻿export default function ({$axios, store}) {
  $axios.setHeader('X-Requested-With', 'XMLHttpRequest');
  $axios.onRequest(config => {
    config.withCredentials = true;
  });

  $axios.onError(error => {
    if (error.response && error.response.status) {
      if ((status | 0) === 400) {
        if (error.response.data) {
          const {data} = error.response;
          store.dispatch('popup/error', data);
          return {error};
        }
      }
      if ((status | 0) === 401) {
        store.dispatch('popup/error', "Brak dostępu!");
        return {error};
      }
    }
  });
}
