<template>
  <div>
    <div>
      <button @click="test" class="button mt-5">GET LIST</button>
    <div v-if="listA">
      <li v-for="item in listA" :key="item.id" class="w-45">
        <ul class="border p-9">
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
     <!--Add ingredient modal  -->
    <button class="button mt-5" @click=" isModalOpen=true">ADD INGREDIENT</button>
    <transition name="modal" class="z-50">
      <div class="bg-black fixed top-0 left-0 w-screen h-screen flex justify-center content-center" v-if="isModalOpen">
        <div class="relative bg-white mt-20 pt-12 pb-24 px-12 rounded shadow-2xl w-96 height" ref="modal">
          <h1 class="text-3xl font-semibold text-center text-blue-700 mb-4">Add ingredient</h1>
          <label class="block text-sm text-gray-800">Name</label>
          <input placeholder="name" v-model="state.name"
            class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
          <label class="block text-sm text-gray-800">description</label>
          <input placeholder="description" v-model="state.description"
            class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
          <label class="block text-sm text-gray-800">
            unit
          </label>
          <input placeholder="unit" v-model="state.measurementUnits" type="number"
            class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
          <label class="block text-sm text-gray-800">
            Price
          </label>
          <input placeholder="price" v-model="state.pricePerStack" type="number"
            class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
          <label class="block text-sm text-gray-800">
            Size
          </label>
          <input placeholder="size" v-model="state.stackSize" type="number"
            class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />

          <div class="absolute bottom-5 right-4 flex gap-4">
            <button class="bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600" @click="addIngredient">Save</button>
            <button class="bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600"
              @click="isModalOpen=false">Anuluj</button>
          </div>
        </div>
      </div>
    </transition>
   

  </div>
</template>

<script>
import {useStore} from "vuex";
import {computed, reactive} from "vue";
import { ref } from "vue";
import { onClickOutside } from '@vueuse/core'
import { createIngredient} from "../../../services/gastronomyServiceAPI";
import UpdateForm from "../../../components/generic/UpdateForm.vue";

export default {
    name: "Ingredients",
    setup() {
        const isModalOpen = ref(false);
        const modal = ref(null);
        const store = useStore();
        const state = reactive([
            { name: "" },
            { description: "" },
            { measurementUnits: null },
            { pricePerStack: null },
            { stackSize: null }
        ]);
        async function test() {
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
                test();
            }
        }
        onClickOutside(modal, () => (isModalOpen.value = false));
        async function addIngredient() {
            try {
                let payload = {
                    name: state.name,
                    description: state.description,
                    measurementUnits: state.measurementUnits,
                    pricePerStack: state.pricePerStack,
                    stackSize: state.stackSize
                };
                console.log("payload", payload);
                await createIngredient(payload);
                isModalOpen.value = false;
            }
            catch (error) {
                console.log("error", error);
            }
        }
        return {
            listA: computed(() => store.getters["gastronomy/listOfingredients"]),
            test,
            remove,
            isModalOpen,
            onClickOutside,
            modal,
            addIngredient,
            state,
        };
    },
    components: { UpdateForm }
}
</script>

<style scoped>
.modal-enter-active,
.modal-leave-active {
  transition: all 0.25s ease;
}

.modal-enter-from,
.modal-leave-to

/* .fade-leave-active below version 2.1.8 */
  {
  opacity: 0;
  transform: scale(1.1);
}

.height {
  height: 555px;
}
</style>