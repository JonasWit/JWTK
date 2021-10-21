<template>
  <v-speed-dial v-model="fab" fixed bottom right class="mr-8 mb-8" transition="slide-x-transition">
    <template v-slot:activator>
      <v-btn v-model="fab" :color="bellColor" dark fab>
        <v-icon v-if="fab">
          mdi-close
        </v-icon>
        <v-icon v-else :class="bellClass">
          mdi-bell
        </v-icon>
        <v-badge color="primary" bottom :content="newEvents.length" :value="newEvents.length">
        </v-badge>
      </v-btn>
    </template>
    <v-row class="mt-16 direction d-flex">
      <v-card width="400">
        <v-toolbar color="error" class="white--text">
          <v-toolbar-title>Plany na dzisiaj i jutro</v-toolbar-title>
        </v-toolbar>
        <v-virtual-scroll :items="newEvents" height="300" item-height="92">
          <template v-slot:default="{ item }">
            <v-list-item :style="backgroundColor(item)">
              <v-list-item-content>
                <v-list-item-title class="font-weight-bold">{{ categoryToDisplay(item) }}</v-list-item-title>
                <v-list-item-title>{{ item.name }}</v-list-item-title>
                <v-list-item-subtitle>{{ item.details }}</v-list-item-subtitle>
                <v-list-item-subtitle>{{ formatDateForCalendar(item.deadline) }}
                </v-list-item-subtitle>
              </v-list-item-content>
              <v-list-item-action>
                <nuxt-link class="nav-item" :to="eventLink(item)">
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-icon small color="error" v-bind="attrs" v-on="on">
                        mdi-open-in-new
                      </v-icon>
                    </template>
                    <span>Sprawdź</span>
                  </v-tooltip>
                </nuxt-link>
              </v-list-item-action>
            </v-list-item>
            <v-divider></v-divider>
          </template>
        </v-virtual-scroll>
      </v-card>
    </v-row>
  </v-speed-dial>
</template>

<script>
import {
  formatDate,
  queryDateForFloatingBell,
  todayDate
} from "@/data/date-extensions";
import {mapActions, mapGetters, mapState} from "vuex";
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
    loader: false,
  }),

  async fetch() {
    try {
      let dates = this.query
      await this.getEventsForNotifications({dates})
    } catch (error) {
      handleError(error);
    }
  },
  computed: {
    ...mapState('legal-app-client-store', ['newEvents']),
    ...mapGetters('legal-app-client-store', ['notificationsCount']),
    todayDate() {
      return todayDate()
    },
    query() {
      return queryDateForFloatingBell(this.todayDate)
    },

    bellColor() {
      if (this.newEvents.length === 0) {
        return 'green'
      } else {
        return 'red'
      }
    },
    bellClass() {
      if (this.newEvents.length > 0) {
        return 'reminder-bell'
      } else {
        return ''
      }
    }
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getEventsForNotifications']),
    eventLink(item) {
      if (item.type === 'deadline') {
        let clientId = item.client
        let caseId = item.case
        return `/legal-application/clients/${clientId}/cases/${caseId}/deadlines`
      }
      if (item.type === 'event') {
        return '/legal-application/reminders'
      }
    },
    categoryToDisplay(item) {
      if (item.category === 0) {
        return "Spotkanie"
      }
      if (item.category === 1) {
        return "Przypomnienie"
      }
      if (item.category === 2) {
        return "Zadanie"
      } else {
        return "Termin"
      }
    },
    formatDateForCalendar(date) {
      return formatDate(date)
    },
    backgroundColor(item) {
      if (item.category === 4) {
        return "border-left: solid 5px red"
      }
      if (item.category === 0) {
        return "border-left: solid 5px blue"
      }
      if (item.category === 1) {
        return "border-left: solid 5px orange"
      }
      if (item.category === 2) {
        return "border-left: solid 5px green"
      }


    }

  }
}

</script>

<style scoped>

.direction {
  position: absolute;
  bottom: 45%;
  width: 800px;
}

@media only screen and (max-width: 600px) {
  .direction {
    position: absolute;
    bottom: 45%;
    right: -50%;
    width: 100vw;
  }

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
