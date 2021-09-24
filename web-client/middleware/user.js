export default function ({store, redirect}) {
  if (!store.getters["auth/user"] && !store.getters["auth/userAdmin"] && !store.getters["auth/portalAdmin"]) {
    redirect('/');
  }
}
