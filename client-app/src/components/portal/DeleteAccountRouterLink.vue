<template>
  <router-link :to="{ 'name': 'DeleteAccount' }" class="shadow-lg shadow-white profile-link">
    <div class="profile-card flex flex-col">
      <svg class="h-10 w-10 m-auto" fill="none" stroke="currentColor" viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
          d="M13 7a4 4 0 11-8 0 4 4 0 018 0zM9 14a6 6 0 00-6 6v1h12v-1a6 6 0 00-6-6zM21 12h-6" />
      </svg>
      <span class="m-auto">Usuń Konto</span>
    </div>
  </router-link>
</template>

<script>
export default {
  name: "DeleteAccountRouterLink"
}
</script>

<style scoped>

</style>