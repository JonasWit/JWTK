<template>
  <Nav/>
  <main>
    <Authorization/>
    <router-view/>
  </main>
</template>

<style lang="scss">

</style>
<script>
import Nav from "@/components/portal/navigation/Nav";
import Authorization from "@/components/portal/user/Authorization";
export default {
  components: {Authorization, Nav}
}
</script>