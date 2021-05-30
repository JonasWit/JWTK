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
      <template v-for="(user, index) in usersList">
        <v-list-item :key="user.id">
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
              <!--              <data-access-key-dialog :selected-user="user"/>-->
              <lock-user-dialog :selected-user="user"/>
              <roles-management-dialog :selected-user="user"/>
              <applications-access-dialog :selected-user="user"/>
              <delete-user-dialog :selected-user="user"/>
            </div>
          </v-list-item-content>
        </v-list-item>
        <v-divider v-if="index < usersList.length - 1" :key="index"></v-divider>
      </template>
    </v-list>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import {hasOccurrences} from "@/data/functions";
import {formatDate} from "@/data/date-extensions";
import DataAccessKeyDialog from "@/components/portal-admin/users-management/data-access-key-dialog";
import LockUserDialog from "@/components/portal-admin/users-management/lock-user-dialog";
import RolesManagementDialog from "@/components/portal-admin/users-management/roles-management-dialog";
import ApplicationsAccessDialog from "@/components/portal-admin/users-management/applications-access-dialog";
import DeleteUserDialog from "@/components/portal-admin/users-management/delete-user-dialog";

const initState = () => ({
  showDataAccessKeyDialog: false,
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
  components: {DeleteUserDialog, ApplicationsAccessDialog, RolesManagementDialog, LockUserDialog, DataAccessKeyDialog},
  data: initState,
  beforeMount() {
    this.loading = true;
    this.getUsers();
    this.loading = false;
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
    ...mapActions('admin-panel-store', ['getUsers']),
    formatDate(date) {
      return formatDate(date);
    },
    searchFilter(item, queryText, itemText) {
      return hasOccurrences(item.searchIndex, queryText);
    },
    sendInvite() {
      if (!this.$refs.inviteForm.validate()) return;

      if (this.loading) return;
      this.loading = true;

      const data = {
        email: this.email,
        returnUrl: location.origin
      };

      this.inviteRequest(data);
      this.loading = false;
    },
    inviteRequest(data) {
      return this.$axios.$post("/api/portal-admin/clients", data)
        .then(r => {
          this.$notifier.showSuccessMessage("User Invited!");
        })
        .catch((e) => {
          this.$notifier.showSuccessMessage(`${e}`);
        })
        .finally(this.getUsers());
    }
  },
};
</script>

<style scoped>

</style>
