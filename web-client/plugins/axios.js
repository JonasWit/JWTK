export default function ({$axios, store}) {
  $axios.setHeader('X-Requested-With', 'XMLHttpRequest');
  $axios.onRequest(config => {
    config.withCredentials = true;
  });
}
