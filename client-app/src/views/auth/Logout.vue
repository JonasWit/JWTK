<template>
  <button @click="logout"
          class="w-full portal-button mt-2 text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white">
    Wyloguj z Aplikacji
  </button>
</template>

<script>
import {useStore} from "vuex";
import {removeLocalStoreItem} from "@/appControl/controlFunctions";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";
import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";
import {useRouter} from "vue-router";

export default {
  name: "Logout",
  setup(){
    const store = useStore()
    const router = useRouter()

    function logout() {
      try{
        removeLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN)
        store.commit('auth/resetCredentials')
        store.dispatch('snack/snack', {
          text: "Pomyślnie wylogowano",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.SUCCESS
        })
        router.push('/')
      }catch (e){
        store.dispatch('snack/snack', {
          text: "Błąd podczas wylogowywania",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.ERROR
        })      
      }
    }
    return {
      logout,
    }
  }
}
</script>

<style scoped>

</style>