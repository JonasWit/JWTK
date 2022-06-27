<template>
    <div>
      <AddIngredientForm></AddIngredientForm>
      <button @click="getList" class="button mt-5">GET LIST</button>
    <div v-if="listA">
      <li v-for="item in listA" :key="item.id" class="w-45">
        <ul class="border p-9">
          <p>ID: {{item.id}}</p>
          <p>Name:{{item.name}}</p>
          <p>Description:{{item.description}}</p>
          <p>Measurement Unit: {{item.measurementUnits}}</p>
          <p>Price Stack:{{item.pricePerStack}}</p>
          <p>Stack Size: {{item.stackSize}}</p>
          <button @click="() => remove(item.id)" class="button m-2">REMOVE
            INGREDIENT</button>
          <UpdateForm :item="item"></UpdateForm>
        </ul>
      </li>
    </div>
        </div>    
</template>

<script>
import {useStore} from "vuex";
import {computed} from "vue";
import UpdateForm from "../../../components/generic/UpdateForm.vue";
import AddIngredientForm from "../../../components/gastronomy/ingredients/AddIngredientForm.vue";


export default {
    name: "Ingredients",
    setup() {
      const store = useStore();
        
      async function getList() {
            try {
                await store.dispatch("gastronomy/getAllIngredients");
                console.log("downloaded");
            }
            catch (error) {
                console.log("error", error);
            }
        }
      async function remove(id) {
            try {
                await store.dispatch("gastronomy/deleteOneIngredient", id);
                console.log("removed");
            }
            catch (error) {
                console.log("error", error);
            }
            finally {
                getList();
            }
        }
        
        return {
            listA: computed(() => store.getters["gastronomy/listOfingredients"]),
            getList,
            remove,
            
        };
    },
    components: { UpdateForm, AddIngredientForm }
}
</script>

<style scoped>
</style>