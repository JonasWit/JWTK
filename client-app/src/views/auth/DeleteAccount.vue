<template>
  <div class="mt-8 mx-4">
    <div
        class="w-full p-3 m-auto bg-white border-t-4 border-blue-600 rounded shadow-lg shadow-purple-800/50 lg:max-w-md">
      <div class="max-w-md w-full space-y-8">
        <h1 class="text-3xl font-semibold text-center text-blue-700 mt-2">
          Czy napewno chcesz usunąć swoje konto?
        </h1>
        <div class="flex flex justify-evenly">
          <div>
            <button class="button" @click="deleteAccount">
              Usuń Konto
            </button>
          </div>
          <div>
            <button class="button">
              Anuluj
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
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
  setup() {
    const store = useStore()
    const router = useRouter()

    async function deleteAccount() {
      try {
        await deleteUserAccount()
        removeLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN)
        store.commit('auth/resetCredentials')
        await store.dispatch('snack/snack', {
          text: "Konto zostało usunięte",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.SUCCESS
        })
        await router.push('/')
      } catch (e) {
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