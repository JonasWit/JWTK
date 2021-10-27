<template>
  <v-container fluid>
    <section>
      <div class="content text-center white--text d-flex align-center">
        <div>
          <h1>Twoja Kancelaria</h1>
          <h5>
            W tym miejscu możesz zapoznać się z najważniejszymi komponentami aplikacji Twoja Kancelaria.</h5>
        </div>
      </div>
    </section>
    <v-row class="d-flex justify-space-between my-4">
      <v-card elevation="0" id="card" v-for="item in items" :key="item.id" width="350px" class="mx-auto my-2">
        <v-row align="center" justify="space-around">
          <img class="icon" :src="item.icon">
        </v-row>
        <v-card-title class="text-uppercase justify-center">{{ item.title }}</v-card-title>
        <v-card-text>{{ item.text }}</v-card-text>
      </v-card>
    </v-row>
    <section class="section-2">
      <v-container>
        <v-row class="mt-6 d-flex justify-space-around">
          <v-card v-for="item in videos" :key="item.id" class="mt-6 mx-4 pa-3">
            <v-card-title>{{ item.title }}</v-card-title>
            <v-card-subtitle>Tekst subtitle</v-card-subtitle>
            <video :src="item.video" width="420" height="340" controls></video>
            <v-card-text>{{ item.description }}</v-card-text>
          </v-card>
        </v-row>
      </v-container>
    </section>

  </v-container>
</template>
<script>
import Layout from "@/components/legal-app/layout";

export default {
  name: "index",
  components: {Layout},
  data: () => ({
    items: [
      {
        id: 1,
        icon: `${require('~/assets/images/icons8-video-playlist-100.png')}`,
        title: 'Wybierz filmik',
        text: 'Filmy prezentuą najważniejsze komponenety aplikacji. '

      },

      {
        id: 2,
        icon: `${require('~/assets/images/icons8-create-100.png')}`,
        title: 'Zapoznaj się z opisem',
        text: 'Zapoznaj się z opisem wybranej funkcjonalności, aby zrozumieć do czego służy.'
      },


      {
        id: 3,
        icon: `${require('~/assets/images/icons8-email-100.png')}`,
        title: 'Skontaktuj się z nami',
        text: 'Jeśli zainteresowała Cię nasza aplikacja, skontaktuj się znami!'
      },
    ],
    videos: [
      {
        id: 1,
        video: `${require('~/assets/videos/client-section.mp4')}`,
        title: 'Klient',
        description: 'Sekcja klient opis'
      },
      {
        id: 2,
        video: `${require('~/assets/videos/client-contact.mp4')}`,
        title: 'Klient-kontakt',
        description: 'Sekcja klient-kontakt opis'
      },
      {
        id: 3,
        video: `${require('~/assets/videos/client-notes.mp4')}`,
        title: 'Klient-notes',
        description: 'Sekcja klient-notes opis'
      },
      {
        id: 4,
        video: `${require('~/assets/videos/client-work-record.mp4')}`,
        title: 'Klient-notes',
        description: 'Sekcja klient-notes opis'
      },
      {
        id: 5,
        video: `${require('~/assets/videos/work-record-report-preview.mp4')}`,
        title: 'Work record report preview',
        description: 'Sekcja klient-notes opis'
      },
      {
        id: 6,
        video: `${require('~/assets/videos/client-generate-report.mp4')}`,
        title: 'Generate report',
        description: 'Sekcja generate report'
      },
      {
        id: 7,
        video: `${require('~/assets/videos/case-section.mp4')}`,
        title: 'Case',
        description: 'Sekcja -opis'
      },
      {
        id: 8,
        video: `${require('~/assets/videos/case-notes.mp4')}`,
        title: 'Case - notes',
        description: 'Sekcja -opis'
      },
      {
        id: 9,
        video: `${require('~/assets/videos/case-deadline.mp4')}`,
        title: 'Case - deadline',
        description: 'Sekcja -opis'
      },
      {
        id: 10,
        video: `${require('~/assets/videos/case-archive.mp4')}`,
        title: 'Case - archive',
        description: 'Sekcja -opis'
      },
      {
        id: 11,
        video: `${require('~/assets/videos/case-accesses.mp4')}`,
        title: 'Case - accesses',
        description: 'Sekcja -opis'
      },
      {
        id: 12,
        video: `${require('~/assets/videos/client-archive.mp4')}`,
        title: 'Client - archive',
        description: 'Sekcja -opis'
      },
      {
        id: 13,
        video: `${require('~/assets/videos/calendar.mp4')}`,
        title: 'Calendar',
        description: 'Sekcja -calendar'
      }

    ]
  })
};
</script>

<style scoped>

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
  margin-top: 25px;

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

h1 {
  font-size: 65px;
  font-weight: 800 !important;
  line-height: 60px;
  padding: 10px 0px;
  letter-spacing: -.03em !important;
}

h5 {
  font-size: 25px;
  font-weight: 400 !important;
  padding: 10px 0px;

}

#card {
  background: transparent !important;
}

.section-2 {
  width: 100%;
  height: 100%;

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
