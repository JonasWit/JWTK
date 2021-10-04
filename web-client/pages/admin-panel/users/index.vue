<template>
  <div class="ma-3">
    <div>
      <h3 class="text-h3 mb-4 text-center">Users</h3>
    </div>
    <div class="mb-4">
      <invite-user v-on:action-complete="refresh"/>
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

            <div v-if="user.legalAppDataAccessKey">
              <v-list-item-subtitle class="success--text">
                Legal App Data Access Key: {{
                  user.legalAppDataAccessKey.name
                }}
              </v-list-item-subtitle>
              <v-list-item-subtitle :class="colorDate(user.legalAppDataAccessKey.expireDate)"> Legal App Expiration:
                {{ formatDate(user.legalAppDataAccessKey.expireDate) }}
              </v-list-item-subtitle>
            </div>

            <v-divider/>

            <v-list-item-subtitle class="success--text" v-if="user.medicalAppAllowed">Medical App: Allowed
            </v-list-item-subtitle>
            <v-list-item-subtitle class="error--text" v-else>Medical App: Forbidden</v-list-item-subtitle>

            <div v-if="user.medicalAppDataAccessKey">
              <v-list-item-subtitle :class="colorDate(user.medicalAppDataAccessKey.expireDate)">
                Medical App Data Access Key: {{
                  user.medicalAppDataAccessKey.name
                }}
              </v-list-item-subtitle>
              <v-list-item-subtitle class="success--text"> Medical App Expiration:
                {{ formatDate(user.medicalAppDataAccessKey.expireDate) }}
              </v-list-item-subtitle>
            </div>

            <v-divider/>

            <v-list-item-subtitle class="success--text" v-if="user.restaurantAppAllowed">Restaurant App: Allowed
            </v-list-item-subtitle>
            <v-list-item-subtitle class="error--text" v-else>Restaurant App: Forbidden</v-list-item-subtitle>

            <div v-if="user.restaurantAppDataAccessKey">
              <v-list-item-subtitle :class="colorDate(user.restaurantAppDataAccessKey.expireDate)">
                Restaurant App Data Key: {{
                  user.restaurantAppDataAccessKey.name
                }}
              </v-list-item-subtitle>
              <v-list-item-subtitle class="success--text"> Restaurant App Expiration:
                {{ formatDate(user.restaurantAppDataAccessKey.expireDate) }}
              </v-list-item-subtitle>
            </div>

          </v-list-item-content>
          <v-spacer/>
          <v-list-item-content>
            <div class="d-flex justify-end">
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
import {colorDate, formatDate} from "@/data/date-extensions";
import DeleteUserDialog from "@/components/portal-admin/users-management/delete-user-dialog";
import RolesManagementDialog from "@/components/portal-admin/users-management/roles-management-dialog";
import LockUserDialog from "@/components/portal-admin/users-management/lock-user-dialog";
import ApplicationsAccessDialog from "@/components/portal-admin/users-management/applications-access-dialog";
import InviteUser from "@/components/portal-admin/users-management/invite-user";

const initState = () => ({});

const searchItemFactory = (name, email) => ({
  name,
  email,
  searchIndex: (name + email).toLowerCase(),
  text: name
});

export default {
  components: {InviteUser, ApplicationsAccessDialog, LockUserDialog, RolesManagementDialog, DeleteUserDialog},
  middleware: ["portal-admin"],
  name: "index",
  data: () => ({
    selectedUser: null,
    searchResult: "",
  }),
  async fetch() {
    await this.getUsers();
  },
  computed: {
    ...mapState('portal-admin-store', ['users']),
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
    ...mapActions('portal-admin-store', ['getUsers']),
    formatDate(date) {
      return formatDate(date);
    },
    colorDate(date) {
      return colorDate(date);
    },
    searchFilter(item, queryText, itemText) {
      return hasOccurrences(item.searchIndex, queryText);
    },
    refresh() {
      this.$fetch();
    }
  },
};
</script>

<style scoped>

</style>
