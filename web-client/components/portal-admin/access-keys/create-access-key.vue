<template>
  <div class="ma-3">
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

import {mapActions} from "vuex";
import {notEmptyAndLimitedRule, notEmptyRule} from "@/data/vuetify-validations";

export default {
  name: "create-access-key",
  props: {
    keyType: {
      required: true,
      type: String,
      default: ""
    }
  },
  data: () => ({
    loading: false,
    form: {
      keyName: "",
      expireDate: ""
    },
    validation: {
      valid: false,
      key: notEmptyAndLimitedRule('Cannot be empty and must be between 10 and 50', 10, 50),
      expireDate: notEmptyRule('Date cannot be empty'),
    },
  }),
  computed: {
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    },
  },
  methods: {
    ...mapActions('portal-admin-store', ['getLegalAppAccessKeys', 'getMedicalAppAccessKeys']),
    async addKey() {
      if (!this.$refs.createDataAccessKeyForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const payload = {
        keyName: this.form.keyName,
        expireDate: this.form.expireDate,
      };

      try {
        await this.$axios.$post(`/api/portal-admin/key-admin/${this.keyType}/access-key/create`, payload);
        this.resetForm();
        this.$notifier.showSuccessMessage("New key added!");
      } catch (error) {
        this.$notifier.showSuccessMessage(`Issue! ${error}`);
      } finally {
        this.getLegalAppAccessKeys();
        this.loading = false;
      }
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
