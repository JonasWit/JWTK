<template>
  <div>

    <v-form ref="dataAccessKeyForm" v-model="validation.valid">
      <v-container>
        <v-row class="d-flex align-center">
          <v-col cols="12" md="4">
            <v-text-field v-model="form.newKeyName" :rules="validation.key" :counter="10" label="Key String"
                          required></v-text-field>
          </v-col>
          <v-col cols="12" md="4">
            <v-menu ref="menu" transition="scale-transition" offset-y min-width="auto" :close-on-content-click="false"
                    :return-value.sync="form.expirationDate">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field :rules="validation.expirationDate" readonly v-bind="attrs" v-on="on"
                              v-model="form.expirationDate" label="Expire Date"
                              prepend-icon="mdi-calendar"></v-text-field>
              </template>
              <v-date-picker :min="todayDate" v-model="form.expirationDate" no-title scrollable>
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="menu = false">
                  Cancel
                </v-btn>
                <v-btn text color="primary" @click="$refs.menu.save(form.expirationDate)">
                  OK
                </v-btn>
              </v-date-picker>
            </v-menu>
          </v-col>
          <v-spacer></v-spacer>
          <v-col cols="12" sm="6" md="4">
            <div>
              <v-btn>Add Key</v-btn>
            </div>
          </v-col>
        </v-row>
      </v-container>
    </v-form>
  </div>

</template>

<script>
export default {
  name: "access-keys-admin-panel",
  data: () => ({
    activeKeys: [],
    loading: false,
    form: {
      newKeyName: "",
      expirationDate: ""
    },
    validation: {
      valid: false,
      key: [
        v => !!v || "Key is required!",
        v => v?.length >= 5 || "10 or more characters!"
      ],
      expirationDate: [
        v => !!v || "Duration is required!",
      ],
    },
  }),
  computed: {
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    }
  },
  methods: {
    addKey() {

    }
  }
};
</script>

<style scoped>

</style>
