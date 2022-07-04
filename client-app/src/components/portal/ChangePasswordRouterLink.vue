<template>
  <router-link :to="{ 'name': 'ChangePassword' }" class="shadow-lg shadow-white profile-link">
    <div class="profile-card flex flex-col">
      <svg class="h-10 w-10 m-auto" fill="none" stroke="currentColor" viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
          d="M15 7a2 2 0 012 2m4 0a6 6 0 01-7.743 5.743L11 17H9v2H7v2H4a1 1 0 01-1-1v-2.586a1 1 0 01.293-.707l5.964-5.964A6 6 0 1121 9z">
        </path>
      </svg>
      <span class="m-auto">Zmień Hasło</span>
    </div>
  </router-link>
</template>

<script>
export default {
  name: "ChangePasswordRouterLink"
}
</script>

<style scoped>

</style>