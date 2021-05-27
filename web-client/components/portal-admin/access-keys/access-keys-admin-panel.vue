<template>
  <div>
    <create-access-key-component/>
    <v-expansion-panels focusable>
      <v-expansion-panel v-for="(keyItem, index) in keysList" :key="`key-index-${index}`">
        <v-expansion-panel-header :class="colorDates(keyItem.expireDate)">{{ keyItem.name }} Expires:
          {{ formatDate(keyItem.expireDate) }}
        </v-expansion-panel-header>
        <v-expansion-panel-content>
          <v-chip v-for="user in keyItem.assignedUsers" class="ma-2" small>
            {{ user.email }}
          </v-chip>
          <div class="d-flex justify-end">
            <edit-access-key-form-dialog :selected-key="keyItem"/>
            <delete-access-key-dialog :selected-key="keyItem"/>
          </div>
        </v-expansion-panel-content>
      </v-expansion-panel>
    </v-expansion-panels>

  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import {addDays, formatDate} from "@/data/date-extensions";
import CreateAccessKeyComponent from "@/components/portal-admin/access-keys/create-access-key-component";
import EditAccessKeyFormDialog from "@/components/portal-admin/access-keys/edit-access-key-form-dialog";
import DeleteAccessKeyDialog from "@/components/portal-admin/access-keys/delete-access-key-dialog";

const searchItemFactory = (id, exp) => ({
  id,
  exp,
  searchIndex: (id + exp).toLowerCase(),
  text: id
});

export default {
  name: "access-keys-admin-panel",
  components: {DeleteAccessKeyDialog, EditAccessKeyFormDialog, CreateAccessKeyComponent},
  fetchOnServer: false,
  data: () => ({
    activeKeys: [],
    loading: false,
    searchResult: "",
    selectedKey: null,
  }),
  fetch() {
    this.getAccessKeys();
    this.getUsers();
  },
  computed: {
    ...mapState('admin-panel-store', ['accessKeys', 'users']),
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    },
    keyItems() {
      return []
        .concat(this.accessKeys.map(x => searchItemFactory(x.id)));
    },
    keysList() {
      if (!this.searchResult) {
        this.accessKeys.forEach(ak => {
          ak.assignedUsers = this.users?.filter(user => user?.dataAccessKey?.id === ak.id);
        });
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
  }
};
</script>

<style scoped>

</style>
