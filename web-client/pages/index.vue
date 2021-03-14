<template>
  <div>
    <p>Home Page</p>
    <home-welcome/>
    <home-main-content />
  </div>
</template>

<script>
import HomeMainContent from "~/components/home-page/home-main-content";
export default {
  components: {HomeMainContent}
};
</script>
