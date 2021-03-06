<template>
  <v-dialog :value="selectedUser" persistent width="500">
    <v-card>
      <v-card-title>
        Data Access Key
      </v-card-title>
      <v-form>
        <v-card-text>
          <p v-if="selectedUser && selectedUser.dataAccessKey">Current Key: {{ selectedUser.dataAccessKey }}</p>
          <v-select v-model="form.dataAccessKey" :items="activeKeys" filled label="Filled style"></v-select>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn :disabled="!form.dataAccessKey" outlined color="warning" text @click="grantAccessKey()">
            Grant Key
          </v-btn>
          <v-btn v-if="selectedUser" :disabled="!selectedUser.dataAccessKey" outlined color="error" text
                 @click="revokeAccessKey()">
            Revoke Key
          </v-btn>
          <v-spacer/>
          <v-btn outlined color="success" text @click="cancelDialog">
            Cancel
          </v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script>

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
    loading: false,
    form: {
      dataAccessKey: "",
      userId: ""
    },
  }),
  async fetch() {
    await this.$axios.$get("/api/portal-admin/access-keys")
      .then((res) => {
        console.log(res);
        this.activeKeys = res.map(x => x.id);
        console.log(this.activeKeys);
      });
  },
  created() {
    if (this.selectedUser.dataAccessKey) {
      this.form.dataAccessKey = this.selectedUser.dataAccessKey;
    }
  },
  methods: {
    async grantAccessKey() {
      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;
      console.log("accessKeyPayload", this.form.dataAccessKey);

      return this.$axios.post("/api/portal-admin/client/grant/access-key", this.form)
        .catch((e) => {
          console.log(e);
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
        });
    },
    async revokeAccessKey() {
      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;

      return this.$axios.post("/api/portal-admin/client/revoke/access-key", this.form)
        .catch((e) => {
          console.log(e);
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
        });
    },
    cancelDialog() {
      this.$emit('action-completed');
    }
  },
};
</script>

<style scoped>

</style>
