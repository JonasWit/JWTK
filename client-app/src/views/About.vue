<template>
  <div class="div">
    <div class="flex flex-row justify-between header-top">
      <ToSRouterLink />
      <PrivacyRouterLink />
    </div>
    <section class="section-1">
      <div class="content text-center p-16 mt-2 text-white">
        <h1 class="text-2xl md:text-5xl mb-4 tracking-wide font-bold">Zapraszamy do współpracy</h1>
        <h5 class="text-base sm:text-2xl font-medium">
          <span class="font-bold">Systemy Wspomagania Pracy</span> to rozwiązania dostosowane do potrzeb <span
            class="font-bold">Twojej firmy.</span>
        </h5>
        <h5 class="sm:text-2xl font-medium"> Tworzymy strony internetowe i aplikacje, które wspierają funkcjonowanie
          biznesu. </h5>
      </div>
    </section>
    <section class="mx-1.5">
      <div class="flex flex-col md:flex-row mt-20">
        <div v-for="item in items" :key="item.id" class="my-2.5 w-full">
          <h1 class="font-bold tracking-wide text-center text-2xl text-customUltraViolet">{{ item.title }}</h1>
          <img :src="item.icon" class="m-auto">
          <div class="text-center">{{ item.text }}</div>
        </div>
      </div>
    </section>
    <section class="section-2 my-10 w-full border-t-4 border-customSecondaryGold">
      <div class="text-center p-16">
        <h1 class="text-2xl md:text-5xl mb-4 tracking-wide font-bold text-customUltraViolet">Technologie</h1>
        <h5 class=" my-10">
          Więcej informacji na temat technologii, które wykorzystujemy, możesz uzyskać na oficjlanych stronach
          podlinkowanych poniżej. </h5>
          <div class="flex flex-row justify-around flex-wrap">
            <div v-for="item in icons" :key="item.id" class="mx-auto">
              <a :href="item.url" target="_blank"><img :src="item.img" class="icon" /></a>
            </div>
          </div>
      </div>
    </section>

  </div>
</template>
<script>
import ToSRouterLink from '../components/portal/ToSRouterLink.vue';
import PrivacyRouterLink from '../components/portal/PrivacyRouterLink.vue';
export default {
    name: "About",
    data() {
        return {
            items: [
                {
                    id: 1,
                    title: "Bezpieczeństwo",
                    icon: "img/bubble-icons/icons8-security-configuration-100.png",
                    text: "Kładziemy nacisk na bezpieczeńswto naszych aplikacji. Korzystamy z najnowszych rozwiązań w zakresie bezpieczeństwa."
                },
                {
                    id: 2,
                    title: "Innowacyjność",
                    icon: "img/bubble-icons/icons8-innovation-100.png",
                    text: "Zawsze rozpoczynamy projekt w najnowszej dostępnej wersji danej technologii. Oferujemy również późniejsze utrzymanie i akutalizaję aplikacji."
                },
                {
                    id: 3,
                    title: "Elastyczność",
                    icon: "img/bubble-icons/icons8-couple-target-100.png",
                    text: "Jesteśmy otwarci na wszelkie zmiany i dodatki do aplikacji. " +
                        "Zarówno oferowanych już na portalu jak i stworonych na zamówienie. Oferujemy zawsze bezpłatną konsultację i wycenę każdej zmiany lub rozszeżenia."
                },
            ],
            icons: [
                {
                    id: 1,
                    url: "https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor",
                    img: "img/icons/Blazor2.png",
                },
                {
                    id: 2,
                    url: "https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet",
                    img: "img/icons/Net2.png",
                },
                {
                    id: 3,
                    url: "https://getbootstrap.com/",
                    img: "img/icons/icons8-bootstrap-48.png",
                },
                {
                    id: 4,
                    url: "https://vuejs.org/",
                    img: "img/icons/icons8-vue-js-48.png",
                },
                {
                    id: 5,
                    url: "https://vuetifyjs.com/en/",
                    img: "img/icons/vuetify.png",
                },
                {
                    id: 6,
                    url: "https://wordpress.com/",
                    img: "img/icons/icons8-wordpress-48.png",
                },
                {
                    id: 7,
                    url: "https://www.postgresql.org/",
                    img: "img/icons/icons8-postgresql-48.png",
                },
            ],
        };
    },
    components: { ToSRouterLink, PrivacyRouterLink }
}
</script>

<style>
.icon {
  height: 48px;
  margin: 22px 0 0 22px;
  padding: 5px;
}

.icon:hover {
  opacity: 0.7;

}
</style>