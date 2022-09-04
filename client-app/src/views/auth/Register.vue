<template>
  <div class="mt-8 mx-4">
    <div class="auth-card">
      <div class="auth-card-header">
        <div class="auth-card-content">
          <h1 class="text-3xl font-semibold text-center text-customPrimaryVioletLight">
            Rejestracja
          </h1>
          <form class="mt-6" @submit.prevent="submitForm">
            <div>
              <label class="block text-sm text-gray-800">Email</label>
              <input v-model="state.email" type="email" placeholder="Email"
                class="block w-full px-4 py-2 mt-2 text-customPrimaryVioletLight bg-white border rounded-md focus:border-customSecondaryGold focus:ring-customSecondaryGoldLight focus:outline-none focus:ring focus:ring-opacity-40">
              <div v-if="v$.email.$error">
                <span class="validation-error-span" v-for="error in v$.email.$errors" :key="error.$uid"> {{
                  error.$message
                  }}</span>
              </div>
            </div>
            <div class="mt-4">
              <div class="relative">
                <label class="block text-sm text-gray-800">Hasło</label>
                <input v-model="state.password.password" :type="state.type" placeholder="Hasło"
                  class="block w-full px-4 py-2 mt-2 text-customPrimaryVioletLight bg-white border rounded-md focus:border-customSecondaryGold focus:ring-customSecondaryGoldLight focus:outline-none focus:ring focus:ring-opacity-40">
                <button class="absolute bottom-2 right-2 text-sm" @click="showPassword">{{ state.btnText }}</button>

                <div v-if="v$.password.password.$error">
                  <span class="validation-error-span" v-for="error in v$.password.password.$errors" :key="error.$uid">
                    {{
                    error.$message
                    }}</span>
                </div>
              </div>
              <div class="mt-4 relative">
                <label class="block text-sm text-gray-800">Powtórz hasło</label>
                <input v-model="state.password.confirm" :type="state.type" placeholder="Powtórz hasło"
                  class="block w-full px-4 py-2 mt-2 text-customPrimaryVioletLight bg-white border rounded-md focus:border-customSecondaryGold focus:ring-customSecondaryGoldLight focus:outline-none focus:ring focus:ring-opacity-40">
                <button class="absolute bottom-2 right-2 text-sm" @click="showPassword">{{ state.btnText }}</button>
                <div v-if="v$.password.confirm.$error">
                  <span class="validation-error-span" v-for="error in v$.password.confirm.$errors" :key="error.$uid">
                    {{error.$message}}
                  </span>
                </div>
              </div>
              <div class="mt-4">
                <div class="flex items-center py-2 mt-2">
                  <input v-model="state.rulesAccepted" id="remember-me" name="remember-me" type="checkbox"
                    class="h-4 w-4 accent-pink-500" />
                  <label for="remember-me" class="ml-2 block text-sm text-gray-900">
                    Akceptuję politykę i regulamin
                  </label>
                </div>
                <div v-if="v$.rulesAccepted.$error">
                  <span class="text-xs text-red-900 block" v-for="error in v$.rulesAccepted.$errors" :key="error.$uid">
                    {{
                    error.$message
                    }}</span>
                </div>
              </div>
              <div class="mt-6">
                <button class="button">
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
      type: 'password',
      btnText: 'Pokaż',
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
        },
       
      }
    })

    const v$ = useVuelidate(rules, state)

   function showPassword() {
      if (state.type === 'password') {
        state.type = 'text'
        state.btnText = 'Ukryj'
      } else {
        state.type = 'password'
       state.btnText = 'Pokaż'
      }
}

    async function submitForm() {
      console.log("form submitted")
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
      showPassword,
      state,
      v$
    }
  },
}
</script>

<style scoped>

</style>