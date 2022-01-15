<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <form novalidate @submit.prevent="login">
      <EmailAuthInput v-model="email" />
      <PasswordAuthInput v-model="password" />
      <button class="portal-button" type="submit">Logowanie</button>
    </form>
  </div>
</template>

<script>

import {ref} from "vue";
import EmailAuthInput from "@/components/inputs/EmailAuthInput";
import PasswordAuthInput from "@/components/inputs/PasswordAuthInput";

export default {
  name: "Login",
  components: {PasswordAuthInput, EmailAuthInput},
  setup() {
    const email = ref("");
    const password = ref("");
    
    const login = () => { console.log("login clicked")}

    return { login, email, password };
  },
}
</script>

<style scoped>

</style>