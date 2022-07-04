<template>
    <div>
        <button class="button mt-5" @click=" isModalOpen=true">ADD INGREDIENT</button>
        <transition name="modal" class="z-50">
            <div class="bg-black fixed top-0 left-0 w-screen h-screen flex justify-center content-center"
                v-if="isModalOpen">
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
                        <button class="bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600"
                            @click="addIngredient">Save</button>
                        <button class="bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600"
                            @click="isModalOpen = false">Anuluj</button>
                    </div>
                </div>
            </div>
        </transition>
    </div>
</template>

<script>
import { ref, reactive } from 'vue';
import { onClickOutside } from '@vueuse/core'
import { createIngredient } from "../../../services/gastronomyServiceAPI";
import { useStore } from "vuex";
    export default {
    setup() {
            const store = useStore();
            const isModalOpen = ref(false);
            const modal = ref(null);
            const state = reactive([
                { name: "" },
                { description: "" },
                { measurementUnits: null },
                { pricePerStack: null },
                { stackSize: null }
            ]);
           
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
                    await store.dispatch("gastronomy/getAllIngredients");
                    console.log("downloaded");
                    isModalOpen.value = false;

                }
                catch (error) {
                    console.log("error", error);
                }
            }

            return {
                isModalOpen,
                onClickOutside,
                modal,
                addIngredient,
                state,
            }
        }
    }
</script>

<style>

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