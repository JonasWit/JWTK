<template>
  <teleport to="body">
    <div class="fixed z-10 inset-0 overflow-y-auto bg-black bg-opacity-50">
      <div class="flex items-start justify-center min-h-screen pt-7 text-center">
        <div class="bg-white rounded-lg text-left overflow-hidden shadow-xl p-5 w-10/12" role="dialog">
          <div class="flex content-center justify-center">
            <h3 class="font-bold mb-3">Wymagana Zgoda</h3>
          </div>
          <div class="text-xs">
            Używamy ciasteczek m.in. w celach: świadczenia usług, statystyk. Korzystanie
            z witryny bez zmiany ustawień Twojej przeglądarki oznacza, że będą one umieszczane w Twoim urządzeniu
            końcowym. 
            Pamiętaj, że zawsze możesz zmienić te ustawienia. 
            Szczegóły znajdziesz w <router-link class="text-blue-700" :to="{ 'name': 'Privacy' }">Polityce Prywatności.</router-link>
          </div>
          <button @click="acceptGDPR" class="w-full portal-button mt-2 text-black bg-yellow-400 md:border-2 p-3">Wyrażam
            Zgodę
          </button>
        </div>
      </div>
    </div>
  </teleport>
</template>

<script>
import {useStore} from "vuex";

export default {
  name: "GdprModal",
  setup() {
    const store = useStore()

    function acceptGDPR() {
      store.dispatch('acceptGDPR')
    }

    return {
      acceptGDPR
    }
  }
}
</script>

<style scoped>

</style>