<template>
  <div>
    <div>
      <v-text-field label="Email" :disabled="loading" v-model="email">
        <template slot="append-outer">
          <v-btn :disabled="loading" color="primary" @click="sendInvite">Invite</v-btn>
        </template>
      </v-text-field>
    </div>

    <v-list>
      <v-list-item v-for="user in users" :key="user.id" class="mb-2">
        <v-list-item-content>
          <v-list-item-title>User Name: {{ user.username }}</v-list-item-title>
          <v-list-item-title>Email: {{ user.email }}</v-list-item-title>
          <v-list-item-subtitle>Role: {{ user.role }}</v-list-item-subtitle>

          <v-list-item-subtitle v-if="user.dataAccessKey">Data Access Key: {{
              user.dataAccessKey
            }}
          </v-list-item-subtitle>
          <v-list-item-subtitle v-else>No Data Access Key</v-list-item-subtitle>
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
    <data-access-key-dialog v-on:action-completed="dataKeyDialogClosed" v-if="showDataAccessKeyDialog"
                            :selected-user="selectedUser"/>
    <lock-user-dialog v-on:action-completed="lockDialogClosed" v-if="showLockDialog" :selected-user="selectedUser"/>
    <roles-management-dialog v-on:action-completed="rolesDialogClosed" v-if="showRolesDialog"
                             :selected-user="selectedUser"/>
    <applications-access-dialog v-on:action-completed="appsDialogClosed" v-if="showAppsDialog"
                                :selected-user="selectedUser"/>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import DataAccessKeyDialog from "@/components/portal-admin/data-access-key-dialog";
import LockUserDialog from "@/components/portal-admin/lock-user-dialog";
import RolesManagementDialog from "@/components/portal-admin/roles-management-dialog";
import ApplicationsAccessDialog from "@/components/portal-admin/applications-access-dialog";

const initState = () => ({
  showDataAccessKeyDialog: false,
  showRolesDialog: false,
  showAppsDialog: false,
  showLockDialog: false,
  selectedUser: null,
  loading: false,
  email: "",
});

export default {
  components: {RolesManagementDialog, LockUserDialog, DataAccessKeyDialog, ApplicationsAccessDialog},
  name: "user-admin-panel",
  data: initState,
  beforeMount() {
    this.getUsers();
  },
  computed: {
    ...mapState('admin-panel-store', ['users'])
  },
  methods: {
    ...mapActions('admin-panel-store', ['getUsers']),
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
      this.selectedUser = null,
        this.loading = false,
        this.email = "",
        this.dataKeyForm = {
          dataKeyString: "",
          userId: ""
        };
    },
    sendInvite() {
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
