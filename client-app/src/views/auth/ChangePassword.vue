<template>
  <div>


    <div>change password</div>
    <button @click="test">
      Test
    </button>


  </div>
</template>

<script>
import {testAuth} from "@/services/devTools";

export default {
  name: "ChangePassword",
  setup() {


    async function test() {
      try {
        const res = await testAuth()
        console.log("data: ", res.data);
        console.log("status: ", res.status);
        console.log("headers: ", res.headers);
      } catch (error) {
        console.log("data: ", error.response.data);
        console.log("status: ", error.response.status);
        console.log("headers: ", error.response.headers);
      }


    }
    return {
      test,
    }
  }
}
</script>

<style scoped>

</style>