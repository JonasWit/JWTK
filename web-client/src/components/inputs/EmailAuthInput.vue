<template>
  <div>
    <input id="email-address" name="email" type="email" autocomplete="email"
           v-model="input"
           class="relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
           placeholder="Email"/>
    <p v-for="er in errors" :key="er"> {{ er }}</p>
  </div>
</template>

<script>
import useInputValidator from "@/validators/useInputValidator";
import {isEmail} from "@/validators/appValidators";

export default {
  name: "EmailAuthInput",
  props: {
    value: String
  },
  setup(props, {emit}) {
    const {input, errors} = useInputValidator(
        props.value,
        [isEmail()],
        value => emit("input", value)
    );
    return {
      input,
      errors
    }
  }
}
</script>

<style scoped>

</style>