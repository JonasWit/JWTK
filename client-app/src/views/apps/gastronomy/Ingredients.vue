<template>
<div>
<AccordionTemplate @click="getList">
    <template v-slot:title>
      <h1>Warzywa</h1>
    </template>
    <template v-slot:content>
           
           <div v-for="item in state.listOfIngredients" :key="item.id" class="flex flex-row p-2">
      <div class="p-5 rounded shadow bg-indigo-500 text-white">
        {{item.name}}
      </div>
    </div>
     
        </template>
  </AccordionTemplate>  
 <ModalTemplate>
  <template v-slot:title>
    Add ingredient
  </template>
  <template v-slot:name>
    Name
  </template>
  <template v-slot:description>
    Description
  </template>
  <template v-slot:unit>
    Unit
  </template>
  <template v-slot:price>
    Price
  </template>
  <template v-slot:size>
    Size
  </template>
 </ModalTemplate>
  
</div>
   
</template>

<script>
import AccordionTemplate from "@/components/generic/AccordionTemplate";
import {getIngredients} from "@/services/gastronomyServiceAPI";
import {reactive} from "vue";
import ModalTemplate from "@/components/generic/ModalTemplate";

export default {
  name: "Ingredients",
  components: { AccordionTemplate, ModalTemplate },
  setup() {

    const state = reactive({
      listOfIngredients: [],
      name: 'test'
    })

    async function getList() {
      try {
            const res = await getIngredients()
            state.listOfIngredients = res.data
            console.log("listOfIngredients:",  state.listOfIngredients)
         } catch (error) {
            console.log(error)}
    }

    return {
      state,
      getList
    }
  }
  // data() {
  //   return {
  //     ListOfIngredients: []
  //   }
  // },
  //
  //
  // methods: {
  //   getList() {
  //     try {
  //       let items = this.getIngredients();
  //       console.log(items)
  //
  //     } catch (e) {
  //       console.warn(e);
  //     }
  //   }
  //
  // }


}
</script>

<style scoped>

</style>