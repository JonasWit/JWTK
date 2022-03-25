<template>
<section class="bg-blue-500">
  <div class="text-center p-24 text-white">
    <h1 class="text-3xl mb-4 mt-5 tracking-wide font-black">Zapraszamy do współpracy</h1>
    <h5 class="text-2xl font-medium">
      <span>Systemy Wspomagania Pracy</span> to rozwiązania dostosowane do potrzeb Twojej firmy.</h5>
    <h5 class="text-2xl font-medium"> Tworzymy strony internetowe i aplikacje, które wspierają funkcjonowanie biznesu. </h5>
  </div>
  </section>       
<section>
  <div class="flex flex-row mt-20">
    <div v-for="item in items" :key="item.id" class="m-5">
      <h1>{{ item.title }}</h1>
      <img class="m-auto" :src="item.icon">
      <div>{{ item.text }}</div>
    </div>
  </div>
</section>  
</template>
<script>
export default {
  name: "About",
  data () {
    return {
      items: [
        {
          id: 1,
          title: 'Bezpieczeństwo',
          icon: 'img/bubble-icons/icons8-security-configuration-100.png',
          text: 'Kładziemy nacisk na bezpieczeńswto naszych aplikacji. Korzystamy z najnowszych rozwiązań w zakresie bezpieczeństwa.'
        },
        {
          id: 2,
          title: 'Innowacyjność',
          icon: 'img/bubble-icons/icons8-innovation-100.png',
          text: 'Zawsze rozpoczynamy projekt w najnowszej dostępnej wersji danej technologii. Oferujemy również późniejsze utrzymanie i akutalizaję aplikacji.'
        },
        {
          id: 3,
          title: 'Elastyczność',
          icon: 'img/bubble-icons/icons8-couple-target-100.png',          
          text: 'Jesteśmy otwarci na wszelkie zmiany i dodatki do aplikacji. Zarówno oferowanych już na portalu jak i stworonych na zamówienie. Oferujemy zawsze bezpłatną konsultację i wycenę każdej zmiany lub rozszeżenia.'
        },
      ],
     
    };
  },
}
</script>

<style scoped>

</style>