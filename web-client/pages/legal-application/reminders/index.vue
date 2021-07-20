<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="white--text" color="primary">
        <v-toolbar-title>Kalendarz</v-toolbar-title>
        <v-spacer></v-spacer>
        <add-reminder v-on:action-completed="actionDone"/>
      </v-toolbar>
      <v-alert v-if="" elevation="5" text type="info" dismissible close-text="Zamknij">
        Witaj w panelu przypomnień! Używając strzałek przejdziesz do kolejnych miesięcy. Używając guzika "DZISIAJ"
        powrócisz do dziejszej daty.
        Aby zmienić widok kalendarza użyj guzika po prawej stronie z listą dostępnych widoków.
      </v-alert>
      <div class="mt-7">
        <v-sheet tile>

          <v-col>
            <v-row>
              <v-btn icon small color="primary" @click="prev" class="mx-2">
                <v-icon small color="">
                  mdi-chevron-left
                </v-icon>
              </v-btn>
              <v-toolbar-title v-if="$refs.calendar">
                {{ $refs.calendar.title }}
              </v-toolbar-title>
              <v-btn icon small color="primary" @click="next" class="mx-2">
                <v-icon small>
                  mdi-chevron-right
                </v-icon>
              </v-btn>
              <v-spacer></v-spacer>
              <v-select
                v-model="type"
                :items="types"
                item-text="text"
                dense
                outlined
                hide-details
                label="Zmień widok"
              ></v-select>
              <v-spacer></v-spacer>
              <v-btn class="mr-4" color="accent" @click="setToday">
                Dzisiaj
              </v-btn>
            </v-row>
            <v-row>
              <v-expansion-panels flat>
                <v-expansion-panel>
                  <v-expansion-panel-header>Wybierz filtry</v-expansion-panel-header>
                  <v-expansion-panel-content>
                    <v-row>
                      <v-col>
                        <v-select
                          v-model="selectedCategory"
                          :items="items"
                          item-text="text" :item-value="value" return-object
                          label="Wybierz kategorię"
                          dense
                          outlined
                          hide-details
                          class="ma-2"
                        ></v-select>
                      </v-col>
                      <v-col>
                        <v-select
                          v-model="selectedStatus"
                          :items="statuses"
                          item-text="text" :item-value="value" return-object
                          label="Wybierz status"
                          dense
                          outlined
                          hide-details
                          class="ma-2"
                        ></v-select>
                      </v-col>
                      <v-col>
                        <v-btn @click="filterResults()">
                          Filtruj
                        </v-btn>
                      </v-col>
                    </v-row>
                  </v-expansion-panel-content>
                </v-expansion-panel>
              </v-expansion-panels>
            </v-row>
          </v-col>


        </v-sheet>
        <v-sheet height="600">
          <v-calendar ref="calendar" v-model="focus"
                      :events="filteredEvents"
                      :type="type" color="primary"
                      @click:event="showEvent" @click:more="viewDay" @click:date="viewDay"
                      @change="getEvents" :first-interval=7 :interval-minutes=60 :interval-count=12 locale="pl"
                      :weekdays="weekday" event-overlap-mode="stack"></v-calendar>
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
    selectedCategory: {text: 'Wszystkie kategorie', value: 3},
    items: [
      {text: 'Spotkania', value: 0},
      {text: 'Przypomnienia', value: 1},
      {text: 'Zadania', value: 2},
      {text: 'Wszystkie kategorie', value: 3}],
    selectedStatus: {text: 'Wszystkie statusy', value: null},
    statuses: [
      {text: 'Publiczne', value: true},
      {text: 'Prywatne', value: false},
      {text: 'Wszystkie statusy', value: null}],
    type: 'month',
    types: [
      {text: 'Miesiąc', value: 'month'},
      {text: 'Tydzień', value: 'week'},
      {text: 'Dzień', value: 'day'},
    ],
    weekday: [1, 2, 3, 4, 5, 6, 0],
    value: '',
    newEvents: [],
    filteredEvents: [],
    remindersList: [],
    selectedEvent: {},
    selectedElement: null,
    selectedOpen: false,
    checkbox: true,
    events: [],
  }),
  async mounted() {
    try {
      this.$refs.calendar.checkChange();
      await this.getEvents();
      this.filteredEvents = this.newEvents
      console.warn('filtered events', this.filteredEvents)
    } catch (e) {
      console.error('error in fetching event data', e)
    }
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
    filterResults() {
      //All
      if (this.selectedCategory.value === 3 && this.selectedStatus.value === null) {
        this.filteredEvents = this.newEvents
        console.warn('Wszystko', this.filteredEvents)
      }
      if (this.selectedCategory.value === 3 && this.selectedStatus.value === true) {
        this.filteredEvents = this.newEvents.filter((item) => item.public === true)
        console.warn('Wszystko publiczne', this.filteredEvents)
      }
      if (this.selectedCategory.value === 3 && this.selectedStatus.value === false) {
        this.filteredEvents = this.newEvents.filter((item) => item.public === false)
        console.warn('Wszystko prywatne', this.filteredEvents)
      }
      // Meetings
      if (this.selectedCategory.value === 0 && this.selectedStatus.value === false) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 0 && item.public === false)
        console.warn('Spotkania prywatne', this.filteredEvents)
      }
      if (this.selectedCategory.value === 0 && this.selectedStatus.value === true) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 0 && item.public === true)
        console.warn('Spotkania publiczne', this.filteredEvents)
      }
      if (this.selectedCategory.value === 0 && this.selectedStatus.value === null) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 0)
        console.warn('Spotkania wszystkie', this.filteredEvents)
      }
      // Reminders
      if (this.selectedCategory.value === 1 && this.selectedStatus.value === false) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 1 && item.public === false)
        console.warn('Przypomnienia prywatne', this.filteredEvents)
      }
      if (this.selectedCategory.value === 1 && this.selectedStatus.value === true) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 1 && item.public === true)
        console.warn('Przypomnienia publiczne', this.filteredEvents)
      }
      if (this.selectedCategory.value === 1 && this.selectedStatus.value === null) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 1)
        console.warn('Przypomnienia wszystkie', this.filteredEvents)
      }
      // Tasks
      if (this.selectedCategory.value === 2 && this.selectedStatus.value === false) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 2 && item.public === false)
        console.warn('Zadania prywatne', this.filteredEvents)
      }
      if (this.selectedCategory.value === 2 && this.selectedStatus.value === true) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 2 && item.public === true)
        console.warn('Zadania publiczne', this.filteredEvents)
      }
      if (this.selectedCategory.value === 2 && this.selectedStatus.value === null) {
        this.filteredEvents = this.newEvents.filter((item) => item.category === 2)
        console.warn('Zadania wszystkie', this.filteredEvents)
      }
    },

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
        // console.log('selected event', this.selectedEvent);
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
        // console.warn('reminders', this.remindersList);
        let newEvents = [];
        this.remindersList.forEach(x => {
          newEvents.push({
            name: x.name,
            details: x.message,
            start: this.eventStartDate(x),
            end: this.eventEndDate(x),
            color: this.setColor(x),
            id: x.id,
            public: x.public,
            category: x.reminderCategory,
            timed: x.allDayEvent

          });
        });
        console.warn('nowe eventy', newEvents);
        this.newEvents = newEvents;
        // console.warn('calendar start', this.$refs.calendar.)
        // this.filteredEvents = newEvents

      } catch (e) {
        console.error('calendar fetch error', e)
      }

    },

    setColor(item) {
      if (item.reminderCategory === 0) {
        return "blue"
      }
      if (item.reminderCategory === 1) {
        return "orange lighten-1"
      }
      if (item.reminderCategory === 2) {
        return "green"
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
    async actionDone() {
      try {
        await this.getEvents();
        this.filteredEvents = this.newEvents
        this.selectedOpen = false;
      } catch (e) {
        console.error('error in refreshing data', e)
      }

    },
  }

};
</script>

<style scoped>

</style>
