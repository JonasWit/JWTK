<template>
  <v-container fluid>
    <section>
      <div class="content white--text text-center d-flex align-center">
        <div>
          <h1>Zapraszamy do współpracy</h1>
          <h5>
            <span>Systemy Wspomagania Pracy</span> to rozwiązania dostosowane do potrzeb Twojej firmy.</h5>
          <h5> Tworzymy strony internetowe i aplikacje, które wspierają funkcjonowanie biznesu. </h5>
        </div>
      </div>
    </section>
    <v-row class="d-flex justify-space-between my-5">
      <v-card elevation="0" id="card" v-for="item in items" :key="item.id" max-width="350px" class="mx-auto my-2">
        <v-row align="center" justify="space-around" class="my-2">
          <img :src="item.icon">
        </v-row>
        <v-card-title class="text-uppercase justify-center">{{ item.title }}</v-card-title>
        <v-card-text>{{ item.text }}</v-card-text>
      </v-card>
    </v-row>
    <section class="section-2">
      <div class="content white--text text-center">
        <h1>Technologie</h1>
        <h5>
          Więcej informacji na temat technologii, które wykorzystujemy, możesz uzyskać na oficjlanych stronach
          podlinkowanych
          poniżej. </h5>
        <v-row class="d-flex justify-space-between">
          <v-card elevation="0" class="icon-card mx-auto" v-for="item in icons" :key="item.id">
            <a :href="item.url" target="_blank"><img class="icon" :src="item.img"/></a>
          </v-card>
        </v-row>
      </div>
    </section>
  </v-container>
</template>
<script>

export default {
  name: "home-main-content",
  transition: 'slide-bottom',
  data: () => {
    return ({
      items: [
        {
          id: 1,
          img: `${require('assets/images/background.jpg')}`,
          icon: `${require('assets/images/icons8-security-configuration-100.png')}`,
          title: 'Bezpieczeństwo',
          text: 'Kładziemy nacisk na bezpieczeńswto naszych aplikacji. Korzystamy z najnowszych rozwiązań w zakresie bezpieczeństwa.'
        },
        {
          id: 2,
          img: `${require('assets/images/background.jpg')}`,
          icon: `${require('assets/images/icons8-innovation-100.png')}`,
          title: 'Innowacyjność',
          text: 'Zawsze rozpoczynamy projekt w najnowszej dostępnej wersji danej technologii. Oferujemy również późniejsze utrzymanie i akutalizaję aplikacji.\n' +
            '\n'
        },
        {
          id: 3,
          img: `${require('assets/images/background.jpg')}`,
          icon: `${require('assets/images/icons8-short-hair-girl-question-mark-100.png')}`,
          title: 'Elastyczność',
          text: 'Jesteśmy otwarci na wszelkie zmiany i dodatki do aplikacji. Zarówno oferowanych już na portalu jak i stworonych na zamówienie. Oferujemy zawsze bezpłatną konsultację i wycenę każdej zmiany lub rozszeżenia.'
        },


      ],
      icons: [
        {
          id: 1,
          url: 'https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor',
          img: `${require('~/assets/images/Blazor2.png')}`,

        },
        {
          id: 2,
          url: 'https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet',
          img: `${require('~/assets/images/Net2.png')}`,

        },
        {
          id: 3,
          url: 'https://getbootstrap.com/',
          img: `${require('~/assets/images/icons8-bootstrap-48.png')}`,

        },
        {
          id: 4,
          url: 'https://vuejs.org/',
          img: `${require('~/assets/images/icons8-vue-js-48.png')}`,

        },
        {
          id: 5,
          url: 'https://vuetifyjs.com/en/',
          img: `${require('~/assets/images/vutify.png')}`,

        },
        {
          id: 6,
          url: 'https://wordpress.com/',
          img: `${require('~/assets/images/icons8-wordpress-48.png')}`,
        },
        {
          id: 7,
          url: 'https://www.postgresql.org/',
          img: `${require('~/assets/images/icons8-postgresql-48.png')}`,
        },
      ]

    });
  },
};
</script>

<style scoped>
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

.icon-card {
  background: transparent !important;
  padding: 10px;
}

h1 {
  font-size: 65px;
  font-weight: 800 !important;
  line-height: 60px;
  padding: 10px 0;
  letter-spacing: -.03em !important;
}

h5 {
  font-size: 25px;
  font-weight: 400 !important;
  padding: 10px 0;
  margin-left: 10px;
}

@media only screen and (max-width: 780px) {
  .content h1 {
    font-size: 30px;
    line-height: 30px;
  }

  .content h5 {
    font-size: 15px;
    padding: 2px 0px;
  }
}

#card {
  background: transparent;
}

section {
  position: relative;
  width: 100%;
  height: 47vh;
  display: flex;
  justify-content: center;
  overflow: hidden;
}

section .content {
  position: relative;
  z-index: 1;
  text-align: left;
  margin-top: 20px;

}

.section-2 {
  justify-content: center;
  position: relative;
  width: 100%;
  height: 100%;
  display: flex;
  overflow: hidden;
}

.section-2 .content {
  margin-bottom: 25px;
}

section:before {
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

.section-2:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: linear-gradient(45deg, cornflowerblue, #000000);
  border-radius: 0 !important;
  transform: scaleX(1.5);
}

</style>
