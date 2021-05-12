<template>
  <legalapp-layout>
    <template v-slot:content>
      <v-snackbar :color="snackbar.color" v-model="snackbar.show" :timeout="timeout">
        <span>{{ snackbar.message }}</span>
        <v-btn text @click="snackbar = false">Zamknij</v-btn>
      </v-snackbar>

      <v-toolbar extended>
        <v-toolbar-title class="mr-3">
          Lista Klientów
        </v-toolbar-title>

        <v-autocomplete return-object clearable v-model="searchResult" placeholder="Start typing to Search" dense
                        hide-details append-icon="" prepend-inner-icon="mdi-magnify" :items="clientItems"
                        :filter="searchFilter">
          <template v-slot:item="{item ,on , attrs}">
            <v-list-item v-on="on" :attrs="attrs">
              <v-list-item-content>{{ item.name }}</v-list-item-content>
            </v-list-item>
          </template>
        </v-autocomplete>
        <template v-slot:extension>
          <v-btn fab color="primary" left absolute @click="dialog = !dialog">
            <v-icon>mdi-plus</v-icon>
          </v-btn>
        </template>
      </v-toolbar>
      <div v-scroll="onScroll">
        <v-list>
          <legalapp-client-list-item :client-item="ci" v-for="ci in clientList" :key="`ci-item-${ci.id}`"/>
        </v-list>
      </div>
      <v-dialog v-model="dialog" max-width="500px">
        <v-form ref="addNewClientForm" v-model="validation.valid">
          <v-card-text>
            <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj nowego Klienta"
                          required></v-text-field>
            <small class="grey--text">* Hint text here</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text color="primary" @click="handleSubmit()">
              Dodaj
            </v-btn>
          </v-card-actions>
        </v-form>
      </v-dialog>
    </template>
  </legalapp-layout>
</template>

<script>
import NavigationDrawer from "@/components/legal-app/navigation-drawer";
import {hasOccurrences} from "@/data/functions";

const searchItemFactory = (name, id) => ({
  id,
  name,
  searchIndex: (name).toLowerCase(),
  text: name
});

export default {
  name: "index",
  components: {NavigationDrawer},
  data: () => ({
    searchResult: "",
    clientSearchItems: [],
    clientList: [],
    finished: false,
    loading: false,
    searchConditionsProvided: false,
    cursor: 0,
    dialog: false,
    form: {
      name: "",

    },
    validation: {
      valid: false,
      name: [
        v => !!v || "Nazwa jest wymagana!",
        v => (v?.length >= 10 && v?.length <= 50) || "Between 10 and 50 characters!",
      ],
    },
    snackbar: {
      show: false,
      message: null,
      color: null
    },
    timeout: 4000,
  }),

  async fetch() {
    this.clientSearchItems = await this.$axios.$get("/api/legal-app-clients/clients/basic-list");
  },
  created() {
    this.handleFeed();
  },
  watch: {
    searchResult(searchResult) {
      if (this.searchResult) {
        console.warn('search result:', this.searchResult);
        this.loading = true;

        this.$axios.$get(`/api/legal-app-clients/client/${this.searchResult.id}`)
          .then(clientFound => {
            if (clientFound) {
              this.clientList = [];
              this.clientList.push(clientFound);
              this.cursor = 0;
              this.finished = false;
            }
          })
          .finally(() => this.loading = false);
      } else {
        this.clientList = [];
        this.handleFeed();
      }
    }
  },
  computed: {
    clientItems() {
      return []
        .concat(this.clientSearchItems.map(x => searchItemFactory(x.name, x.id)));
    },
    query() {
      if (this.searchConditionsProvided) {
        this.cursor = 0;
        this.clientList = [];
        this.showSelectedClient();
      } else {
        return `cursor=${this.cursor}&take=10`;
      }
    },
  },
  methods: {
    searchFilter(item, queryText, itemText) {
      return hasOccurrences(item.searchIndex, queryText);
    },
    onScroll() {
      if (this.finished || this.loading || this.searchResult) return;
      const loadMore = document.body.offsetHeight - (window.pageYOffset + window.innerHeight) < 500;
      if (loadMore) {
        this.handleFeed();
      }
    },
    handleFeed() {
      console.warn('query!', this.query);
      this.loading = true;

      this.$axios.$get(`/api/legal-app-clients/clients?${this.query}`)
        .then(clientsFeed => {
          console.warn('clients form API for feed', clientsFeed);
          if (clientsFeed.length === 0) {
            this.finished = true;
          } else {
            clientsFeed.forEach(x => this.clientList.push(x));
            this.cursor += 10;
          }
        })
        .finally(() => this.loading = false);
    },
    handleSubmit() {
      if (!this.$refs.addNewClientForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const client = {
        name: this.form.name,
      };
      return this.$axios.$post("/api/legal-app-clients/create", client)
        .then((resp) => {
          console.warn('create client response', resp);
          this.resetForm();
          this.snackbar = {
            message: `Nowy klient ${this.form.name} został dodany pomyślnie!`,
            color: 'success',
            show: true
          };
        })
        .catch((e) => {
          console.warn('create client error', e);
          this.snackbar = {
            message: 'Wystąpił błąd. Spróbuj ponownie.',
            color: 'error',
            show: true

          };
        }).finally(() => {
          this.$nuxt.refresh();
          this.loading = false;
          this.dialog = false;

        });


    },
    resetForm() {
      this.$refs.createDataAccessKeyForm.reset();
      this.$refs.createDataAccessKeyForm.resetValidation();
    },
  }
};
</script>

<style scoped>

</style>
