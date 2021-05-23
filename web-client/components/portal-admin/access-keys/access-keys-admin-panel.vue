<template>
  <div>
    <create-access-key-component/>
    <v-list>
      <template v-for="(keyItem, index) in keysList">
        <v-list-item :key="`key-${keyItem.id}`">
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
              <edit-access-key-form-dialog :selected-key="keyItem"/>
              <delete-access-key-dialog :selected-key="keyItem"/>
            </div>
          </v-list-item-content>
        </v-list-item>
        <v-divider v-if="index < keysList.length - 1" :key="index"></v-divider>
      </template>
    </v-list>

    <v-expansion-panels focusable>
      <v-expansion-panel v-for="(keyItem, index) in keysList" :key="`key-index-${index}`">
        <v-expansion-panel-header>{{ keyItem.name }}</v-expansion-panel-header>
        <v-expansion-panel-content>
          <p :class="colorDates(keyItem.expireDate)" class="ma-0">Expiration Date: {{
              formatDate(keyItem.expireDate)
            }} </p>
          <p :class="colorDates(keyItem.expireDate)" class="ma-0">Assigned Users: {{ keyItem.assignedUsers }} </p>
          <p :class="colorDates(keyItem.expireDate)" class="ma-0">Related Legal App Data: {{
              keyItem.relatedLegalAppClients
            }} </p>
          <v-chip v-for="user in keyItem.assignedUsersDetials" class="ma-2" x-small>
            {{ user.name }}
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
        // let result = this.accessKeys.forEach(x => {
        //   let match =
        // });


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
