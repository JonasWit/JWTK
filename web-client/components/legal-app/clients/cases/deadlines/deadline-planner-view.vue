<template>
  <div>
    <v-sheet tile height="64" class="d-flex">
      <v-toolbar flat>
        <v-btn class="mr-4" color="accent" @click="setToday">
          Dzisiaj
        </v-btn>
        <v-btn fab text small color="grey darken-2" @click="prev">
          <v-icon small>
            mdi-chevron-left
          </v-icon>
        </v-btn>
        <v-btn fab text small color="grey darken-2" @click="next">
          <v-icon small>
            mdi-chevron-right
          </v-icon>
        </v-btn>
        <v-toolbar-title v-if="$refs.calendar">
          {{ $refs.calendar.title }}
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-menu bottom right>
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="accent" v-bind="attrs" v-on="on">
              <span>{{ typeToLabel[type] }}</span>
              <v-icon right>
                mdi-menu-down
              </v-icon>
            </v-btn>
          </template>
          <v-list>
            <v-list-item @click="type = 'day'">
              <v-list-item-title>Dzień</v-list-item-title>
            </v-list-item>
            <v-list-item @click="type = 'week'">
              <v-list-item-title>Tydzień</v-list-item-title>
            </v-list-item>
            <v-list-item @click="type = 'month'">
              <v-list-item-title>Miesiąc</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
      </v-toolbar>
    </v-sheet>
    <v-alert v-if="deadlines.length > 2" elevation="5" text type="info" dismissible close-text="Zamknij">
      Używając strzałek przejdziesz do kolejnych miesięcy. Używając guzika "DZISIAJ" powrócisz do dziejszej daty. Aby
      zmienić widok kalendarza użyj guzika po prawej stronie z
      listą dostępnych widoków.
    </v-alert>
    <v-sheet height="600">
      <v-calendar v-model="focus" locale="pl" ref="calendar" :weekdays="weekday" :type="type" :events="newEvents"
                  :event-overlap-mode="mode" :event-overlap-threshold="30" @change="getEvents" @click:more="viewDay"
                  @click:date="viewDay" @click:event="showEvent"></v-calendar>
      <v-menu v-model="selectedOpen" :close-on-content-click="false" :activator="selectedEvent">
        <v-card color="grey lighten-4" min-width="500px" max-width="800px" flat light>
          <v-toolbar :color="selectedEvent.color" dark>
            <v-toolbar-title v-html="selectedEvent.name"></v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-card-subtitle>Sprawa: {{ selectedEvent.details }}</v-card-subtitle>
            <v-card-subtitle>Termin: {{ selectedEvent.start }}</v-card-subtitle>
            <v-card-subtitle>Sygnatura: {{ selectedEvent.signature }}</v-card-subtitle>
            <v-alert dense elevation="5" text type="info">Zarządzanie terminami dla spraw nie jest możliwe z poziomu
              kalendarza. Kalendarz stanowi
              jedynie podgląd zbliżających się terminów. Proszę wejść w zakładkę 'LISTA TERMINÓW', aby dokonać zmian.
            </v-alert>
          </v-card-text>
          <v-card-actions>
            <v-btn text color="secondary" @click="selectedOpen = false">
              Anuluj
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-menu>


    </v-sheet>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import {handleError} from "@/data/functions";

export default {
  name: "deadline-planner-view",
  data: () => ({
    focus: '',
    type: 'month',
    types: ['month', 'week', 'day'],
    mode: 'stack',
    weekday: [1, 2, 3, 4, 5, 6, 0],
    weekdays: [
      {text: 'Nd - Sob', value: [0, 1, 2, 3, 4, 5, 6]},
      {text: 'Pon - Nd', value: [1, 2, 3, 4, 5, 6, 0]},
      {text: 'Pon - Pt', value: [1, 2, 3, 4, 5]},
      {text: 'Pon, Śr, Pt', value: [1, 3, 5]},
    ],
    value: '',
    typeToLabel: {
      month: 'Miesiąc',
      week: 'Tydzień',
      day: 'Dzień',
    },
    newEvents: [],
    selectedEvent: {},
    selectedElement: null,
    selectedOpen: false,
  }),

  computed: {
    ...mapState('legal-app-client-store', ['deadlines']),
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDeadlines']),
    viewDay({date}) {
      this.focus = date;
      this.type = 'day';
    },
    setToday() {
      this.focus = '';
    },
    prev() {
      this.$refs.calendar.prev();
    },
    next() {
      this.$refs.calendar.next();
    },
    showEvent({nativeEvent, event}) {
      const open = () => {
        this.selectedEvent = event;
        console.log('selected event', event);
        requestAnimationFrame(() => requestAnimationFrame(() => this.selectedOpen = true));
      };
      if (this.selectedOpen) {
        this.selectedOpen = false;
        requestAnimationFrame(() => requestAnimationFrame(() => open()));
      } else {
        open();
      }
      nativeEvent.stopPropagation();
    },
    eventDate(item) {
      return new Date(item.deadline).toISOString().substr(0, 10);
    },
    async getEvents() {
      try {
        let newEvents = [];
        this.deadlines.forEach(x => {
          newEvents.push({
            details: x.case.name,
            signature: x.case.signature,
            name: x.message,
            start: this.eventDate(x),
            end: this.eventDate(x),
            color: 'error',
            id: x.id,
            timed: false
          });
        });
        this.newEvents = newEvents;
      } catch (error) {
        handleError(error);
      }
    },
  },
};
</script>

<style scoped>

</style>
