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
                    <input placeholder="name" v-model="item.name"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    <label class="block text-sm text-gray-800">description</label>
                    <input placeholder="description" v-model="item.description"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    <label class="block text-sm text-gray-800">
                        unit
                    </label>
                    <input placeholder="unit" v-model="item.measurementUnits" type="number"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    <label class="block text-sm text-gray-800">
                        Price
                    </label>
                    <input placeholder="price" v-model="item.pricePerStack" type="number"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />
                    <label class="block text-sm text-gray-800">
                        Size
                    </label>
                    <input placeholder="size" v-model="item.stackSize" type="number"
                        class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white border rounded-md focus:border-blue-400 focus:ring-blue-300 focus:outline-none focus:ring focus:ring-opacity-40" />

                    <div class="absolute bottom-5 right-4 flex gap-4">
                        <!-- <button class="bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600"
                            @click="() => update(item)">Save</button> -->
                        <button class=" bg-indigo-500 text-white px-4 py-2 hover:bg-indigo-600"
                            @click="isModalUpdateOpen = false">Anuluj</button>
                    </div>
                </div>
            </div>
        </transition>
    </div>
</template>

<script>
import { ref } from "vue";
// import { useStore } from "vuex";

export default {
    // props: {
    //     item: Object,
    // },
    setup() {
        const isModalUpdateOpen = ref(false);
        const modalUpdate = ref(null);

        // const store = useStore();

        // async function update(item) {
        //     try {
        //         console.log("item", item)
        //         await store.dispatch("gastronomy/updateOneIngredient", item);
        //         console.log("updated", item);
        //         isModalUpdateOpen.value = false
        //     }
        //     catch (error) {
        //         console.log("error", error);
        //     }
        // }
        return {
            isModalUpdateOpen,
            modalUpdate,
            // update
        }
    }
}
</script>

<style>

</style>