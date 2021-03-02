export default function ({store, redirect}) {
  if (!store.getters["auth/client"]) {
    redirect('/');
  }
}
