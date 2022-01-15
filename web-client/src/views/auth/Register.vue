<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
        Rejestracja
      </h2>
      <form>

        
        

        <button type="submit" class="w-full portal-button mt-2 text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white">
          Zarejestruj się
        </button>
      </form>
    </div>
  </div>
</template>

<script>
import {reactive, ref} from "vue";

export default {
  name: "Register",
  components: {},
  setup() {
    const email = ref("");
    const password = ref("");
    const repeatPassword = ref("");
    const errors = reactive({errorsList: []});

    function onSubmit() {
      console.log(`${email.value} ---${password.value} --- ${repeatPassword.value}`)
      console.log("Button clicked")
    }

    return {onSubmit, email, password, repeatPassword, errors};
  },
}
</script>

<style scoped>

</style>