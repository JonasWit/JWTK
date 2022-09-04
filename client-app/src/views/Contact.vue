<template>
<div>
 <div class="flex flex-row justify-between header-top">
    <ToSRouterLink />
    <PrivacyRouterLink />
  </div>
  <div>Contact page</div>
</div>
 </template>

<script>
import ToSRouterLink from '../components/portal/ToSRouterLink.vue';
import PrivacyRouterLink from '../components/portal/PrivacyRouterLink.vue';
export default {
    name: "Contact",
    components: { ToSRouterLink, PrivacyRouterLink }
}
</script>

<style scoped>

</style>