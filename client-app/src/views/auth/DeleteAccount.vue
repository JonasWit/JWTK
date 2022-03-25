<template>
  
  
  
  
  <button @click="deleteAccount"
          class="w-full portal-button mt-2 text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white">
    Usuń Konto
  </button>
</template>

<script>
import {useStore} from "vuex";
import {useRouter} from "vue-router";
import {removeLocalStoreItem} from "@/appControl/controlFunctions";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";
import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";
import {deleteUserAccount} from "@/services/authAPI";

export default {
  name: "DeleteAccount",
  setup(){
    const store = useStore()
    const router = useRouter()

    async function deleteAccount() {
      try{
        await deleteUserAccount()
        removeLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN)
        store.commit('auth/resetCredentials')
        await store.dispatch('snack/snack', {
          text: "Konto zostało usunięte",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.SUCCESS
        })
        await router.push('/')
      }catch (e){
        await store.dispatch('snack/snack', {
          text: "Błąd usuwania konta",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.ERROR
        })
      }
    }
    return {
      deleteAccount,
    }
  }
}
</script>

<style scoped>

</style>