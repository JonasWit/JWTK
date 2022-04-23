<template>
  <div class="mt-8">
    <div class="w-full p-3 m-auto bg-white border-t-4 border-blue-600 rounded shadow-lg shadow-purple-800/50 lg:max-w-md">
      <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div class="max-w-md w-full space-y-8">
          <h1 class="text-3xl font-semibold text-center text-blue-700">
            Logowanie
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
                 <input v-model="state.password" type="password" placeholder="Hasło"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40">
               </div>
               <a href="#" class="text-xs text-gray-600 hover:underline">Zapomniałeś hasła?</a>
               <div class="mt-6">
                 <button @click="submitForm"
                     class="button">
                   Zaloguj
                 </button>
               </div>
             </div>
           </form>
           <p class="mt-8 text-xs font-light text-center text-gray-700"> Nie masz konta? 
             <a href="#" class="font-medium text-blue-600 hover:underline">Zarejestruj się</a></p>
</div>
</div>
</div>
  </div>   
</template>

<script>
import {useRouter} from "vue-router";
import {computed, reactive} from "vue";
import {useStore} from "vuex";
import {email, helpers, required} from "@vuelidate/validators";
import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";
import {authenticate} from "@/services/authAPI";
import useVuelidate from "@vuelidate/core";

export default {
  name: "Login",
  components: {},
  setup() {
    const router = useRouter()

    const state = reactive({
      email: '',
      password: ''
    })
    //
    const store = useStore()
    const rules = computed(() => {
      return {
        email: {
          required: helpers.withMessage("Pole nie może być puste", required),
          email: helpers.withMessage("Nieprawidłowy adres email", email),
        },
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
          password: state.password
        }
        console.log("credentials: ", payload)
        const res = await authenticate(payload)
        console.log("data: ", res.data);
        console.log("status: ", res.status);
        console.log("headers: ", res.headers);

        const token = res.data.token
        console.log("Passing token to store: ", token)
        await store.dispatch('auth/setIdToken', token)
        await store.dispatch('snack/snack', {
          text: "Logowanie powiodło się",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.SUCCESS
        })
        await router.push('/')
      } catch (error) {
        if (error.response) {
          await store.dispatch('snack/snack', {
            text: "Logowanie nie powiodło się lub przekroczono limit prób. Spróbuj ponownie poźniej",
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