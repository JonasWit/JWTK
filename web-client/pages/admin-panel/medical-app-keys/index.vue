<template>
  <div class="ma-3">
    <div>
      <h3 class="text-h3 mb-4 text-center">Medical App Access Keys</h3>
    </div>
    <create-access-key key-type="medical-app"/>
    <v-expansion-panels focusable>
      <v-expansion-panel v-for="(keyItem, index) in keysList" :key="`key-index-${index}`">
        <v-expansion-panel-header :class="colorDate(keyItem.expireDate)">{{ keyItem.name }} Expires:
          {{ formatDate(keyItem.expireDate) }}
        </v-expansion-panel-header>
        <v-expansion-panel-content>
          <v-chip close @click:close="revokeKeyForUser(user)" v-for="(user, index) in keyItem.users" class="ma-2" small
                  :key="`chip-key-index-${index}`">
            {{ user.email }}
          </v-chip>
          <div class="d-flex justify-end">
            <add-user :selected-key="keyItem" key-type="medical-app"/>
            <edit-access-key-form-dialog :selected-key="keyItem" key-type="medical-app"/>
            <delete-access-key-dialog :selected-key="keyItem" key-type="medical-app"/>
          </div>
        </v-expansion-panel-content>
      </v-expansion-panel>
    </v-expansion-panels>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import CreateAccessKey from "@/components/portal-admin/access-keys/create-access-key";
import EditAccessKeyFormDialog from "@/components/portal-admin/access-keys/edit-access-key-form-dialog";
import DeleteAccessKeyDialog from "@/components/portal-admin/access-keys/delete-access-key-dialog";
import {colorDate, formatDate} from "@/data/date-extensions";
import AddUser from "@/components/portal-admin/access-keys/add-user";
import {revokeMedicalAppKey} from "@/data/endpoints/admin/admin-panel-endpoints";

export default {
  middleware: ["portal-admin"],
  name: "index",
  components: {AddUser, DeleteAccessKeyDialog, EditAccessKeyFormDialog, CreateAccessKey},
  fetchOnServer: false,

  data: () => ({
    activeKeys: [],
    searchResult: "",
    selectedKey: null,
  }),
  async fetch() {
    await this.getMedicalAppAccessKeys();
    await this.getUsers();
  },
  computed: {
    ...mapState('portal-admin-store', ['medicalAppAccessKeys']),
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    },
    keyItems() {
      return [].concat(this.medicalAppAccessKeys.map(x => searchItemFactory(x.id)));
    },
    keysList() {
      if (!this.searchResult) {
        this.medicalAppAccessKeys.forEach(ak => {
          ak.assignedUsers = this.users?.filter(user => user?.dataAccessKey?.id === ak.id);
        });
        return this.medicalAppAccessKeys;
      } else {
        return this.medicalAppAccessKeys.filter(x => x.name === this.searchResult);
      }
    }
  },
  methods: {
    ...mapActions('portal-admin-store', ['getUsers', 'getMedicalAppAccessKeys']),
    async revokeKeyForUser(user) {
      try {
        await this.$axios.post(revokeMedicalAppKey(), {userId: user.id});
        this.$notifier.showSuccessMessage("User removed from key!");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        await this.$fetch();
      }
    },
    formatDate(date) {
      return formatDate(date);
    },
    colorDate(date) {
      return colorDate(date);
    }
  }
};
</script>

<style scoped>

</style>
