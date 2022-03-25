<template>
  <div class="grid grid-cols-3 md:grid-cols-6 gap-4">
    <NavButton text="Menu" navigate-to="GastronomyMenus" icon-path="/img/bubble-icons/icons8-menu-100.png"></NavButton>
    <NavButton text="Dania" navigate-to="GastronomyDishes" icon-path="/img/bubble-icons/icons8-dish-100.png"></NavButton>
    <NavButton text="Składniki" navigate-to="GastronomyIngredients" icon-path="/img/bubble-icons/icons8-ingredients-100.png"></NavButton>
    <NavButton text="Kalkulator" navigate-to="GastronomyCalculator" icon-path="/img/bubble-icons/icons8-calculator-100.png"></NavButton>
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