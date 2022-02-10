<template>
  <div>
    <div>
      <h4 class="font-bold mt-12 pb-2 border-b border-gray-200 text-xl text-center">Rozwiązania dla Firm</h4>
      <div class="mt-8 grid lg:grid-cols-3 gap-7">
        <router-link :to="{ 'name': 'Gastronomy' }">
          <div class="portal-card">
            <img src="/img/restaurant-card-sm.jpg" alt="gastronomy" class="w-full h-32 sm:h-48 object-cover">
            <div class="m-4">
              <span class="font-bold">Gastronomia</span>
              <span
                  class="block text-gray-500 text-sm">Twórz własne dania i komponuj z nich Menu. Automatycznie wyliczaj ilości potrzebnych składników.</span>
            </div>
            <div class="portal-badge">
              <span class="text-customClassicBlue">Wkrótce Darmowy Dostęp</span>
            </div>
          </div>
        </router-link>
        <router-link :to="{ 'name': 'Gastronomy' }">
          <div class="portal-card">
            <img src="/img/warehouse-card-sm.jpg" alt="logistics" class="w-full h-32 sm:h-48 object-cover">
            <div class="m-4">
              <span class="font-bold">Logistyka</span>
              <span class="block text-gray-500 text-sm">Zarządzaj Logistyką</span>
            </div>
            <div class="portal-badge">
              <span class="text-customClassicBlue">Wkrótce Darmowy Dostęp</span>
            </div>
          </div>
        </router-link>
        <router-link :to="{ 'name': 'Solutions' }">
          <div class="portal-card">
            <img src="/img/individual.jpg" alt="individual-solutions" class="w-full h-32 sm:h-48 object-cover">
            <div class="m-4">
              <span class="font-bold">Rozwiązania Indywidualne</span>
              <span class="block text-gray-500 text-sm">Projektujemy i budujemy aplikacje dla każdego rodzaju działalności</span>
            </div>
            <div class="portal-badge">
              <span class="text-customClassicBlue">Darmowa Wycena</span>
            </div>
          </div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>

export default {
  name: 'Home',
  components: {},
  setup() {


  }
}
</script>

