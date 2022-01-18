<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
        Rejestracja
      </h2>
      <div>
        <input v-model="state.email"
               type="text"
               class="form-input-text-general"
               placeholder="Email"/>
        <div v-if="v$.email.$error">
          <span class="validation-error-span" v-for="error in v$.email.$errors" :key="error.$uid"> {{
              error.$message
            }}</span>
        </div>
      </div>
      <div>
        <input v-model="state.password.password"
               type="password"
               class="form-input-text-general"
               placeholder="Hasło"/>
        <div v-if="v$.password.password.$error">
          <span class="validation-error-span" v-for="error in v$.password.password.$errors" :key="error.$uid"> {{
              error.$message
            }}</span>
        </div>
      </div>
      <div>
        <input v-model="state.password.confirm"
               type="password"
               class="form-input-text-general"
               placeholder="Powtórz Hasło"/>
        <div v-if="v$.password.confirm.$error">
          <span class="validation-error-span" v-for="error in v$.password.confirm.$errors" :key="error.$uid"> {{
              error.$message
            }}</span>
        </div>
      </div>
      <div>
        <div class="flex items-center">
          <input v-model="state.rulesAccepted" id="remember-me" name="remember-me" type="checkbox"
                 class="h-4 w-4 text-indigo-600 focus:ring-customClassicBlue border-gray-300 rounded"/>
          <label for="remember-me" class="ml-2 block text-sm text-gray-900">
            Akceptuję polityke i regulamin
          </label>
        </div>
        <div v-if="v$.rulesAccepted.$error">
          <span class="text-xs text-red-900 block" v-for="error in v$.rulesAccepted.$errors" :key="error.$uid"> {{
              error.$message
            }}</span>
        </div>
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
import {required, email, minLength, sameAs, helpers} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import {register} from "@/services/authAPI";


export default {
  name: "Register",
  components: {},
  setup() {
    const state = reactive({
      email: '',
      rulesAccepted: false,
      password: {
        password: '',
        confirm: '',
      },
    })
    
    const rules = computed(() => {
      return {
        rulesAccepted: {
          sameAs: helpers.withMessage("Pole jest wymagane", sameAs(true))
        },
        email: {
          required: helpers.withMessage("Pole nie może być puste", required),
          email: helpers.withMessage("Nieprawidłowy adres email", email),
        },
        password: {
          password: {
            required: helpers.withMessage("Pole nie może być puste", required),
            minLength: helpers.withMessage("Minimum 12 znaków", minLength(12))
          },
          confirm: {
            required: helpers.withMessage("Pole nie może być puste", required),
            sameAs: helpers.withMessage("Hasła nie są identyczne", sameAs(state.password.password))
          },
        }
      }
    })

    const v$ = useVuelidate(rules, state)
    
    async function submitForm() {
      this.v$.$validate()
      if (this.v$.$error) return;
      
      console.log("submitting")
      
      try {
        const res = await register() 
        console.log(res.data)
        
      }catch (e){
        console.log(e)    
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