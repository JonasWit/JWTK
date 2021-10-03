<template>
  <v-dialog v-model="dialog" :value="selectedUser" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-2" icon v-bind="attrs" v-on="on">
        <v-icon medium color="success">mdi-application-cog</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">Applications Access Management</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn v-if="selectedUser.legalAppAllowed" color="warning" text @click="revokeLegalAppAccess">
          Revoke Legal App Access
        </v-btn>
        <v-btn v-else=outlined color="warning" text @click="grantLegalAppAccess">
          Grant Legal App Access
        </v-btn>
        <v-spacer/>
      </v-card-actions>
      <v-card-actions>
        <v-btn v-if="selectedUser.medicalAppAllowed" color="warning" text @click="revokeMedicalAppAccess">
          Revoke Medical App Access
        </v-btn>
        <v-btn v-else=outlined color="warning" text @click="grantMedicalAppAccess">
          Grant Medical App Access
        </v-btn>
        <v-spacer/>
      </v-card-actions>
      <v-card-actions>
        <v-btn v-if="selectedUser.restaurantAppAllowed" color="warning" text @click="revokeRestaurantAppAccess">
          Revoke Restaurant App Access
        </v-btn>
        <v-btn v-else=outlined color="warning" text @click="grantRestaurantAppAccess">
          Grant Restaurant App Access
        </v-btn>
        <v-spacer/>
      </v-card-actions>
      <v-card-actions>
        <v-spacer/>
        <v-btn color="success" text @click="dialog = false">
          Cancel
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "applications-access-dialog",
  props: {
    selectedUser: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
  }),
  methods: {
    ...mapActions('portal-admin-store', ['getUsers']),
    async grantLegalAppAccess() {
      try {
        await this.$axios.$post("/api/portal-admin/user-admin/user/grant/legal-app", {userId: this.selectedUser.id});
        this.$notifier.showSuccessMessage("Access granted");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getUsers();
        this.dialog = false;
      }
    },
    async revokeLegalAppAccess() {
      try {
        await this.$axios.$post("/api/portal-admin/user-admin/user/revoke/legal-app", {userId: this.selectedUser.id});
        this.$notifier.showSuccessMessage("Access revoked");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getUsers();
        this.dialog = false;
      }
    },
    async grantMedicalAppAccess() {
      try {
        await this.$axios.$post("/api/portal-admin/user-admin/user/grant/legal-app", {userId: this.selectedUser.id});
        this.$notifier.showSuccessMessage("Access granted");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getUsers();
        this.dialog = false;
      }
    },
    async revokeMedicalAppAccess() {
      try {
        await this.$axios.$post("/api/portal-admin/user-admin/user/revoke/legal-app", {userId: this.selectedUser.id});
        this.$notifier.showSuccessMessage("Access revoked");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getUsers();
        this.dialog = false;
      }
    },
    async grantRestaurantAppAccess() {
      try {
        await this.$axios.$post("/api/portal-admin/user-admin/user/grant/legal-app", {userId: this.selectedUser.id});
        this.$notifier.showSuccessMessage("Access granted");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getUsers();
        this.dialog = false;
      }
    },
    async revokeRestaurantAppAccess() {
      try {
        await this.$axios.$post("/api/portal-admin/user-admin/user/revoke/legal-app", {userId: this.selectedUser.id});
        this.$notifier.showSuccessMessage("Access revoked");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getUsers();
        this.dialog = false;
      }
    },
  },
};
</script>

<style scoped>

</style>
