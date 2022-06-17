<template>
<div>
   
<form class="mt-6 mx-4">
            <div>
                <slot name="name">
                    <label class="block text-sm text-gray-800"></label>
                                   </slot> 
                <input class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40">
            </div>
           <div class="mt-4">
              <slot name="description">
                    <label class="block text-sm text-gray-800"></label>  </slot>
                    <input class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40">
              
              </div>
</form>

</div>

</template>

<script>
export default {
  name: "ModalTemplate",
 
  
}
</script>

<style scoped>

</style>