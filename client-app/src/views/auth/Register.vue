<template>
  <div class="mt-8">
    <div class="w-full p-3 m-auto bg-white border-t-4 border-blue-600 rounded shadow-lg shadow-purple-800/50 lg:max-w-md">
      <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div class="max-w-md w-full space-y-8">
          <h1 class="text-3xl font-semibold text-center text-blue-700">
            Rejestracja
          </h1>
          <form class="mt-6">
            <div>
              <label class="block text-sm text-gray-800">Email</label>
              <input v-model="state.email" type="email" placeholder="Email"
                     class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40">
              <div v-if="v$.email.$error">
          <span class="validation-error-span" v-for="error in v$.email.$errors" :key="error.$uid"> {{
              error.$message
            }}</span>
              </div>
            </div>
            <div class="mt-4">
              <div>
                <label  class="block text-sm text-gray-800">Hasło</label>
                <input v-model="state.password.password" type="password" placeholder="Hasło"
                       class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40">
                <div v-if="v$.password.password.$error">
          <span class="validation-error-span" v-for="error in v$.password.password.$errors" :key="error.$uid"> {{
              error.$message
            }}</span>
                </div>
              </div>
              <div class="mt-4">
                <label class="block text-sm text-gray-800">Powtórz hasło</label>
                <input v-model="state.password.confirm" type="password" placeholder="Powtórz hasło"
                       class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40">
                <div v-if="v$.password.confirm.$error">
                <span class="validation-error-span" v-for="error in v$.password.confirm.$errors" :key="error.$uid"> 
                  {{error.$message}}
                </span>
                </div>
              </div>
              <div class="mt-4">
                <div class="flex items-center py-2 mt-2">
                  <input v-model="state.rulesAccepted" id="remember-me" name="remember-me" type="checkbox"
                         class="h-4 w-4 text-indigo-600 focus:ring-customClassicBlue border-gray-300 rounded"/>
                  <label for="remember-me" class="ml-2 block text-sm text-gray-900">
                    Akceptuję politykę i regulamin
                  </label>
                </div>
                <div v-if="v$.rulesAccepted.$error">
          <span class="text-xs text-red-900 block" v-for="error in v$.rulesAccepted.$errors" :key="error.$uid"> {{
              error.$message
            }}</span>
                </div>
              </div>               
              <div class="mt-6">
                <button @click="submitForm"
                        class="button">
                  Zarejestruj
                </button>
              </div>
            </div>
          </form>         
        </div>
      </div>
    </div>
  </div>

</template>

<script>

import {computed, reactive} from "vue";
import {required, email, minLength, sameAs, helpers} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import {register} from "@/services/authAPI";
import {useStore} from "vuex";
import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";
import {useRouter} from "vue-router";

export default {
  name: "Register",
  components: {},
  setup() {
    const router = useRouter()
    
    const state = reactive({
      email: '',
      rulesAccepted: false,
      password: {
        password: '',
        confirm: '',
      },
    })

    const store = useStore()
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
      if (this.v$.$error) {
        await store.dispatch('snack/snack', {
          text: "Niepoprawne dane",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.WARNING
        })
        return;
      }

      try {
        const payload = {
          email: state.email, 
          password: state.password.password
        }
        console.log("credentials: ", payload)
        const res = await register(payload)
        console.log("data: ", res.data);
        console.log("status: ", res.status);
        console.log("headers: ", res.headers);
        
        await store.dispatch('snack/snack', {
          text: "Rejestracja powiodła się",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.SUCCESS
        })
        await router.push('/')
      } catch (error) {
        if (error.response) {
          await store.dispatch('snack/snack', {
            text: "Rejestracja nie powiodła się",
            textColor: SNACK_TEXT.WHITE,
            backColor: SNACK_BACKGROUNDS.ERROR
          })
          console.log("data: ", error.response.data);
          console.log("status: ", error.response.status);
          console.log("headers: ", error.response.headers);
        }
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