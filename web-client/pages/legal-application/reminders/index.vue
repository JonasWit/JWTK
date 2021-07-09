<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="white--text" color="primary">
        <v-toolbar-title>Przypomnienia</v-toolbar-title>
        <v-spacer></v-spacer>
        <add-reminder/>
      </v-toolbar>
      <v-alert v-if="" elevation="5" text type="info" dismissible close-text="Zamknij">
        Witaj w panelu przypomnień!
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
        <v-alert v-if="" elevation="5" text type="info" dismissible close-text="Zamknij">
          Używając strzałek przejdziesz do kolejnych miesięcy. Używając guzika "DZISIAJ" powrócisz do dziejszej daty.
          Aby zmienić widok kalendarza użyj guzika po prawej stronie z listą dostępnych widoków.
        </v-alert>
        <v-sheet height="600">
          <v-calendar ref="calendar" v-model="focus" color="primary" :events="newEvents" :type="type"
                      @click:event="showEvent" @click:more="viewDay" @click:date="viewDay"
                      @change="getEvents"></v-calendar>
          <v-menu v-model="selectedOpen" :close-on-content-click="false" :activator="selectedElement" offset-x>
            <v-card color="grey lighten-4" max-width="800px" flat>
              <v-toolbar :color="selectedEvent.color" dark>
                <v-btn icon>
                  <v-icon>mdi-pencil</v-icon>
                </v-btn>
                <v-toolbar-title v-html="selectedEvent.name"></v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <span v-html="selectedEvent.details"></span>
                <v-checkbox v-model="checkbox" :label="labelCondition(selectedEvent.public)"
                            :value="selectedEvent.public" value disabled></v-checkbox>
              </v-card-text>
              <v-card-actions>
                <v-btn text color="secondary" @click="selectedOpen = false">
                  Anuluj
                </v-btn>
                <v-spacer></v-spacer>
                <delete-reminder :reminder-for-action="selectedEvent" v-on:delete-completed="actionDone"/>
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

export default {
  name: "index",
  components: {DeleteReminder, AddReminder, Layout},
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
    remindersList: [],
    selectedEvent: {},
    selectedElement: null,
    selectedOpen: false,
    checkbox: true,
    colors: ['blue', 'deep-purple', 'cyan'],
  }),
  mounted() {
    this.$refs.calendar.checkChange();
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
            start: new Date(x.start).toISOString().substr(0, 10),
            end: new Date(x.end).toISOString().substr(0, 10),
            color: this.colors[this.rnd(0, this.colors.length - 1)],
            id: x.id,
            public: x.public,
            category: x.reminderCategory
          });
        });
        console.warn('nowe eventy', newEvents);
        this.newEvents = newEvents;

      } catch (e) {

      }

    },
    rnd(a, b) {
      return Math.floor((b - a + 1) * Math.random()) + a;
    },
    actionDone() {
      this.getEvents();
      this.selectedOpen = false;
    },
    labelCondition(val) {
      if (val) {
        return "Publiczne";
      }
      return "Prywatne";
    }


  }

};
</script>

<style scoped>

</style>
