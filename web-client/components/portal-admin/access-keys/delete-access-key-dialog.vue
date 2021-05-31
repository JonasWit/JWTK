<template>
  <v-dialog v-model="dialog" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-3" icon v-bind="attrs" v-on="on">
        <v-icon medium color="error">mdi-delete</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">Delete Access Key</v-card-title>
      <v-divider/>
      <v-card-text>
        <v-card-subtitle class="success--text" v-if="selectedKey.assignedUsers === 0">This key has no assigned users
        </v-card-subtitle>
        <v-card-subtitle class="error--text" v-else>This key has assigned users! Users: {{
            selectedKey.assignedUsers
          }}
        </v-card-subtitle>
      </v-card-text>
      <v-card-actions>
        <v-btn text color="warning" @click="deleteKey">Delete</v-btn>
        <v-spacer/>
        <v-btn text color="success" @click="dialog = false">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "delete-access-key-dialog",
  props: {
    selectedKey: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: null,
    loading: false
  }),
  methods: {
    ...mapActions('portal-admin-store', ['getLegalAppAccessKeys', 'getMedicalAppAccessKeys']),
    async deleteKey() {
      if (this.loading) return;
      this.loading = true;

      try {
        await this.$axios.$delete(`/api/portal-admin/key-admin/${this.selectedKey.keyType}/access-key/delete/${this.selectedKey.id}`);
      } catch (error) {
        console.error(error);
      } finally {
        this.getLegalAppAccessKeys();
        this.loading = false;
        this.dialog = false;
      }
    }
  }
};
</script>

<style scoped>

</style>
