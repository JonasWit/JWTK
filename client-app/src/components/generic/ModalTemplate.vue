<template>
  <div>
    <button @click="isModalOpen=true">Open modal</button>
    <Teleport to="#body">
      <transition name="modal">
          <div class="bg-black fixed top-0 left-0 w-screen h-screen flex justify-center content-center" v-if="isModalOpen">
            <div class="relative bg-white mt-20 pt-12 pb-24 px-12 rounded shadow-2xl w-96 height" ref="modal">
              
                <h1 class="text-3xl font-semibold text-center text-blue-700 mb-4"><slot name="title"></slot></h1>
                
                <label class="block text-sm text-gray-800"><slot name="name"></slot></label>
                <input class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40"/>
                <slot name="name-input"></slot>     

                <label class="block text-sm text-gray-800"><slot name="description"></slot></label>
                <input class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40"/>
                        
                <label class="block text-sm text-gray-800"><slot name="unit"></slot></label>
                <input class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40"/>
                          
                <label class="block text-sm text-gray-800"><slot name="price"></slot></label>
                <input class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40"/>
                          
                <label class="block text-sm text-gray-800"><slot name="size"></slot></label>
                <input class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40"/>

                <div class="absolute bottom-5 right-4 flex gap-4">
                  <button class="bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600" @click="isModalOpen=false">Save</button>
                  <button class="bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600" @click="isModalOpen=false">Anuluj</button>
                </div>
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

.height {
  height: 555px;
}
</style>