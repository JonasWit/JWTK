<template>
    <div class="mt-8 mx-4">
        <div
            class="w-full p-3 m-auto bg-white border-t-4 border-customPrimaryVioletDark rounded shadow-lg shadow-purple-800/50 lg:max-w-md">
            <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
                <div class="max-w-md w-full space-y-8">
                    <h1 class="text-3xl font-semibold text-center text-customPrimaryViolet">
                        {{title}}
                    </h1>
                <slot name="formSlot">
                    
                </slot>
                <slot name="bottomContent">

                </slot>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

export default {
    props: {
        title: {
            type: String,
            default: ""
        },
    },
 
}
</script>

<style>

</style>