<template>
  <Nav/>
  <main>
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
