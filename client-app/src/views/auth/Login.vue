<template>
  <div class="mt-8 mx-4">
    <div
      class="w-full p-3 m-auto bg-white border-t-4 border-customPrimaryVioletDark rounded shadow-lg shadow-purple-800/50 lg:max-w-md">
      <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div class="max-w-md w-full space-y-8">
          <h1 class="text-3xl font-semibold text-center text-customPrimaryViolet">
            Logowanie
          </h1>
          <form class="mt-6" @submit.prevent="submitForm">
            <div>
              <BaseInput v-model="state.email" label="Email" type="email" />
              <div v-if="v$.email.$error">
                <span class="validation-error-span" v-for="error in v$.email.$errors" :key="error.$uid"> {{
                error.$message
                }}</span>
              </div>
            </div>
            <div class="mt-4">
              <div>
                <BaseInput v-model="state.password" label="Hasło" type="password" />
                

              </div>
              <ForgotPasswordRouterLink />
              <div class="mt-6">
                <button @click="submitForm" class="button">
                  Zaloguj
                </button>
              </div>
            </div>
          </form>
          <p class="mt-8 text-xs font-light text-center text-gray-700"> Nie masz konta?
            <router-link to="/auth/register" class="font-medium text-customPrimaryVioletLight hover:underline">
              Zarejestruj się
            </router-link>
          </p>
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
import ForgotPasswordRouterLink from "@/components/portal/ForgotPasswordRouterLink";
import BaseInput from "../../components/generic/BaseInput.vue";

export default {
  name: "Login",
  components: { ForgotPasswordRouterLink, BaseInput },
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