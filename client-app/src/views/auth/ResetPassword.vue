<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
        Zmiana Hasła
      </h2>
      <div>
        <input v-model="state.newPassword.newPassword"
               class="form-input-text-general"
               placeholder="Nowe Hasło"
               type="password"/>
        <div v-if="v$.newPassword.newPassword.$error">
          <span v-for="error in v$.newPassword.newPassword.$errors" :key="error.$uid" class="validation-error-span"> {{
              error.$message
            }}</span>
        </div>
      </div>
      <div>
        <input v-model="state.newPassword.confirm"
               class="form-input-text-general"
               placeholder="Powtórz Nowe Hasło"
               type="password"/>
        <div v-if="v$.newPassword.confirm.$error">
          <span v-for="error in v$.newPassword.confirm.$errors" :key="error.$uid" class="validation-error-span"> {{
              error.$message
            }}</span>
        </div>
      </div>
      <button
          class="w-full portal-button mt-2 text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white"
          @click="submitForm">
        Zmień Hasło
      </button>
    </div>
  </div>
</template>

<script>

import {useRouter} from "vue-router";
import {useStore} from "vuex";
import {helpers, minLength, required, sameAs} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";
import {computed, onMounted, reactive} from "vue";
import {resetPasswordAction} from "@/services/authAPI";

export default {
  name: "ChangePassword",
  setup() {
    const router = useRouter()
    const state = reactive({
      newPassword: {
        newPassword: '',
        confirm: '',
      },
    })

    onMounted(() => {
      console.warn("target: ", router.currentRoute.value.query.target);
      console.warn("token: ", router.currentRoute.value.query.token);
    })

    const store = useStore()
    const rules = computed(() => {
      return {
        newPassword: {
          newPassword: {
            required: helpers.withMessage("Pole nie może być puste", required),
            minLength: helpers.withMessage("Minimum 12 znaków", minLength(12))
          },
          confirm: {
            required: helpers.withMessage("Pole nie może być puste", required),
            sameAs: helpers.withMessage("Hasła nie są identyczne", sameAs(state.newPassword.newPassword))
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
      }

      try {
        const payload = {
          password: state.newPassword.newPassword,
          token: router.currentRoute.value.query.token
        }

        console.log("credentials: ", payload)
        const res = await resetPasswordAction(payload)
        console.log("data: ", res.data);
        console.log("status: ", res.status);
        console.log("headers: ", res.headers);

        await store.dispatch('snack/snack', {
          text: "Hasło zmienione",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.SUCCESS
        })

        await store.commit('auth/resetCredentials')
        await router.push('/')

      } catch (error) {
        if (error.response) {
          await store.dispatch('snack/snack', {
            text: "Zmiana hasła nie powiodła się",
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