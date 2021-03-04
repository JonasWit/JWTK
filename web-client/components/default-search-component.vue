<template>
  <v-autocomplete dense rounded filled hide-details append-icon="" prepend-inner-icon="mdi-magnify" :items="items"
                  :filter="searchFilter">
    <template v-slot:item="{item, on, attrs}">
      <v-list-item v-on="on" :attrs="attrs" :to="item.route">
        <v-list-item-content>{{ item.name }}</v-list-item-content>
        <v-spacer/>
        <v-list-item-content class="justify-end">{{ item.type }}</v-list-item-content>
      </v-list-item>
    </template>
  </v-autocomplete>
</template>

<script>
import {mapState} from "vuex";
import {hasOccurrences} from "@/data/functions";

export default {
  name: "default-search-component",
  methods: {
    searchFilter(item, queryText, itemText) {
      return hasOccurrences(item.searchIndex, queryText);
    }
  },
  computed: {
    items() {

    }
  }
};
</script>

<style scoped></style>
