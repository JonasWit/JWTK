<template>
  <section class="section-1">
    <div class="content text-center p-16 mt-2 text-blue-50">
      <h1 class="text-2xl md:text-5xl mb-4 tracking-wide font-bold">Zapraszamy do współpracy</h1>
      <h5 class="text-base sm:text-2xl font-medium">
        <span class="font-bold">Systemy Wspomagania Pracy</span> to rozwiązania dostosowane do potrzeb <span
          class="font-bold">Twojej firmy.</span></h5>
      <h5 class="sm:text-2xl font-medium"> Tworzymy strony internetowe i aplikacje, które wspierają funkcjonowanie
        biznesu. </h5>
    </div>
  </section>
  <section class="mx-1.5">
    <div class="flex flex-col md:flex-row mt-20">
      <div v-for="item in items" :key="item.id" class="my-2.5 w-full">
        <h1 class="font-bold tracking-wide text-center text-2xl text-customClassicBlue">{{ item.title }}</h1>
        <img :src="item.icon" class="m-auto">
        <div class="text-center">{{ item.text }}</div>
      </div>
    </div>
  </section>
  <section class="section-2 my-10 w-full border-t-4 border-b-4">
    <div class="text-center p-16">
      <h1 class="text-2xl md:text-5xl mb-4 tracking-wide font-bold text-customClassicBlue">Technologie</h1>
      <h5 class="my-10">
        Więcej informacji na temat technologii, które wykorzystujemy, możesz uzyskać na oficjlanych stronach
        podlinkowanych poniżej. </h5>
      <div class="flex flex-row justify-around">
        <div v-for="item in icons" :key="item.id" class="mx-auto">
          <a :href="item.url" target="_blank"><img :src="item.img" class="icon"/></a>
        </div>
      </div>
    </div>
  </section>
  <section class="my-10">
    <div class="text-center ">
      <h1 class="text-2xl md:text-5xl mb-4 tracking-wide font-bold text-customClassicBlue">Cennik</h1>
      <h5 class="mx-7">
        Zapraszamy do kontaktu, aby poznać szczegółową wycenę usługi. </h5>
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
</template>
<script>
export default {
  name: "About",
  data() {
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
          text: 'Jesteśmy otwarci na wszelkie zmiany i dodatki do aplikacji. ' +
              'Zarówno oferowanych już na portalu jak i stworonych na zamówienie. Oferujemy zawsze bezpłatną konsultację i wycenę każdej zmiany lub rozszeżenia.'
        },
      ],
      icons: [
        {
          id: 1,
          url: 'https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor',
          img: 'img/icons/Blazor2.png',
        },
        {
          id: 2,
          url: 'https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet',
          img: 'img/icons/Net2.png',
        },
        {
          id: 3,
          url: 'https://getbootstrap.com/',
          img: 'img/icons/icons8-bootstrap-48.png',
        },
        {
          id: 4,
          url: 'https://vuejs.org/',
          img: 'img/icons/icons8-vue-js-48.png',
        },
        {
          id: 5,
          url: 'https://vuetifyjs.com/en/',
          img: 'img/icons/vuetify.png',
        },
        {
          id: 6,
          url: 'https://wordpress.com/',
          img: 'img/icons/icons8-wordpress-48.png',
        },
        {
          id: 7,
          url: 'https://www.postgresql.org/',
          img: 'img/icons/icons8-postgresql-48.png',
        },
      ],
      others: [
        {
          id: 1,
          title: 'Aplikacje Webowe',
          price: 'od 3500 PLN netto',
          bullet: [
            ['Bezpłatna wycena i konsultacja co do technologii odpowiedniej dla projektu'],
            ['Aplikacje do obługi firmy, sklepy internetowe'],
            ['Możliwość wyboru technologii, w której stworzymy projekt. Zapewniamy darmową pomoc w dokonaniu właściwego wyboru.'],
            ['Technologie dla rozwiązań indywidualnych: Wordpress, Vue.js (Nuxt), Blazor WASM, .NET5 i wyższe, PostgreSQL'],
          ],
          img: 'img/bubble-icons/icons8-tick-24.png'

        },
        {
          id: 2,
          title: 'Strony Internetowe',
          price: 'od 1500 PLN netto',
          bullet: [
            ['Statyczne strony internetowe'],
            ['Możliwość dodania dodatkowych funkcjonalności'],
            ['Możliwość zastosowania CMS (Wordpress)'],
          ],
        },
        {
          id: 3,
          title: 'Aplikacje Desktopowe',
          price: 'od 4000 PLN netto',
          bullet: [
            ['Bezpłatna wycena i konsultacja co do technologii odpowiedniej dla projektu'],
            ['Zawsze rozpoczynamy projekt w najnowszej dostępnej produkcyjnej wersji .NET'],
            ['Aplikacje WPF'],
            ['Aplikacje WinForms'],
            ['Aplikacje MAUI'],
            ['Integracja z bazą danych'],
          ],
        },
        {
          id: 4,
          title: 'Utrzymanie Hostingu',
          price: 'od 300 PLN netto',
          subtitle: 'Kwota miesięczna za aplikację',
          bullet: [
            ['Obsługa i utrzymanie stworzonej przez nas aplikacji lub statycznej strony internetowej w chmurze'],
            ['Możliwość wyboru lokalizacji serwera, Używamy VPS dostarczanych przez firmę Linode'],
            ['Monitorowanie stanu aplikacji'],
            ['Regularne instalowanie łatek systemu'],
          ],
        },
        {
          id: 5,
          title: 'Utrzymanie Aplikacji',
          price: 'od 200 PLN netto',
          subtitle: 'Kwota miesięczna za aplikację',
          bullet: [
            ['Pomoc techniczna w przypadku błędów'],
            ['Wprowadzanie nowych funkcjonalności'],
            ['Zmiany istniejących funkcjonalności'],
            ['Monitorowanie stanu aplikacji'],
            ['Zmiany lub rozszeżenia do aplikacji wyceniane są indywidualnie']
          ],
        },
      ],

    };
  },
}
</script>

<style>
.icon {
  height: 48px;
  margin: 22px 0 0 22px;
}

.icon:hover {
  animation: shake 0.5s;
  animation-iteration-count: infinite;
}

@keyframes shake {
  0% {
    transform: translate(1px, 1px) rotate(0deg);
  }
  10% {
    transform: translate(-1px, -2px) rotate(-1deg);
  }
  20% {
    transform: translate(-3px, 0px) rotate(1deg);
  }
  30% {
    transform: translate(3px, 2px) rotate(0deg);
  }
  40% {
    transform: translate(1px, -1px) rotate(1deg);
  }
  50% {
    transform: translate(-1px, 2px) rotate(-1deg);
  }
  60% {
    transform: translate(-3px, 1px) rotate(0deg);
  }
  70% {
    transform: translate(3px, 1px) rotate(-1deg);
  }
  80% {
    transform: translate(-1px, -1px) rotate(1deg);
  }
  90% {
    transform: translate(1px, 2px) rotate(0deg);
  }
  100% {
    transform: translate(1px, -2px) rotate(-1deg);
  }
}


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