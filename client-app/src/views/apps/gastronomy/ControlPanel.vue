<template>
  <div class="grid grid-cols-3 md:grid-cols-6 gap-4 p-10">
    <NavButton icon-path="/img/bubble-icons/icons8-menu-100.png" navigate-to="GastronomyMenus" text="Menu"></NavButton>
    <NavButton icon-path="/img/bubble-icons/icons8-dish-100.png" navigate-to="GastronomyDishes"
               text="Dania"></NavButton>
    <NavButton icon-path="/img/bubble-icons/icons8-ingredients-100.png" navigate-to="GastronomyIngredients"
               text="Składniki"></NavButton>
    <NavButton icon-path="/img/bubble-icons/icons8-calculator-100.png" navigate-to="GastronomyCalculator"
               text="Kalkulator"></NavButton>
  </div>
</template>

<script>
import NavButton from "@/components/applicationNavigationButtons/NavButton";

export default {
  name: "ControlPanel",
  components: {NavButton},
}
</script>

<style scoped>

</style>