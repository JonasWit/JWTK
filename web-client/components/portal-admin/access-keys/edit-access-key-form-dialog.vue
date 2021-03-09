<template>
  <v-dialog persistent width="700" :value="selectedKey">
    <v-card>
      <v-card-title>Access Key Management</v-card-title>
      <v-divider></v-divider>
      <v-form ref="dataAccessKeyForm" v-model="validation.valid">

        <v-text-field class="ma-3" v-model="form.keyName" :rules="validation.key" label="Key String"
                      required></v-text-field>
        <v-menu ref="menu" transition="scale-transition" offset-y min-width="auto" :close-on-content-click="false"
                :return-value.sync="form.expireDate">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field class="ma-3" :rules="validation.expireDate" readonly v-bind="attrs" v-on="on"
                          v-model="form.expireDate" label="Expire Date" prepend-icon="mdi-calendar"></v-text-field>
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
      <v-card-actions>
        <v-btn text color="warning" @click="editKey">Update Key</v-btn>
        <v-spacer/>
        <v-btn text color="success" @click="cancelDialog">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapState} from "vuex";

export default {
  name: "edit-access-key-form-dialog",
  data: () => ({
    dialog: null,
    loading: false,
    form: {
      keyName: "",
      expireDate: ""
    },
    validation: {
      valid: false,
      key: [
        v => !!v || "Key is required!",
        v => v?.length >= 10 || "10 or more characters!"
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
  props: {
    selectedKey: {
      required: false,
      type: Object,
      default: null
    },
  },
  mounted() {
    this.form.keyName = this.selectedKey.name;
    this.form.expireDate = this.selectedKey.expireDate.substr(0, 10);
  },
  methods: {
    editKey() {
      if (this.loading) return;
      this.loading = true;

      const payload = {
        newKeyName: this.form.keyName,
        oldKeyName: this.selectedKey.name,
        expireDate: this.form.expireDate,
      };

      return this.$axios.$put("/api/portal-admin/key-admin/access-key/update", payload)
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
        });
    },
    cancelDialog() {
      this.dialog = false;
      this.$emit('action-completed');
    },
  }
};
</script>

<style scoped>

</style>
