<template>
  <div>
    <v-text-field
      v-model="userName"
      label="Nazwa Użytkownika"
      required
    ></v-text-field>
    <v-text-field
      v-model="password"
      label="Hasło"
      required
    ></v-text-field>
    <v-btn color="success" text @click="login">
      Login
    </v-btn>
  </div>
</template>

<script>
import {login} from "~/data/endpoints/auth/auth-endpoints";
import {handleError} from "~/data/functions";

export default {
  name: "index",
  data: () => ({
    userName: '',
    password: '',
  }),
  methods: {
    async login() {
      try {
        const credentials = {
          userName: this.userName,
          password: this.password,
        };
        console.log(`Logging with credentials: ${credentials.userName} - ${credentials.password}`)
        //await this.$axios.$post(login(), credentials);
      } catch (error) {
        handleError(error);
      } finally {

      }
    }
  }

}
</script>

<style scoped>

</style>
