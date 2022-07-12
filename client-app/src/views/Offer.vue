<template>
  <div class="div">
    <div class="flex flex-row justify-between header-top">
      <ToSRouterLink/>
      <PrivacyRouterLink/>
    </div>
    <section class="section-1">
      <div class="content text-center p-16 mt-2 text-blue-50">
        <h1 class="text-2xl md:text-5xl mb-4 tracking-wide font-bold">Zapraszamy do współpracy</h1>
        <h5 class="text-base sm:text-2xl font-medium">
          <span class="font-bold">Systemy Wspomagania Pracy</span> to rozwiązania dostosowane do potrzeb <span
            class="font-bold">Twojej firmy.</span>
        </h5>
        <h5 class="sm:text-2xl font-medium"> Tworzymy strony internetowe i aplikacje, które wspierają funkcjonowanie
          biznesu. </h5>
      </div>
    </section>
    <section class="my-10">
      <div class="text-center ">
        <h1 class="text-2xl md:text-5xl mb-4 tracking-wide font-bold text-customClassicBlue">Cennik</h1>
        <h5 class="mx-7">
          Zapraszamy do kontaktu, aby poznać szczegółową wycenę usługi. </h5>
        <h1 class="text-xl"><a href="mailto:systemywp@pm.me">systemywp@pm.me</a></h1>
      </div>
      <div class="flex flex-col md:flex-row flex-wrap justify-center">
        <div v-for=" item in others" :key="item.id" class="justify-between m-6 border-4 rounded-lg p-4 md:w-1/4">
          <div class="text-center">
            <h2 class="text-3xl text-center mb-4 text-customClassicBlue tracking-wide font-bold">{{ item.title }}</h2>
            <h2 class="text-2xl my-3 font-bold">{{ item.price }} </h2>
            <h5 class="text-center"> {{ item.subtitle }}</h5>
          </div>
          <ul class="list-disc px-3 text">
            <li v-for="(bullet, i) in item.bullet" :key="i" class="list-outside py-1">
              {{ bullet[0] }}
            </li>
          </ul>
        </div>
      </div>
    </section>
  </div>
</template>
<script>
import ToSRouterLink from '../components/portal/ToSRouterLink.vue';
import PrivacyRouterLink from '../components/portal/PrivacyRouterLink.vue';
export default {
    name: "Offer",
    data() {
        return {
            others: [
                {
                    id: 1,
                    title: "Aplikacje Webowe",
                    price: "od 3500 PLN netto",
                    bullet: [
                        ["Bezpłatna wycena i konsultacja co do technologii odpowiedniej dla projektu"],
                        ["Aplikacje do obługi firmy, sklepy internetowe"],
                        ["Możliwość wyboru technologii, w której stworzymy projekt. Zapewniamy darmową pomoc w dokonaniu właściwego wyboru."],
                        ["Technologie dla rozwiązań indywidualnych: Wordpress, Vue.js (Nuxt), Blazor WASM, .NET5 i wyższe, PostgreSQL"],
                    ],
                    img: "img/bubble-icons/icons8-tick-24.png"
                },
                {
                    id: 2,
                    title: "Strony Internetowe",
                    price: "od 1500 PLN netto",
                    bullet: [
                        ["Statyczne strony internetowe"],
                        ["Możliwość dodania dodatkowych funkcjonalności"],
                        ["Możliwość zastosowania CMS (Wordpress)"],
                    ],
                },
                {
                    id: 3,
                    title: "Aplikacje Desktopowe",
                    price: "od 4000 PLN netto",
                    bullet: [
                        ["Bezpłatna wycena i konsultacja co do technologii odpowiedniej dla projektu"],
                        ["Zawsze rozpoczynamy projekt w najnowszej dostępnej produkcyjnej wersji .NET"],
                        ["Aplikacje WPF"],
                        ["Aplikacje WinForms"],
                        ["Aplikacje MAUI"],
                        ["Integracja z bazą danych"],
                    ],
                },
                {
                    id: 4,
                    title: "Utrzymanie Hostingu",
                    price: "od 300 PLN netto",
                    subtitle: "Kwota miesięczna za aplikację",
                    bullet: [
                        ["Obsługa i utrzymanie stworzonej przez nas aplikacji lub statycznej strony internetowej w chmurze"],
                        ["Możliwość wyboru lokalizacji serwera, Używamy VPS dostarczanych przez firmę Linode"],
                        ["Monitorowanie stanu aplikacji"],
                        ["Regularne instalowanie łatek systemu"],
                    ],
                },
                {
                    id: 5,
                    title: "Utrzymanie Aplikacji",
                    price: "od 200 PLN netto",
                    subtitle: "Kwota miesięczna za aplikację",
                    bullet: [
                        ["Pomoc techniczna w przypadku błędów"],
                        ["Wprowadzanie nowych funkcjonalności"],
                        ["Zmiany istniejących funkcjonalności"],
                        ["Monitorowanie stanu aplikacji"],
                        ["Zmiany lub rozszeżenia do aplikacji wyceniane są indywidualnie"]
                    ],
                },
            ],
        };
    },
    components: { ToSRouterLink, PrivacyRouterLink }
}
</script>

<style>

.section-1 {
  position: relative;
  width: 100%;
  height: 40vh;
  display: flex;
  justify-content: center;
  overflow: hidden;
}

.section-1 .content {
  z-index: 1;
}

.section-1:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: linear-gradient(45deg, cornflowerblue, #000000);
  border-radius: 0 0 50% 50%/0 0 100% 100%;
  transform: scaleX(1.5);
}


</style>