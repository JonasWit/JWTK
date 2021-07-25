export default function ({store, redirect}) {
  if (!store.getters["auth/client"] && !store.getters["auth/clientAdmin"] && !store.getters["auth/portalAdmin"]) {
    redirect('/');
  }
}
