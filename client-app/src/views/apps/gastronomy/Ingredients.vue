<template>
  <AccordionTemplate>
    <template v-slot:title>
      <h1>Warzywa</h1>
    </template>
    <template v-slot:content>
      <button class="button" @click="getList">get list</button>
      <div>

      </div>
    </template>
  </AccordionTemplate>
  <AccordionTemplate>
    <template v-slot:title>
      <h1>Owoce</h1>
    </template>
    <template v-slot:content>
      <p>A paragraph for the component.</p>
      <p>And <b>another</b> one.</p>
    </template>
  </AccordionTemplate>
  <AccordionTemplate>
    <template v-slot:title>
      <h1>Napoje</h1>
    </template>
    <template v-slot:content>
      <p>A paragraph for the component.</p>
      <p>And <b>another</b> one.</p>
    </template>
  </AccordionTemplate>
  <AccordionTemplate>
    <template v-slot:title>
      <h1>Produkty zbożowe</h1>
    </template>
    <template v-slot:content>
      <p>A paragraph for the component.</p>
      <p>And <b>another</b> one.</p>
    </template>
  </AccordionTemplate>
  <AccordionTemplate>
    <template v-slot:title>
      <h1>Mięso i Ryby</h1>
    </template>
    <template v-slot:content>
      <p>A paragraph for the component.</p>
      <p>And <b>another</b> one.</p>
    </template>
  </AccordionTemplate>
</template>

<script>
import AccordionTemplate from "@/components/generic/AccordionTemplate";

export default {
  name: "Ingredients",
  components: {AccordionTemplate},
  setup() {

    const state = reactive({
      listOfIngredients: [],
    })

    async function getList() {
      const res = await getIngredients()
      console.log("result:", res)

    }

    return {
      state,
      getList
    }
  }
  // data() {
  //   return {
  //     ListOfIngredients: []
  //   }
  // },
  //
  //
  // methods: {
  //   getList() {
  //     try {
  //       let items = this.getIngredients();
  //       console.log(items)
  //
  //     } catch (e) {
  //       console.warn(e);
  //     }
  //   }
  //
  // }


}
</script>

<style scoped>

</style>