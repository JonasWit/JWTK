<template>
  <v-dialog :value="selectedUser" persistent width="500">
    <v-card>
      <v-card-title>
        Data Access Key
      </v-card-title>
      <v-form ref="dataAccessKeyForm" v-model="validation.valid">
        <v-card-text>
          <p v-if="selectedUser && selectedUser.dataAccessKey">Current Key: {{ selectedUser.dataAccessKey }}</p>
          <!--          <v-text-field :rules="validation.key" label="Key" v-model="form.dataAccessKey"></v-text-field>-->

          <v-combobox clearable :rules="validation.key" v-model="form.dataAccessKey" :items="activeKeys"></v-combobox>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn :disabled="!validation.valid" outlined color="warning" text @click="grantAccessKey()">
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
    validation: {
      valid: false,
      key: [
        v => !!v || "Key is required!",
        v => v?.length >= 5 || "5 or more characters!"
      ],
    },
  }),
  async fetch() {
    await this.$axios.$get("/api/admin/access-keys")
      .then((res) => {
        console.log(res);
        this.activeKeys = res;
      });
  },
  created() {
    if (this.selectedUser.dataAccessKey) {
      this.form.dataAccessKey = this.selectedUser.dataAccessKey;
    }
  },
  methods: {
    async grantAccessKey() {
      if (!this.$refs.dataAccessKeyForm.validate()) return;

      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;
      console.log("accessKeyPayload", this.form.dataAccessKey);

      return this.$axios.post("/api/admin/client/grantAccessKey", this.form)
        .catch((e) => {
          console.log(e);
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
        });
    },
    async revokeAccessKey() {
      if (!this.selectedUser.dataAccessKey) return;

      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.selectedUser.id;

      return this.$axios.post("/api/admin/client/revokeAccessKey", this.form)
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
