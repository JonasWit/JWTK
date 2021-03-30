<template>
  <v-dialog v-model="dialog" :value="selectedUser" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-3" icon v-bind="attrs" v-on="on">
        <v-icon medium color="error">mdi-key-variant</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">
        Data Access Key
      </v-card-title>
      <v-form>
        <v-card-text>
          <p v-if="selectedUser && selectedUser.dataAccessKey">Current Key: {{
              selectedUser.dataAccessKey.name
            }}</p>
          <v-select v-model="form.dataAccessKey" :items="activeKeys" filled label="Choose a Key"></v-select>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn :disabled="!form.dataAccessKey" color="warning" text @click="grantAccessKey()">
            Grant Key
          </v-btn>
          <v-btn v-if="selectedUser" :disabled="!selectedUser.dataAccessKey" color="error" text
                 @click="revokeAccessKey()">
            Revoke Key
          </v-btn>
          <v-spacer/>
          <v-btn color="success" text @click="dialog = false">
            Cancel
          </v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>

import {mapActions, mapGetters} from "vuex";

export default {
  name: "data-access-key-dialog",
  props: {
    selectedUser: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    activeKeys: [],
    dialog: false,
    loading: false,
    form: {
      dataAccessKey: "",
      userId: ""
    },
  }),
  fetch() {
    this.activeKeys = this.accessKeys.map(x => x.name);
  },
  created() {
    if (this.selectedUser.dataAccessKey) {
      this.form.dataAccessKey = this.selectedUser.dataAccessKey;
    }
  },
  computed: {
    ...mapGetters('admin-panel-store', ['accessKeys'])
  },
  methods: {
    ...mapActions('admin-panel-store', ['getUsers']),
    async grantAccessKey() {
      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;
      return this.$axios.$post("/api/portal-admin/key-admin/user/grant/access-key", this.form)
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.dialog = false;
          this.getUsers();
        });
    },
    async revokeAccessKey() {
      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;
      return this.$axios.$post("/api/portal-admin/key-admin/user/revoke/access-key", this.form)
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.dialog = false;
          this.getUsers();
        });
    },
  },
};
</script>

<style scoped>

</style>
