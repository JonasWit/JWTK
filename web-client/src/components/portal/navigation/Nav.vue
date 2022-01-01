<template>
<nav>
  <div>
    <h1>
      <router-link to="/">systemywp</router-link>    
    </h1>
  </div>
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
      <router-link :to="{ 'name': 'About' }">
        <span>Kontakt</span>
      </router-link>
    </li>
    <li>
      <router-link :to="{ 'name': 'Privacy' }">
        <span>Polityka Prywatności</span>
      </router-link>
    </li>
  </ul>
</nav>
</template>

<script>
export default {
  name: "Nav"
}
</script>

<style scoped>

</style>