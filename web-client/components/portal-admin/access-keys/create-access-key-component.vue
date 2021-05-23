<template>
  <div>
    <v-form ref="createDataAccessKeyForm" v-model="validation.valid">
      <v-text-field v-model="form.keyName" :rules="validation.key" label="Key String" required></v-text-field>
      <v-menu ref="menu" transition="scale-transition" offset-y min-width="auto" :close-on-content-click="false"
              :return-value.sync="form.expireDate">
        <template v-slot:activator="{ on, attrs }">
          <v-text-field :rules="validation.expireDate" readonly v-bind="attrs" v-on="on" v-model="form.expireDate"
                        label="Expire Date" prepend-icon="mdi-calendar"></v-text-field>
        </template>
        <v-date-picker :min="todayDate" v-model="form.expireDate" no-title scrollable>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="menu = false">
            Cancel
          </v-btn>
          <v-btn text color="primary" @click="$refs.menu.save(form.expireDate)">
            OK
          </v-btn>
        </v-date-picker>
      </v-menu>
    </v-form>

    <div class="d-flex align--left">
      <v-spacer/>
      <v-btn text color="warning" @click="addKey">Add Key</v-btn>
    </div>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
  name: "create-access-key-component",
  data: () => ({
    loading: false,
    form: {
      keyName: "",
      expireDate: ""
    },
    validation: {
      valid: false,
      key: [
        v => !!v || "Key is required!",
        v => (v?.length >= 10 && v?.length <= 50) || "Between 10 and 50 characters!",
      ],
      expireDate: [
        v => !!v || "Duration is required!",
      ],
    },
  }),
  computed: {
    ...mapState('admin-panel-store', ['accessKeys']),
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    },
  },
  methods: {
    ...mapActions('admin-panel-store', ['getAccessKeys']),
    addKey() {
      if (!this.$refs.createDataAccessKeyForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const payload = {
        keyName: this.form.keyName,
        expireDate: this.form.expireDate,
      };

      return this.$axios.$post("/api/portal-admin/key-admin/access-key/create", payload)
        .then(() => {
          this.resetForm();
          this.getAccessKeys();
          this.$notifier.showSuccessMessage("New key added!");
        })
        .catch((e) => {
          console.error(e);
          this.$notifier.showSuccessMessage(`Issue! ${e}`);
        }).finally(() => {
          this.loading = false;
          this.dialog = false;
        });
    },
    resetForm() {
      this.$refs.createDataAccessKeyForm.reset();
      this.$refs.createDataAccessKeyForm.resetValidation();
    },
  }
};
</script>

<style scoped>

</style>
