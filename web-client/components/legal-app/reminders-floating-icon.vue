<template>
  <v-speed-dial
    v-model="fab"
    absolute
    top
    right
    direction="left"
    class="mt-8"
    transition="slide-x-reverse-transition"
  >
    <template v-slot:activator>
      <v-btn v-model="fab" color="warning" dark
             fab
      >
        <v-icon v-if="fab">
          mdi-close
        </v-icon>
        <v-icon v-else>
          mdi-bell
        </v-icon>
      </v-btn>
    </template>
    <v-row>
      <v-col>
        <v-card width="480px" class="mt-10">
          <v-card-title>Masz zbliżające się terminy</v-card-title>
          <v-virtual-scroll :items="deadlinesList" :item-height="50" height="120">
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
                      <v-icon color="orange darken-4" right>
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
      <v-col>
        <v-card width="480px" class="mt-10">
          <v-card-title>Masz zbliżające się terminy</v-card-title>
          <v-virtual-scroll :items="deadlinesList" :item-height="50" height="120">
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
                      <v-icon color="orange darken-4" right>
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
    deadlinesList: []
  }),
  async fetch() {
    await this.getComingDeadlines()
  },

  computed: {
    todayDate() {
      return todayDate()
    },
    query() {
      return queryDateForFloatingBell(this.todayDate)
    }
  },

  methods: {
    async getComingDeadlines() {
      try {
        let deadlines = await this.$axios.$get(`/api/legal-app-cases/deadlines/list-all${this.query}`)
        this.deadlinesList = deadlines
        console.log('upcoming deadlines', this.deadlinesList)
      } catch (error) {
        handleError(error)
      }

    },
    formatDateForCalendar(date) {
      return formatDate(date)
    }
  }
}

</script>

<style scoped>

</style>
