<template>
  <div>
    <ul>
      <li>
        <router-link to="/">
          <span>Strona główna</span>
        </router-link>
      </li>
      <li>
        <router-link :to="{ 'name': 'About' }">
          <span>O Nas</span>
        </router-link>
      </li>
      <li>
        <router-link :to="{ 'name': 'Contact' }">
          <span>Kontakt</span>
        </router-link>
      </li>
      <li>
        <router-link :to="{ 'name': 'Privacy' }">
          <span>Polityka Prywatności</span>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "MainNav"
}
</script>

<style scoped>

</style>