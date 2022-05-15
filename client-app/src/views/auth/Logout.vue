<template>
  <div class="mt-8 mx-4">
    <div
        class="w-full p-3 m-auto bg-white border-t-4 border-blue-600 rounded shadow-lg shadow-purple-800/50 lg:max-w-md">
      <div class="max-w-md w-full space-y-8">
        <h1 class="text-3xl font-semibold text-center text-blue-700 mt-2">
          Czy napewno chcesz się wylogować z aplikacji?
        </h1>
        <div class="flex flex justify-evenly">
          <div>
            <button class="button"
                    @click="logout">
              Wyloguj
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
import {removeLocalStoreItem} from "@/appControl/controlFunctions";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";
import {SNACK_BACKGROUNDS, SNACK_TEXT} from "@/models/enums";
import {useRouter} from "vue-router";

export default {
  name: "Logout",
  setup() {
    const store = useStore()
    const router = useRouter()

    function logout() {
      try {
        removeLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN)
        store.commit('auth/resetCredentials')
        store.dispatch('snack/snack', {
          text: "Pomyślnie wylogowano",
          textColor: SNACK_TEXT.BLACK,
          backColor: SNACK_BACKGROUNDS.SUCCESS
        })
        router.push('/')
      } catch (e) {
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