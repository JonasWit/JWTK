<template>
  <div>
    <input id="password" name="password" type="password" autocomplete="current-password"
           v-model="input"
           class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
           placeholder="Hasło"/>
    <p v-for="er in errors" :key="er"> {{ er }}</p>
  </div>
</template>

<script>
import useInputValidator from "@/validators/useInputValidator";
import {minLength} from "@/validators/appValidators";

export default {
  name: "PasswordAuthInput",
  props: {
    value: String
  },
  setup(props, {emit}) {
    const {input, errors} = useInputValidator(
        props.value,
        [minLength(12)],
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