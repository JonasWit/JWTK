<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
        Reset Hasła
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
      <button @click="submitForm"
              class="w-full portal-button mt-2 text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white">
        Zresetuj Hasło
      </button>
    </div>
  </div>
</template>

<script>
import {useRouter} from "vue-router";
import {computed, reactive} from "vue/dist/vue";
import {useStore} from "vuex";
import {email, helpers, required} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";
import {resetPasswordRequest} from "@/services/authAPI";

export default {
  name: "ForgotPassword",
  setup() {
    const router = useRouter()
    const state = reactive({
      email: '',
    })
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
        }
        console.log("credentials: ", payload)
        const res = await resetPasswordRequest(payload)
        
        await store.dispatch('snack/snack', {
          text: "Email został wysłany",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.SUCCESS
        })
        await router.push('/')
      } catch (error) {
        if (error.response) {
          await store.dispatch('snack/snack', {
            text: "Bład podczas resetu hasła",
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