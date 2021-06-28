<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn medium color="info" v-on="{ ...tooltip, ...dialog }">
            Nadaj dostęp
          </v-btn>
        </template>
        <span>Nadaj dostęp</span>
      </v-tooltip>
    </template>
    <v-card min-height="400px">
      <v-toolbar color="primary" dark class="mb-3">
        <v-toolbar-title>
          Nadaj dostęp do klienta
        </v-toolbar-title>
      </v-toolbar>
      <v-select class="mx-2"
                v-model="selectedUser"
                :items="eligibleUsers"
                item-text="email"
                :menu-props="{ maxHeight: '200' }"
                return-object
                small-chips
                deletable-chips
                label="Wybierz użytkownika"
                persistent-placeholder
                outlined
      ></v-select>
      <v-alert v-if="selectedUser.length === 0" elevation="5" text type="info" dismissible
               close-text="Zamknij">
        Z listy powyżej wybierz użytkownika, któremu chcesz nadać dostęp do klienta. Upoważniona osoba będzie miała
        dostęp do panelu klienta, w którym będzie mogła zarządzać swoimi notatkami, rozliczeniami oraz sprawami.
      </v-alert>
      <v-list>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>
              Użytkownik: {{ selectedUser.username }}
            </v-list-item-title>
            <v-list-item-subtitle>
              Adres email: {{ selectedUser.email }}
            </v-list-item-subtitle>
            <v-list-item-subtitle>
              Rola: {{ selectedUser.role }}
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn text color="error" @click="dialog=false">
          Anuluj
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn text color="success" @click="grantAccess()">
          Zatwierdź
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {grantAccess} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapActions, mapGetters} from "vuex";

export default {
  name: "grant-access",
  props: {
    clientItem: {
      type: Object,
      required: true
    }
  },
  data: () => ({
    selectedUser: [],
    dialog: false,
    loading: false,
  }),

  async fetch() {
    let clientId = this.clientItem.id;
    await this.getEligibleUsersList({clientId})
    console.warn('eligible users list:', this.eligibleUsers)
  },

  computed: {
    ...mapGetters('legal-app-client-store', ['eligibleUsers']),
  },

  methods: {
    ...mapActions('legal-app-client-store', ['getAllowedUsers']),
    ...mapActions('legal-app-client-store', ['getEligibleUsersList']),

    async grantAccess() {
      const payload = {
        userId: this.selectedUser.id
      }
      try {
        let clientId = this.clientItem.id;
        console.warn('user id', payload)
        await this.$axios.$post(grantAccess(clientId), payload)
        this.$notifier.showSuccessMessage("Dostęp nadany pomyślnie");
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      } finally {
        Object.assign(this.$data, this.$options.data.call(this));
        let clientId = this.clientItem.id;
        console.warn('client id:', clientId)
        await this.getAllowedUsers({clientId})
        await this.getEligibleUsersList({clientId})
        this.dialog = false

      }
    }
  }
}
</script>

<style scoped>

</style>
