<template>
  <Nav/>
  <main class="px-12 py-6">
    <Authorization/>
    <router-view/>
  </main>
</template>

<script>
import Nav from "@/components/portal/navigation/Nav";
import Authorization from "@/components/portal/user/Authorization";
export default {
  components: {Authorization, Nav}
}
</script>
<style lang="scss">

</style>
