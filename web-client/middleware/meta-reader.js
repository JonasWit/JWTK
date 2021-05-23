export default ({route, store}) => {
  const pageName = route.meta.reduce((pageName, meta) => meta.pageName || pageName)
  console.warn('from middleware', pageName)
  store.commit('setMetaTag', pageName)

}
