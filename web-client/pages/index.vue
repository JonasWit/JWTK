<template>
  <div>
    <home-welcome/>
  

  </div>
</template>

<script>
import HomeMainContent from "~/components/home-page/home-main-content";
import HomeProducts from "@/components/home-page/home-products-kancelaria";
import HomePricing from "@/components/home-page/home-pricing";

export default {
  components: {HomePricing, HomeProducts, HomeMainContent}
};
</script>
