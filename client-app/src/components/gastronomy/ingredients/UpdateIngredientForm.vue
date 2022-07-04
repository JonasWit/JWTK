<template>
    <!-- Update ingredient modal -->
    <div>
        <button @click="isModalUpdateOpen = true" class="button m-2">UPDATE
            INGREDIENT</button>
        <transition name="modalUpdate" class="z-50">
            <div class="bg-black fixed top-0 left-0 w-screen h-screen flex justify-center content-center"
                v-if="isModalUpdateOpen">
                <div class="relative bg-white mt-20 pt-12 pb-24 px-12 rounded shadow-2xl w-96 height" ref="modalUpdate">
                    <h1 class="text-3xl font-semibold text-center text-blue-700 mb-4">Update ingredient</h1>
                    <label class="block text-sm text-gray-800">Name</label>
                    <input placeholder="name" v-model="selectedItem.name"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    <label class="block text-sm text-gray-800">description</label>
                    <input placeholder="description" v-model="selectedItem.description"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    <label class="block text-sm text-gray-800">
                        unit
                    </label>
                    <input placeholder="unit" type="number" v-model="selectedItem.measurementUnits"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    <label class="block text-sm text-gray-800">
                        Price
                    </label>
                    <input placeholder="price" type="number" v-model="selectedItem.pricePerStack"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    <label class="block text-sm text-gray-800">
                        Size
                    </label>
                    <input placeholder="size" type="number" v-model="selectedItem.stackSize"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />

                    <div class="absolute bottom-5 right-4 flex gap-4">
                        <button class="bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600"
                            @click="() => update(selectedItem)">Save</button>
                        <button class=" bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600"
                            @click="isModalUpdateOpen = false">Anuluj</button>
                    </div>
                </div>
            </div>
        </transition>
    </div>
</template>

<script>

import { ref } from 'vue';
import { onClickOutside } from '@vueuse/core'
import { useStore } from "vuex";

export default {
    props: {
        selectedIngredient: {
            required: true,
            type: Object,
            default: null
        },
    },

    setup(props) {
        const store = useStore();
        let selectedItem =  ref(props.selectedIngredient)
        const isModalUpdateOpen = ref(false);
        const modalUpdate = ref(null);
   
        onClickOutside(modalUpdate, () => (isModalUpdateOpen.value = false));

        async function update(selectedItem) {
            try {
                console.log("SELECTED item", selectedItem)
                await store.dispatch("gastronomy/updateOneIngredient", selectedItem);
                console.log("updated ITEM", selectedItem);
                isModalUpdateOpen.value = false
            }
            catch (error) {
                console.log("error", error);
            }
        }
        return {
            isModalUpdateOpen,
            modalUpdate,
            onClickOutside,
            selectedItem,
            update
        }
    }
}
</script>

<style>
.modalUpdate-enter-active,
.modalUpdate-leave-active {
    transition: all 0.25s ease;
}

.modalUpdate-enter-from,
.modalUpdate-leave-to

/* .fade-leave-active below version 2.1.8 */
    {
    opacity: 0;
    transform: scale(1.1);
}

.height {
    height: 555px;
}
</style>