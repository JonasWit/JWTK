<template>
  <div class="mt-8 mx-4">
    <div
        class="w-full p-3 m-auto bg-white border-t-4 border-blue-600 rounded shadow-lg shadow-purple-800/50 lg:max-w-md">
      <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div class="max-w-md w-full space-y-8">
          <h1 class="text-3xl font-semibold text-center text-blue-700">
            Przypomnienie Hasła
          </h1>
          <form class="mt-6" @submit.prevent="submitForm">
            <div>
              <label class="block text-sm text-gray-800">
                Email
              </label>
              <input v-model="state.email"
                     class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40"
                     placeholder="Email" type="email"/>
            </div>
            <div class="mt-6">
              <button class="button"
                      @click="submitForm">
                Przypomnij Hasło
              </button>
            </div>
          </form>
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
import useVuelidate from "@vuelidate/core";
import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";
import {resetPasswordRequest} from "@/services/authAPI"

export default {
  name: "ForgotPassword",

  setup() {
    const router = useRouter()

    const state = reactive({
      email: ''
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
        console.log("data: ", res.data);
        console.log("status: ", res.status);
        console.log("headers: ", res.headers);

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

<!--<script>-->
<!--// import {useRouter} from "vue-router";-->
<!--// import {computed, reactive} from "vue/dist/vue";-->
<!--// import {useStore} from "vuex";-->
<!--// import {email, helpers, required} from "@vuelidate/validators";-->
<!--// import useVuelidate from "@vuelidate/core";-->
<!--// import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";-->
<!--// import {resetPasswordRequest} from "@/services/authAPI";-->

<!--export default {-->
<!--  name: "ForgotPassword",-->
<!--  setup() {-->
<!--    // const router = useRouter()-->
<!--    // const state = reactive({-->
<!--    //   email: '',-->
<!--    // })-->
<!--    // const store = useStore()-->
<!--    // const rules = computed(() => {-->
<!--    //   return {-->
<!--    //     email: {-->
<!--    //       required: helpers.withMessage("Pole nie może być puste", required),-->
<!--    //       email: helpers.withMessage("Nieprawidłowy adres email", email),-->
<!--    //     },-->
<!--    //   }-->
<!--    // })-->
<!--    //-->
<!--    // const v$ = useVuelidate(rules, state)-->
<!--    //-->
<!--    // async function submitForm() {-->
<!--    //   this.v$.$validate()-->
<!--    //   if (this.v$.$error) {-->
<!--    //     await store.dispatch('snack/snack', {-->
<!--    //       text: "Niepoprawne dane",-->
<!--    //       textColor: SNACK_TEXT.BLACK,-->
<!--    //       backColor: SNACK_BACKGROUNDS.WARNING-->
<!--    //     })-->
<!--    //     return;-->
<!--    //   }-->
<!--    //-->
<!--    //   try {-->
<!--    //     const payload = {-->
<!--    //       email: state.email,-->
<!--    //     }-->
<!--    //     console.log("credentials: ", payload)-->
<!--    //     const res = await resetPasswordRequest(payload)-->
<!--    //    -->
<!--    //     await store.dispatch('snack/snack', {-->
<!--    //       text: "Email został wysłany",-->
<!--    //       textColor: SNACK_TEXT.BLACK,-->
<!--    //       backColor: SNACK_BACKGROUNDS.SUCCESS-->
<!--    //     })-->
<!--    //     await router.push('/')-->
<!--    //   } catch (error) {-->
<!--    //     if (error.response) {-->
<!--    //       await store.dispatch('snack/snack', {-->
<!--    //         text: "Bład podczas resetu hasła",-->
<!--    //         textColor: SNACK_TEXT.WHITE,-->
<!--    //         backColor: SNACK_BACKGROUNDS.ERROR-->
<!--    //       })-->
<!--    //       console.log("data: ", error.response.data);-->
<!--    //       console.log("status: ", error.response.status);-->
<!--    //       console.log("headers: ", error.response.headers);-->
<!--    //     }-->
<!--    //   }-->
<!--    // }-->

<!--    return {-->
<!--      // submitForm,-->
<!--      // state,-->
<!--      // v$-->
<!--    }-->
<!--  },-->
<!--}-->
<!--</script>-->

<style scoped>

</style>