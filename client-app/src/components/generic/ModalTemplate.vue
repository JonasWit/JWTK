<template>
  <div>
    <button @click="isModalOpen=true">Click me to open modal</button>
    <div class="bg-zinc-100 opacity-10 fixed top-0 left-0 w-screen h-screen flex justify-center content-center" v-if="isModalOpen">
      <div class="relative bg-white pt-12 pb-24 px-12 rounded shadow-2xl w-96 h-40" ref="modal">
        <button class="button" @click="isModalOpen=false">close me</button>
        Click outside this modal to close it.
      </div>
    </div>
  </div>
 </template>

<script>
import {ref} from "vue";
import {onClickOutside} from 'vueuse/core'
export default {
  name: "ModalTemplate",

  setup () {
   const isModalOpen = ref(false)
   const modal = ref(null)

   onClickOutside(modal, () => (isModalOpen.value = false))
    return {
        isModalOpen,
        modal,
        onClickOutside
      } 
    }
  
  
}
</script>

<style scoped>

</style>