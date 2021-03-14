<template>
  <div>
    <padmin-edit-access-key-form-dialog v-if="showKeyDialog" :selected-key="selectedKey"
                                        v-on:action-completed="keyDialogClosed"/>

    <padmin-create-access-key-component v-on:action-completed="keyAdded"/>

    <v-list>
      <v-list-item v-for="keyItem in keysList" :key="keyItem.id" class="mb-2">
        <v-list-item-content>
          <v-list-item-title>Key Name: {{ keyItem.name }}</v-list-item-title>
          <v-list-item-subtitle :class="colorDates(keyItem.expireDate)">Expiration Date: {{
              keyItem.expireDate.substr(0, 10)
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
            <v-btn class="mx-3" icon @click.stop="keyDialogOpen(keyItem)">
              <v-icon medium color="warning">mdi-pencil</v-icon>
            </v-btn>
            <padmin-delete-access-key-dialog :selected-key="keyItem" v-on:action-completed="refreshAfterKeyDelete"/>
          </div>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </div>

</template>

<script>
import {mapActions, mapState} from "vuex";

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
    showKeyDialog: false,
  }),
  beforeMount() {
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
    colorDates(date) {
      let today = new Date();
      const expireDate = Date.parse(date);

      console.log('expire', expireDate);
      console.log('today', today);

      if (expireDate > today.addDays(7)) {
        return "success--text";
      }
      if (expireDate < today.addDays(7) && expireDate > today) {
        return "warning--text";
      }
      if (expireDate < today) {
        return "error--text";
      }
      return "error--text";

    },
    refreshAfterKeyDelete() {
      this.getAccessKeys();
      this.getUsers();
    },
    keyDialogOpen(keyItem) {
      this.selectedKey = keyItem;
      this.showKeyDialog = true;
    },
    keyDialogClosed() {
      this.showKeyDialog = false;
      this.selectedKey = null;
      this.searchResult = "";
      this.getAccessKeys();
      this.getUsers();
    },
    keyAdded() {
      this.selectedKey = null;
      this.searchResult = "";
      this.getAccessKeys();
      this.getUsers();
    }
  }
};
</script>

<style scoped>

</style>
