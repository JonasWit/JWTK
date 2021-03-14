export default function ({store, redirect}) {
  if (!store.getters["auth/legalAppAllowed"] || !store.getters["auth/portalAdmin"]) {
    redirect('/');
  }
}
