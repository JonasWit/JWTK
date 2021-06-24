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
        <v-combobox v-model="select" item-text="email" :items="usersAvailableForKey" label="Select User"></v-combobox>
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
    loading: false,
  }),
  async fetch() {
    await this.getUsers();
  },
  computed: {
    ...mapGetters('portal-admin-store', ['usersAvailableForKey'])
  },
  methods: {
    ...mapActions('portal-admin-store', ['getUsers', 'getLegalAppAccessKeys', 'getMedicalAppAccessKeys']),
    async grantKey() {
      if (this.loading) return;
      this.loading = true;

      const payload = {
        keyId: this.selectedKey.id,
        UserId: this.select.id
      };

      try {
        await this.$axios.post(`/api/portal-admin/key-admin/${this.keyType}/user/grant/access-key`, payload);
        this.$notifier.showSuccessMessage("Granted!");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.getLegalAppAccessKeys();
        this.getMedicalAppAccessKeys();
        this.getUsers();
        this.select = null;
        this.loading = false;
        this.dialog = false;
      }
    }
  }
};
</script>

<style scoped>

</style>
