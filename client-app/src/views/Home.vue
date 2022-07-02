<template>
<div>
<div class="section-1">
    <div class="content text-center p-16 mt-2 text-blue-50">
      <h1 class="text-2xl md:text-5xl mb-4 tracking-wide font-bold">
        Witaj na stronie Systemy Wspomagania Pracy!
      </h1>
      <h5 class="text-base sm:text-2xl font-medium">
        <span class="font-bold">Wybierz </span>rozwiązania dostosowane do potrzeb <span
          class="font-bold">Twojej firmy.</span></h5>
      <h5 class="sm:text-2xl font-medium"> Kliknij na odbowiedni kafelek poniżej, aby sprawdzić szczegóły. </h5>
    </div>
  </div>
  <div>
    <div class="my-8 md:grid md:grid-cols-3 md:h-96 md:gap-x-4 px-4 xs:flex xs:flex-col xs:space-y-4 xs:grow">
      <div class="portal-card">
        <img alt="gastronomy" class="w-full h-32 sm:h-48 object-cover" src="/img/restaurant-card-sm.jpg">
        <div class="m-4">
          <span class="font-bold">Gastronomia</span>
          <span
              class="block text-gray-500 text-sm">Twórz własne dania i komponuj z nich Menu. Automatycznie wyliczaj ilości potrzebnych składników.</span>
        </div>
        <div class="portal-badge">
          <span>Wkrótce Darmowy Dostęp</span>
        </div>
        <router-link :to="{ 'name': 'GastronomyControlPanel' }">
          <div class="card-button">
            Przejdź do aplikacji
          </div>
        </router-link>
      </div>
      <div class="portal-card">
        <img alt="logistics" class="w-full h-32 sm:h-48 object-cover" src="/img/warehouse-card-sm.jpg">
        <div class="m-4">
          <span class="font-bold">Logistyka</span>
          <span class="block text-gray-500 text-sm">Zarządzaj Logistyką</span>
        </div>
        <div class="portal-badge">
          <span>Wkrótce Darmowy Dostęp</span>
        </div>
        <router-link :to="{ 'name': 'GastronomyControlPanel' }">
          <div class="card-button">
            Przejdź do aplikacji
          </div>
        </router-link>
      </div>
    </div>
  </div>
</div>  
</template>
<script>


export default {
 
  name: 'Home',
}
</script>

