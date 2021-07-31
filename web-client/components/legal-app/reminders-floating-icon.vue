<template>
  <v-speed-dial v-model="fab" fixed bottom right class="mr-8 mb-8" transition="slide-x-transition">
    <template v-slot:activator>
      <v-btn v-model="fab" color="error" dark fab>
        <v-icon v-if="fab">
          mdi-close
        </v-icon>
        <v-icon v-else class="reminder-bell">
          mdi-bell
        </v-icon>
        <v-badge
          color="primary"
          bottom
          :content="notifications"
          :value="notifications"
        >
        </v-badge>
      </v-btn>
    </template>
    <v-row class="mt-16 direction d-flex">
      <v-col sm="6">
        <v-card>
          <v-toolbar color="error" class="white--text">
            <v-toolbar-title>Masz zbliżające się terminy</v-toolbar-title>
          </v-toolbar>
          <v-virtual-scroll :items="deadlinesList" :item-height="50" height="300">
            <template v-slot:default="{ item }">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title>{{ item.message }}</v-list-item-title>
                  <v-list-item-subtitle>{{ formatDateForCalendar(item.deadline) }}</v-list-item-subtitle>
                </v-list-item-content>
                <v-list-item-action>
                  <nuxt-link to="/legal-application/reminders">
                    <v-btn depressed small>
                      Sprawdź
                      <v-icon color="red darken-4" right>
                        mdi-open-in-new
                      </v-icon>
                    </v-btn>
                  </nuxt-link>
                </v-list-item-action>
              </v-list-item>
            </template>
          </v-virtual-scroll>
        </v-card>
      </v-col>
      <v-col sm="6">
        <v-card>
          <v-toolbar color="warning" class="white--text">
            <v-toolbar-title>Plany na dzisiaj i jutro</v-toolbar-title>
          </v-toolbar>
          <v-virtual-scroll :items="eventsList" :item-height="50" height="300">
            <template v-slot:default="{ item }">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title>{{ item.name }}</v-list-item-title>
                  <v-list-item-subtitle>{{ categoryToDisplay(item) }}</v-list-item-subtitle>
                </v-list-item-content>
                <v-list-item-action>
                  <nuxt-link to="/legal-application/reminders">
                    <v-btn depressed small>
                      Sprawdź
                      <v-icon color="red darken-4" right>
                        mdi-open-in-new
                      </v-icon>
                    </v-btn>
                  </nuxt-link>
                </v-list-item-action>
              </v-list-item>
            </template>
          </v-virtual-scroll>
        </v-card>
      </v-col>
    </v-row>
  </v-speed-dial>
</template>

<script>
import {
  formatDate,
  queryDateForFloatingBell,
  todayDate
} from "@/data/date-extensions";
import {handleError} from "@/data/functions";

export default {
  name: "reminders-floating-icon",
  data: () => ({
    direction: 'top',
    fab: false,
    fling: false,
    hover: false,
    tabs: null,
    top: true,
    right: true,
    bottom: false,
    left: false,
    transition: 'scale',
    deadlinesList: [],
    eventsList: [],
    notifications: 0,
  }),
  async fetch() {
    await this.getComingDeadlines()
    await this.getEventsList()
    await this.countNotifications()
  },
  computed: {
    todayDate() {
      return todayDate()
    },
    query() {
      return queryDateForFloatingBell(this.todayDate)
    },

  },

  methods: {
    async getComingDeadlines() {
      try {
        this.deadlinesList = await this.$axios.$get(`/api/legal-app-cases/deadlines/list-all${this.query}`)
        console.log('upcoming deadlines', this.deadlinesList)
      } catch (error) {
        handleError(error)
      }

    },
    async getEventsList() {
      try {
        this.eventsList = await this.$axios.$get(`/api/legal-app-reminders/list/limit${this.query}`)
        console.log('events all', this.eventsList)
      } catch (error) {
        handleError(error)
      }
    },
    categoryToDisplay(item) {
      if (item.reminderCategory === 0) {
        return "Spotkanie"
      }
      if (item.reminderCategory === 1) {
        return "Przypomnienie"
      }
      if (item.reminderCategory === 2) {
        return "Zadanie"
      }
    },

    formatDateForCalendar(date) {
      return formatDate(date)
    },

    async countNotifications() {
      let eventsCount = this.eventsList.length
      let deadlinesCount = this.deadlinesList.length
      this.notifications = eventsCount + deadlinesCount
    }


  }
}

</script>

<style scoped>

.direction {
  position: absolute;
  right: 10%;
  bottom: 15%;
  width: 800px;
}

.reminder-bell {
  -webkit-animation: ring 4s .7s ease-in-out infinite;
  -webkit-transform-origin: 50% 4px;
  -moz-animation: ring 4s .7s ease-in-out infinite;
  -moz-transform-origin: 50% 4px;
  animation: ring 4s .7s ease-in-out infinite;
  transform-origin: 50% 4px;
}

