<template>
  <div class="flex md:flex-row flex-col justify-center pt-5">
    <div class="p-2 m-auto">
      <DeleteAccountRouterLink />
    </div>
    <div class="p-2 m-auto">
      <ChangePasswordRouterLink />
    </div>
    <div class="p-2 m-auto">
      <PrivacyRouterLink />
    </div>
    <div class="p-2 m-auto">
      <ToSRouterLink />
    </div>
  </div>
</template>

<script>
import DeleteAccountRouterLink from '../components/portal/DeleteAccountRouterLink.vue';
import ChangePasswordRouterLink from '../components/portal/ChangePasswordRouterLink.vue';
import PrivacyRouterLink from '../components/portal/PrivacyRouterLink.vue';
import ToSRouterLink from '../components/portal/ToSRouterLink.vue';
export default {
    name: "Profile",
    components: { DeleteAccountRouterLink, ChangePasswordRouterLink, PrivacyRouterLink, ToSRouterLink }
}
</script>

<style scoped>

</style>