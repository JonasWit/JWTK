<template>
  <div>
    <div class="mb-4">
      <v-form>
        <v-text-field ref="inviteForm" :rules="validation.emailRules" label="Email" :disabled="loading" v-model="email">
          <template slot="append-outer">
            <v-btn color="primary" depressed :disabled="loading" @click="sendInvite">Invite</v-btn>
          </template>
        </v-text-field>
      </v-form>
    </div>

    <div class="mb-4">
      <v-autocomplete clearable v-model="searchResult" placeholder="Start typing to Search" dense hide-details
                      append-icon="" prepend-inner-icon="mdi-magnify" :items="userItems" :filter="searchFilter">
        <template v-slot:item="{item,on , attrs}">
          <v-list-item v-on="on" :attrs="attrs">
            <v-list-item-content>{{ item.email }}</v-list-item-content>
            <v-spacer/>
            <v-list-item-content>{{ item.name }}</v-list-item-content>
          </v-list-item>
        </template>
      </v-autocomplete>
    </div>

    <v-list>
      <v-list-item v-for="user in usersList" :key="user.id" class="mb-2">
        <v-list-item-content>
          <v-list-item-title>User Name: {{ user.username }}</v-list-item-title>
          <v-list-item-title>Email: {{ user.email }}</v-list-item-title>
          <v-list-item-subtitle>Role: {{ user.role }}</v-list-item-subtitle>
          <v-list-item-subtitle class="success--text" v-if="user.legalAppAllowed">Legal App: Allowed
          </v-list-item-subtitle>
          <v-list-item-subtitle class="error--text" v-else>Legal App: Forbidden</v-list-item-subtitle>

          <div v-if="user.dataAccessKey">
            <v-list-item-subtitle class="success--text">Data Access Key: {{ user.dataAccessKey.name }}
            </v-list-item-subtitle>
            <v-list-item-subtitle class="success--text">Expiration:
              {{ formatDate(user.dataAccessKey.expireDate) }}
            </v-list-item-subtitle>
          </div>
          <v-list-item-subtitle class="error--text" v-else>No Data Access Key</v-list-item-subtitle>
        </v-list-item-content>
        <v-spacer/>
        <v-list-item-content>
          <div class="d-flex justify-end">
            <v-btn class="mx-3" icon @click.stop="dataKeyDialogOpen(user)">
              <v-icon medium color="error">mdi-key-variant</v-icon>
            </v-btn>
            <v-btn class="mx-3" icon @click.stop="lockDialogOpen(user)">
              <v-icon color="success" v-if="!user.locked" medium>mdi-lock-open-variant</v-icon>
              <v-icon color="warning" v-else medium>mdi-lock</v-icon>
            </v-btn>
            <v-btn class="mx-3" icon @click.stop="rolesDialogOpen(user)">
              <v-icon medium color="success">mdi-account-group</v-icon>
            </v-btn>
            <v-btn class="mx-3" icon @click.stop="appsDialogOpen(user)">
              <v-icon medium color="success">mdi-application-cog</v-icon>
            </v-btn>
            <v-btn class="mx-3" icon @click.stop="appsDialogOpen(user)">
              <v-icon medium color="error">mdi-delete</v-icon>
            </v-btn>
          </div>
        </v-list-item-content>
      </v-list-item>
    </v-list>
    <padmin-data-access-key-dialog v-on:action-completed="dataKeyDialogClosed" v-if="showDataAccessKeyDialog"
                                   :selected-user="selectedUser"/>
    <padmin-lock-user-dialog v-on:action-completed="lockDialogClosed" v-if="showLockDialog"
                             :selected-user="selectedUser"/>
    <padmin-roles-management-dialog v-on:action-completed="rolesDialogClosed" v-if="showRolesDialog"
                                    :selected-user="selectedUser"/>
    <padmin-applications-access-dialog v-on:action-completed="appsDialogClosed" v-if="showAppsDialog"
                                       :selected-user="selectedUser"/>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import {hasOccurrences} from "@/data/functions";
import {formatDate} from "@/data/dateExtensions";

const initState = () => ({
  showDataAccessKeyDialog: false,
  showRolesDialog: false,
  showAppsDialog: false,
  showLockDialog: false,
  selectedUser: null,
  loading: false,
  email: "",
  searchResult: "",
  validation: {
    valid: false,
    emailRules: [
      v => /.+@.+/.test(v) || 'E-mail must be valid',
    ],
  },
});

const searchItemFactory = (name, email) => ({
  name,
  email,
  searchIndex: (name + email).toLowerCase(),
  text: name
});

export default {
  name: "user-admin-panel",
  data: initState,
  beforeMount() {
    this.getUsers();
  },
  computed: {
    ...mapState('admin-panel-store', ['users']),
    userItems() {
      return []
        .concat(this.users.map(x => searchItemFactory(x.username, x.email)));
    },
    usersList() {
      if (!this.searchResult) {
        return this.users;
      } else {
        return this.users.filter(x => x.username === this.searchResult);
      }
    }
  },
  methods: {
    ...mapActions('admin-panel-store', ['getAccessKeys', 'getUsers']),
    formatDate(date) {
      return formatDate(date);
    },
    searchFilter(item, queryText, itemText) {
      return hasOccurrences(item.searchIndex, queryText);
    },
    appsDialogOpen(user) {
      this.selectedUser = user;
      this.showAppsDialog = true;
    },
    appsDialogClosed() {
      this.showAppsDialog = false;
      this.selectedUser = null;
      this.getUsers();
    },
    rolesDialogOpen(user) {
      this.selectedUser = user;
      this.showRolesDialog = true;
    },
    rolesDialogClosed() {
      this.showRolesDialog = false;
      this.selectedUser = null;
      this.getUsers();
    },
    dataKeyDialogOpen(user) {
      this.selectedUser = user;
      this.showDataAccessKeyDialog = true;
    },
    dataKeyDialogClosed() {
      this.showDataAccessKeyDialog = false;
      this.selectedUser = null;
      this.getAccessKeys();
      this.getUsers();
    },
    lockDialogOpen(user) {
      this.selectedUser = user;
      this.showLockDialog = true;
    },
    lockDialogClosed() {
      this.showLockDialog = false;
      this.selectedUser = null;
      this.getUsers();
    },
    resetData() {
      this.selectedUser = null;
      this.loading = false;
      this.email = "";
    },
    sendInvite() {
      if (!this.$refs.inviteForm.validate()) return;

      if (this.loading) return;
      this.loading = true;

      const data = {
        email: this.email,
        returnUrl: location.origin
      };

      return this.$axios.$post("/api/portal-admin/clients", data)
        .then(() => {
          this.email = "";
          this.loading = false;
        })
        .finally(this.getUsers());
    }
  }
};
</script>

<style scoped>

</style>
