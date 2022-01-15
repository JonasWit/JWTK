<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
        Logowanie
      </h2>
      <form>
     
        
        
        
        <button
            class="w-full portal-button mt-2 text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white">
          Zaloguj
        </button>
      </form>
    </div>
  </div>
</template>

<script>

import {ref} from "vue";

export default {
  name: "Login",
  components: {},
  setup() {
    const email = ref("");
    const password = ref("");
    
    const login = () => {
      
      
      
      console.log("login clicked")
    }

    return {login, email, password};
  },
}
</script>

<style scoped>

</style>