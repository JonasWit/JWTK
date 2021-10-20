<template>
  <v-dialog v-model="dialog" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-3" icon v-bind="attrs" v-on="on">
        <v-icon medium color="error">mdi-plus</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">Grant Access Key</v-card-title>
      <v-divider/>
      <v-card-text>
        <v-combobox v-model="select" item-text="email" :items="allowedUsers" label="Select User"></v-combobox>
      </v-card-text>
      <v-card-actions>
        <v-btn text color="warning" @click="grantKey">Grant</v-btn>
        <v-spacer/>
        <v-btn text color="success" @click="dialog = false">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapActions, mapGetters} from "vuex";
import {grantAccessKey} from "@/data/endpoints/admin/admin-panel-endpoints";

export default {
  name: "add-user",
  props: {
    keyType: {
      required: true,
      type: String,
      default: ""
    },
    selectedKey: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    select: null,
    dialog: null,
    allowedUsers: []
  }),
  async fetch() {
    if (this.keyType === "legal-app") {
      this.allowedUsers = this.usersAvailableForLegalKey;
    }
    if (this.keyType === "medical-app") {
      this.allowedUsers = this.usersAvailableForMedicalKey;
    }
    if (this.keyType === "restaurant-app") {
      this.allowedUsers = this.usersAvailableForRestaurantKey;
    }
  },
  computed: {
    ...mapGetters('portal-admin-store', ['usersAvailableForRestaurantKey', 'usersAvailableForLegalKey', 'usersAvailableForMedicalKey'])
  },
  methods: {
    ...mapActions('portal-admin-store', ['getUsers', 'getLegalAppAccessKeys', 'getRestaurantAppAccessKeys', 'getMedicalAppAccessKeys']),
    async grantKey() {
      const payload = {
        keyId: this.selectedKey.id,
        UserId: this.select.id
      };
      try {
        this.$notifier.showSuccessMessage("Grantedting key: ", payload);
        await this.$axios.post(grantAccessKey(this.keyType), payload);
        this.$notifier.showSuccessMessage("Granted!");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.select = null;
        this.dialog = false;
        if (this.keyType === "legal-app") {
          this.getLegalAppAccessKeys();
        }
        if (this.keyType === "medical-app") {
          this.getMedicalAppAccessKeys();
        }
        if (this.keyType === "restaurant-app") {
          this.getRestaurantAppAccessKeys();
        }
        this.getUsers();
      }
    }
  }
};
</script>

<style scoped>

</style>
