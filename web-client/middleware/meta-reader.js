export default ({route, store}) => {
  const pageName = route.meta.reduce((pageName, meta) => meta.pageName)
  const fullPath = route.fullPath
  console.warn('from middleware', fullPath)
  console.warn('from middleware', pageName)
  store.commit('breadcrumbs-store/setMetaTag', {pageName, fullPath})


}
