<template>
  <div>
    <padmin-create-access-key-component v-on:action-completed="refreshData"/>
    <v-list>
      <v-list-item v-for="keyItem in keysList" :key="keyItem.id" class="mb-2">
        <v-list-item-content>
          <v-list-item-title>Key Name: {{ keyItem.name }}</v-list-item-title>
          <v-list-item-subtitle :class="colorDates(keyItem.expireDate)">Expiration Date: {{
              formatDate(keyItem.expireDate)
            }}
          </v-list-item-subtitle>
          <v-list-item-subtitle>Assigned Users: {{ keyItem.assignedUsers }}
          </v-list-item-subtitle>
          <v-list-item-subtitle>Related Legal App Data: {{ keyItem.relatedLegalAppClients }}
          </v-list-item-subtitle>
        </v-list-item-content>
        <v-spacer/>
        <v-list-item-content>
          <div class="d-flex justify-end">
            <padmin-edit-access-key-form-dialog :selected-key="keyItem" v-on:action-completed="refreshData"/>
            <padmin-delete-access-key-dialog :selected-key="keyItem" v-on:action-completed="refreshData"/>
          </div>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </div>

</template>

<script>
import {mapActions, mapState} from "vuex";
import {addDays, formatDate} from "@/data/date-extensions";

const searchItemFactory = (id, exp) => ({
  id,
  exp,
  searchIndex: (id + exp).toLowerCase(),
  text: id
});


export default {
  name: "access-keys-admin-panel",
  data: () => ({
    activeKeys: [],
    loading: false,
    searchResult: "",
    selectedKey: null,
  }),
  beforeMount() {
    console.log('access keys panel mounted');
    this.getAccessKeys();
  },
  computed: {
    ...mapState('admin-panel-store', ['accessKeys']),
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    },
    keyItems() {
      return []
        .concat(this.accessKeys.map(x => searchItemFactory(x.id)));
    },
    keysList() {
      if (!this.searchResult) {
        return this.accessKeys;
      } else {
        return this.accessKeys.filter(x => x.name === this.searchResult);
      }
    }
  },
  methods: {
    ...mapActions('admin-panel-store', ['getAccessKeys', 'getUsers']),
    formatDate(date) {
      return formatDate(date);
    },
    colorDates(date) {
      let today = new Date();
      const expireDate = Date.parse(date);

      if (expireDate > addDays(today, 7)) {
        return "success--text";
      }
      if (expireDate < addDays(today, 7) && expireDate > today) {
        return "warning--text";
      }
      if (expireDate < today) {
        return "error--text";
      }
      return "error--text";

    },
    refreshData() {
      this.getAccessKeys();
      this.getUsers();
    },
  }
};
</script>

<style scoped>

</style>
