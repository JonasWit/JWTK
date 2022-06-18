<template>
  <div>
    <button @click="isModalOpen=true">Open modal</button>
    <Teleport to="#body">
      <transition name="modal">
          <div class="bg-black fixed top-0 left-0 w-screen h-screen flex justify-center content-center" v-if="isModalOpen">
            <div class="relative bg-white pt-12 pb-24 px-12 rounded shadow-2xl w-96 h-40" ref="modal">
              <button class="bg-indigo-500 text-white px-2 py-1 absolute top-0 right-0 " @click="isModalOpen=false">x</button>
              Click outside this modal to close it.
            </div>
          </div>
      </transition>
    </Teleport>
 </div>
 </template>

<script>
import {ref} from "vue";
import {onClickOutside} from '@vueuse/core'
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

<style>
.modal-enter-active
.modal-leave-active {
transition: all 0.25s ease ;
}

.modal-enter-from
.modal-leave-to {
  opacity: 0;
  transform: scale(1.1);
}


</style>