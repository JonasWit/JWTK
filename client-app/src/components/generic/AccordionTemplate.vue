<template>
  <div class="accordion-item bg-white border border-gray-200">
    <button
        :aria-controls="`collapse${_uid}`"
        :aria-expanded="isOpen"
        class="relative flex items-center w-full py-4 px-5 text-base text-white text-left bg-blue-700 border-0 rounded-none transition focus:outline-none"
        @click="toggleAccordion()"
    >
      <svg
          :class="{
          'rotate-180': isOpen,
          'rotate-0': !isOpen,
        }"
          aria-hidden="true"
          class="mr-5 w-3 transition-all duration-200 transform"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 16 10"
          xmlns="http://www.w3.org/2000/svg"
      >
        <path
            d="M15 1.2l-7 7-7-7"
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
        />
      </svg>
      <slot name="title"/>
    </button>
    <div v-show="isOpen" :id="`collapse${_uid}`" class="accordion-body py-4 px-5 flex items-center">
      <slot name="content"/>
    </div>
  </div>
</template>

<script>
export default {
  name: "AccordionTemplate",
  data() {
    return {
      isOpen: false,
    };
  },
  methods: {
    toggleAccordion() {
      this.isOpen = !this.isOpen;
    },
  },
}
</script>

<style scoped>

</style>