@-webkit-keyframes ring {
  0% {
    -webkit-transform: rotateZ(0);
  }
  1% {
    -webkit-transform: rotateZ(30deg);
  }
  3% {
    -webkit-transform: rotateZ(-28deg);
  }
  5% {
    -webkit-transform: rotateZ(34deg);
  }
  7% {
    -webkit-transform: rotateZ(-32deg);
  }
  9% {
    -webkit-transform: rotateZ(30deg);
  }
  11% {
    -webkit-transform: rotateZ(-28deg);
  }
  13% {
    -webkit-transform: rotateZ(26deg);
  }
  15% {
    -webkit-transform: rotateZ(-24deg);
  }
  17% {
    -webkit-transform: rotateZ(22deg);
  }
  19% {
    -webkit-transform: rotateZ(-20deg);
  }
  21% {
    -webkit-transform: rotateZ(18deg);
  }
  23% {
    -webkit-transform: rotateZ(-16deg);
  }
  25% {
    -webkit-transform: rotateZ(14deg);
  }
  27% {
    -webkit-transform: rotateZ(-12deg);
  }
  29% {
    -webkit-transform: rotateZ(10deg);
  }
  31% {
    -webkit-transform: rotateZ(-8deg);
  }
  33% {
    -webkit-transform: rotateZ(6deg);
  }
  35% {
    -webkit-transform: rotateZ(-4deg);
  }
  37% {
    -webkit-transform: rotateZ(2deg);
  }
  39% {
    -webkit-transform: rotateZ(-1deg);
  }
  41% {
    -webkit-transform: rotateZ(1deg);
  }

  43% {
    -webkit-transform: rotateZ(0);
  }
  100% {
    -webkit-transform: rotateZ(0);
  }
}

@-moz-keyframes ring {
  0% {
    -moz-transform: rotate(0);
  }
  1% {
    -moz-transform: rotate(30deg);
  }
  3% {
    -moz-transform: rotate(-28deg);
  }
  5% {
    -moz-transform: rotate(34deg);
  }
  7% {
    -moz-transform: rotate(-32deg);
  }
  9% {
    -moz-transform: rotate(30deg);
  }
  11% {
    -moz-transform: rotate(-28deg);
  }
  13% {
    -moz-transform: rotate(26deg);
  }
  15% {
    -moz-transform: rotate(-24deg);
  }
  17% {
    -moz-transform: rotate(22deg);
  }
  19% {
    -moz-transform: rotate(-20deg);
  }
  21% {
    -moz-transform: rotate(18deg);
  }
  23% {
    -moz-transform: rotate(-16deg);
  }
  25% {
    -moz-transform: rotate(14deg);
  }
  27% {
    -moz-transform: rotate(-12deg);
  }
  29% {
    -moz-transform: rotate(10deg);
  }
  31% {
    -moz-transform: rotate(-8deg);
  }
  33% {
    -moz-transform: rotate(6deg);
  }
  35% {
    -moz-transform: rotate(-4deg);
  }
  37% {
    -moz-transform: rotate(2deg);
  }
  39% {
    -moz-transform: rotate(-1deg);
  }
  41% {
    -moz-transform: rotate(1deg);
  }

  43% {
    -moz-transform: rotate(0);
  }
  100% {
    -moz-transform: rotate(0);
  }
}

@keyframes ring {
  0% {
    transform: rotate(0);
  }
  1% {
    transform: rotate(30deg);
  }
  3% {
    transform: rotate(-28deg);
  }
  5% {
    transform: rotate(34deg);
  }
  7% {
    transform: rotate(-32deg);
  }
  9% {
    transform: rotate(30deg);
  }
  11% {
    transform: rotate(-28deg);
  }
  13% {
    transform: rotate(26deg);
  }
  15% {
    transform: rotate(-24deg);
  }
  17% {
    transform: rotate(22deg);
  }
  19% {
    transform: rotate(-20deg);
  }
  21% {
    transform: rotate(18deg);
  }
  23% {
    transform: rotate(-16deg);
  }
  25% {
    transform: rotate(14deg);
  }
  27% {
    transform: rotate(-12deg);
  }
  29% {
    transform: rotate(10deg);
  }
  31% {
    transform: rotate(-8deg);
  }
  33% {
    transform: rotate(6deg);
  }
  35% {
    transform: rotate(-4deg);
  }
  37% {
    transform: rotate(2deg);
  }
  39% {
    transform: rotate(-1deg);
  }
  41% {
    transform: rotate(1deg);
  }

  43% {
    transform: rotate(0);
  }
  100% {
    transform: rotate(0);
  }
}


</style>
