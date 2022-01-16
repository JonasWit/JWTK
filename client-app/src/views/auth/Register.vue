<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
        Rejestracja
      </h2>

      <div>
        <input v-model="state.email" type="text" placeholder="Email"/>
        <span v-if="v$.email.$error">
        {{ v$.email.$errors[0].$message }}
      </span>
      </div>
      <div>
        <input v-model="state.password.password"
               class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
               placeholder="Hasło"/>
        <span v-if="v$.password.password.$error">
          {{ v$.password.password.$errors[0].$message }}
        </span>
      </div>

      <div>
        <input v-model="state.password.confirm"
               class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
               placeholder="Powtórz Hasło"/>
        <span v-if="v$.password.confirm.$error">
        {{ v$.password.confirm.$errors[0].$message }}
      </span>
      </div>

      <button @click="submitForm"
              class="w-full portal-button mt-2 text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white">
        Zarejestruj się
      </button>
    </div>
  </div>
</template>

<script>

import {computed, reactive} from "vue";
import {required, email, minLength, sameAs} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";

export default {
  name: "Register",
  components: {},
  setup() {
    const state = reactive({
      email: '',
      password: {
        password: '',
        confirm: '',
      },
    })
    const rules = computed(() => {
      return {
        email: {
          required, 
          email
        },
        password: {
          password: {required, minLength: minLength(3)},
          confirm: {required, sameAs: sameAs(state.password.password)},
        }
      }
    })

    const v$ = useVuelidate(rules, state)

    function submitForm() {
      this.v$.$validate() // checks all inputs
      if (!this.v$.$error) { // if ANY fail validation
        alert('Form successfully submitted.')
      } else {
        alert('Form failed validation')
      }
    }

    return {
      submitForm,
      state,
      v$
    }
  },
}
</script>

<style scoped>

</style>