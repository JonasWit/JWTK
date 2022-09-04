<template>
    <div>
        <label class="block text-sm text-gray-800">{{label}}</label>
        <input 
        v-bind="$attrs"
        :placeholder="label" 
        :value="modelValue" 
        @input="$emit('update:modelValue', $event.target.value )"
        class="block w-full px-4 py-2 mt-2 text-customPrimaryVioletLight bg-white border rounded-md 
        focus:border-customSecondaryGold focus:ring-customSecondaryGoldLight focus:outline-none 
        focus:ring focus:ring-opacity-40" />
    </div>
</template>

<script>
export default {
props: {
    label: {
        type: String,
        default: ''
    },
    modelValue: {
        type: [String, Number],
        default: ''
    }
}
}
</script>

<style>

</style>