<template>
  <v-form ref="inviteForm" v-model="validation.valid">
    <v-text-field required :rules="validation.emailRule" label="Email" v-model="form.email">
      <template slot="append-outer">
        <v-btn color="primary" depressed @click="sendInvite">Invite</v-btn>
      </template>
    </v-text-field>
  </v-form>
</template>

<script>
import {emailRule} from "@/data/vuetify-validations";

export default {
  name: "invite-user",
  data: () => ({
    searchResult: "",
    form: {
      email: ""
    },
    validation: {
      valid: false,
      emailRule: emailRule()
    },
  }),
  methods: {
    sendInvite() {
      if (!this.$refs.inviteForm.validate()) return;

      const data = {
        email: this.form.email,
        returnUrl: location.origin
      };
      this.inviteRequest(data);
    },
    async inviteRequest(data) {
      try {
        await this.$axios.$post("/api/portal-admin/clients", data);
        this.$notifier.showSuccessMessage("User Invited!");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.$refs.inviteForm.reset();
        this.$refs.inviteForm.resetValidation();
        this.$emit('action-complete');
      }
    },
  },
};
</script>

<style scoped>

</style>
