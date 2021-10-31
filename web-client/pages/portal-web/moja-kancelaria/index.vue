<template>
  <v-container fluid>
    <section>
      <div class="content text-center white--text d-flex align-center">
        <div>
          <h1>Moja Kancelaria</h1>
          <h5>
            W tym miejscu możesz zapoznać się z najważniejszymi komponentami aplikacji Moja Kancelaria</h5>
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
      <v-container class="mb-9">
        <v-row class="mt-6 d-flex justify-space-around">
          <v-card v-for="item in videos" :key="item.id" class="mt-6 mx-4 pa-3" width="460">
            <v-card-title>{{ item.title }}</v-card-title>
            <v-card-text class="mb-0">{{ item.description }}</v-card-text>
            <video :src="item.video" width="420" height="340" controls></video>
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
        title: 'Tworzenie bazy klientów',
        description: 'Dowiedz się jak dodać klientów do bazy danych Moja Kancelaria. Z tego filmu dowiesz się również jak edytować nazwę klienta, w jaki sposób skorzystać z funkcji wyszukiwania oraz jak usunąć i zarchiwizować dane wybranego klienta.'
      },
      {
        id: 2,
        video: `${require('~/assets/videos/client-contact.mp4')}`,
        title: 'Lista kontaktów',
        description: 'W tym miejscu dowiesz się jak utworzyć listę kontaktów dla wybranego klienta. Film prezentuje jak utworzyć, edytować lub usunąć kontakty.'
      },
      {
        id: 3,
        video: `${require('~/assets/videos/client-notes.mp4')}`,
        title: 'Notes klienta',
        description: 'Notes to komponent, umożliwiający zarządzanie notatkami dla wybranego klienta. W tym miejscu możesz dodawać notatki ze spotkań lub inne dotyczące klienta. Możesz również zdecydować czy notatka ma być widoczna dla innych użytkowników - notatka publiczna - lub tylko dla ciebie - notatka prywatna. '
      },
      {
        id: 4,
        video: `${require('~/assets/videos/client-work-record.mp4')}`,
        title: 'Rozliczenia',
        description: 'Sekcja Rozliczenia umożliwia monitorowanie oraz raportowanie czasu pracy dla danego klienta. W tym miejscu możesz dodawać czas spędzony na pracy dla klienta, jak również utworzyć raport zawierający wybrane rozliczenia. '
      },
      {
        id: 5,
        video: `${require('~/assets/videos/work-record-report-preview.mp4')}`,
        title: 'Rozliczenia - podgląd raportu',
        description: 'Z tego filmu dowiesz się jak sprawdzić podgląd rozliczenia przed wygenerowaniem go w formie pdf.'
      },
      {
        id: 6,
        video: `${require('~/assets/videos/client-generate-report.mp4')}`,
        title: 'Rozliczenia -  generowanie raportu',
        description: 'Raport rozliczeń może zostać wygenrowany w formie pdf lub wydrukowany.'
      },
      {
        id: 7,
        video: `${require('~/assets/videos/case-section.mp4')}`,
        title: 'Zarządzanie sprawami',
        description: 'Każdy dodany klient może mieć swoje przypisane sprawy. Dowiedz się jak dodawać sprawy dla wybranego klienta i jak wygląda panel zarządzania sprawą.'
      },
      {
        id: 8,
        video: `${require('~/assets/videos/case-notes.mp4')}`,
        title: 'Notes sprawy',
        description: 'Każda sprawa ma utworzony notes. Możesz w nim zarządzać notatkami dla wybranej sprawy.'
      },
      {
        id: 9,
        video: `${require('~/assets/videos/case-deadline.mp4')}`,
        title: 'Terminy dla sprawy',
        description: 'W tym miejscu zobaczysz w jaki sposób tworzyć terminy dla wtbranej sprawy i jak nimi zarządzać. Ten komponent pomoże kontrolować ważne terminy, które automatycznie zostaną dodane do głównego kalendarza. Nadchodzące terminy - przypadające na dzisiaj i jutro - będą widoczne pod ikonką dzwonka w prawym dolnym rogu. Czerwona ikonka oznacza nadchodzące terminy lub wydarzenia.'
      },
      {
        id: 10,
        video: `${require('~/assets/videos/case-archive.mp4')}`,
        title: 'Archiwum spraw',
        description: 'Archiwum spraw to miejsce, do którego możesz przenieść nieaktywne sprawy bez konieczności ich całkowitego usuwania z bazy danych klienta. Sprawa może zostać przywróca do statusu aktywnej w każdej chwili. '
      },
      {
        id: 11,
        video: `${require('~/assets/videos/case-accesses.mp4')}`,
        title: 'Dostępy do sprawy',
        description: 'W tym miejscu dowiesz się w jaki sposób zarządzać dostępem do sprawy. Możesz zdecydować, którzy użytkownicy będą mieć uprawnienia do zarządzania wybraną sprawą.'
      },
      {
        id: 12,
        video: `${require('~/assets/videos/client-archive.mp4')}`,
        title: 'Archiwum klientów',
        description: 'Do archiwum możesz dodać klienta i wszystkie jego dane bez konieczności usuwania go z bazy danych. Odzyskanie dostępu będzie możliwe w każdej chwili.'
      },
      {
        id: 13,
        video: `${require('~/assets/videos/calendar.mp4')}`,
        title: 'Kalendarz',
        description: 'Kalendarz umożliwa zarządzanie wydarzeniami takimi jak spotkania, zadania lub przypomnienia. Nadchodzące wydarzenia - przypadające na dzisiaj i jutro - będą widoczne pod ikonką dzwonka w prawym dolnym rogu. Czerwona ikonka oznacza nadchodzące terminy lub wydarzenia. '
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
