<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="white--text" color="primary">
        <v-toolbar-title>Przypomnienia</v-toolbar-title>
        <v-spacer></v-spacer>
        <add-reminder v-on:action-completed="actionDone"/>
      </v-toolbar>
      <v-alert v-if="" elevation="5" text type="info" dismissible close-text="Zamknij">
        Witaj w panelu przypomnień! Używając strzałek przejdziesz do kolejnych miesięcy. Używając guzika "DZISIAJ"
        powrócisz do dziejszej daty.
        Aby zmienić widok kalendarza użyj guzika po prawej stronie z listą dostępnych widoków.
      </v-alert>
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
        <v-sheet height="600">
          <v-calendar ref="calendar" v-model="focus" :events="newEvents" :type="type" color="primary"
                      @click:event="showEvent" @click:more="viewDay" @click:date="viewDay"
                      @change="getEvents" :first-interval=7 :interval-minutes=60 :interval-count=12 locale="pl"
                      :weekdays="weekday" event-overlap-mode="stack"
          ></v-calendar>
          <v-menu v-model="selectedOpen" :close-on-content-click="false" :activator="selectedElement" offset-x>
            <v-card color="grey lighten-4" min-width="500px" max-width="800px" flat light>
              <v-toolbar :color="selectedEvent.color" dark>
                <edit-reminder :event-for-action="selectedEvent" v-on:action-completed="actionDone"/>
                <v-toolbar-title v-html="selectedEvent.name"></v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-card-subtitle>Opis: {{ selectedEvent.details }}</v-card-subtitle>
                <v-card-subtitle>Data początkowa: {{ selectedEvent.start }}</v-card-subtitle>
                <v-card-subtitle>Data końcowa: {{ selectedEvent.end }}</v-card-subtitle>
                <v-card-subtitle>Kategoria: {{ categoryToDisplay }}</v-card-subtitle>
                <v-checkbox v-model="checkbox" label="Status publiczny"
                            :value="selectedEvent.public" disabled></v-checkbox>
              </v-card-text>
              <v-card-actions>
                <v-btn text color="secondary" @click="selectedOpen = false">
                  Anuluj
                </v-btn>
                <v-spacer></v-spacer>
                <delete-reminder :event-for-action="selectedEvent" v-on:delete-completed="actionDone"/>
              </v-card-actions>
            </v-card>
          </v-menu>
        </v-sheet>
      </div>
    </template>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import AddReminder from "@/components/legal-app/reminders/add-reminder";
import DeleteReminder from "@/components/legal-app/reminders/delete-reminder";
import EditReminder from "@/components/legal-app/reminders/edit-reminder";
import {formatDateToLocaleTimeZone, formatDateToLocaleTimeZoneWithoutTime} from "@/data/date-extensions";

export default {
  name: "index",
  components: {EditReminder, DeleteReminder, AddReminder, Layout},
  data: () => ({
    focus: '',
    type: 'month',
    types: ['month', 'week', 'day'],
    weekday: [1, 2, 3, 4, 5, 6, 0],
    value: '',
    typeToLabel: {
      month: 'Miesiąc',
      week: 'Tydzień',
      day: 'Dzień',
    },
    newEvents: [],
    remindersList: [],
    selectedEvent: {},
    selectedElement: null,
    selectedOpen: false,
    checkbox: true,
    colors: ['blue', '#ffaa2d', 'cyan'],
    events: [],
  }),
  mounted() {
    this.$refs.calendar.checkChange();
  },
  computed: {
    categoryToDisplay() {
      if (this.selectedEvent.category === 0) {
        return "Spotkanie"
      }
      if (this.selectedEvent.category === 1) {
        return "Przypomnienie"
      }
      if (this.selectedEvent.category === 2) {
        return "Zadanie"
      }

    }
  },
  methods: {


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
        console.log('selected event', this.selectedEvent);
        this.selectedElement = nativeEvent.target;
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
    async getEvents() {
      try {
        this.remindersList = await this.$axios.$get(`/api/legal-app-reminders/list`);
        console.warn('reminders', this.remindersList);
        let newEvents = [];
        this.remindersList.forEach(x => {
          newEvents.push({
            name: x.name,
            details: x.message,
            start: this.eventStartDate(x),
            end: this.eventEndDate(x),
            color: this.colors[this.rnd(0, this.colors.length - 1)],
            id: x.id,
            public: x.public,
            category: x.reminderCategory,
            timed: x.allDayEvent

          });
        });
        console.warn('nowe eventy', newEvents);
        this.newEvents = newEvents;
      } catch (e) {
        console.error('calendar fetch error', e)
      }

    },
    eventStartDate(item) {
      if (item.allDayEvent) {
        const date = new Date(item.start)
        const isoDateTime = new Date(date.getTime() - (date.getTimezoneOffset() * 60000))
        return formatDateToLocaleTimeZoneWithoutTime(isoDateTime)
      } else {
        const date = new Date(item.start)
        const isoDateTime = new Date(date.getTime() - (date.getTimezoneOffset() * 60000))
        return formatDateToLocaleTimeZone(isoDateTime)

      }
    },
    eventEndDate(item) {
      if (item.allDayEvent) {
        const date = new Date(item.end)
        const isoDateTime = new Date(date.getTime() - (date.getTimezoneOffset() * 60000))
        return formatDateToLocaleTimeZoneWithoutTime(isoDateTime)
      } else {
        const date = new Date(item.end)
        const isoDateTime = new Date(date.getTime() - (date.getTimezoneOffset() * 60000))
        return formatDateToLocaleTimeZone(isoDateTime)
      }
    },
    rnd(a, b) {
      return Math.floor((b - a + 1) * Math.random()) + a;
    },
    actionDone() {
      this.getEvents();
      this.selectedOpen = false;
    },
  }

};
</script>

<style scoped>

</style>
