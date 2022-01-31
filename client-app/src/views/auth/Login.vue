<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
        Logowanie
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
        <input v-model="state.password"
               type="password"
               class="form-input-text-general"
               placeholder="Hasło"/>
      </div>
      <button @click="submitForm"
              class="w-full portal-button mt-2 text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white">
        Zaloguj
      </button>
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
            text: "Logowanie nie powiodło się",
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