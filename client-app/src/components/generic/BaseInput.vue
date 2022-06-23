<template>
<div>
    <label>{{label}}</label>
    <input :placeholder="label"
    :value="modelValue"
    @input="$emit('update:modelValue', $event.target.value )"
    class="block w-full px-4 py-2 mt-2 text-blue-700 bg-white 
    border rounded-md focus:border-blue-400 focus:ring-blue-300 
    focus:outline-none focus:ring focus:ring-opacity-40"
    />  
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