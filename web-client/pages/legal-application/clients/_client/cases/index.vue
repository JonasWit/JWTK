<template>
  <layout>
    <template v-slot:content>
      <v-container>
        <h1 class="my-2">
          Lista spraw
        </h1>
        <v-expansion-panels class="expansion" focusable>
          <v-expansion-panel v-for="item in groupedCases" :key="item[0].group">
            <v-expansion-panel-header>
              {{ item[0].group }}
            </v-expansion-panel-header>
            <v-expansion-panel-content v-for="object in item" :key="object.id">
              <v-toolbar flat color="primary" dark>
                <v-toolbar-title>User Profile</v-toolbar-title>
              </v-toolbar>
              <v-tabs vertical>
                <v-tab>
                  <v-icon left>
                    mdi-account
                  </v-icon>
                  Option 1
                </v-tab>
                <v-tab>
                  <v-icon left>
                    mdi-lock
                  </v-icon>
                  Option 2
                </v-tab>
                <v-tab>
                  <v-icon left>
                    mdi-access-point
                  </v-icon>
                  Option 3
                </v-tab>

                <v-tab-item>
                  <v-card flat>
                    <v-card-text>
                      <p>
                        Sed aliquam ultrices mauris. Donec posuere vulputate arcu. Morbi ac felis. Etiam feugiat lorem
                        non metus. Sed a libero.
                      </p>

                      <p>
                        Nam ipsum risus, rutrum vitae, vestibulum eu, molestie vel, lacus. Aenean tellus metus, bibendum
                        sed, posuere ac, mattis non, nunc. Aliquam lobortis. Aliquam lobortis. Suspendisse non nisl sit
                        amet velit hendrerit rutrum.
                      </p>

                      <p class="mb-0">
                        Phasellus dolor. Fusce neque. Fusce fermentum odio nec arcu. Pellentesque libero tortor,
                        tincidunt et, tincidunt eget, semper nec, quam. Phasellus blandit leo ut odio.
                      </p>
                    </v-card-text>
                  </v-card>
                </v-tab-item>
                <v-tab-item>
                  <v-card flat>
                    <v-card-text>
                      <p>
                        Morbi nec metus. Suspendisse faucibus, nunc et pellentesque egestas, lacus ante convallis
                        tellus, vitae iaculis lacus elit id tortor. Sed mollis, eros et ultrices tempus, mauris ipsum
                        aliquam libero, non adipiscing dolor urna a orci. Curabitur ligula sapien, tincidunt non,
                        euismod vitae, posuere imperdiet, leo. Nunc sed turpis.
                      </p>

                      <p>
                        Suspendisse feugiat. Suspendisse faucibus, nunc et pellentesque egestas, lacus ante convallis
                        tellus, vitae iaculis lacus elit id tortor. Proin viverra, ligula sit amet ultrices semper,
                        ligula arcu tristique sapien, a accumsan nisi mauris ac eros. In hac habitasse platea dictumst.
                        Fusce ac felis sit amet ligula pharetra condimentum.
                      </p>

                      <p>
                        Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, quis gravida magna mi a
                        libero. Nam commodo suscipit quam. In consectetuer turpis ut velit. Sed cursus turpis vitae
                        tortor. Aliquam eu nunc.
                      </p>

                      <p>
                        Etiam ut purus mattis mauris sodales aliquam. Ut varius tincidunt libero. Aenean viverra rhoncus
                        pede. Duis leo. Fusce fermentum odio nec arcu.
                      </p>

                      <p class="mb-0">
                        Donec venenatis vulputate lorem. Aenean viverra rhoncus pede. In dui magna, posuere eget,
                        vestibulum et, tempor auctor, justo. Fusce commodo aliquam arcu. Suspendisse enim turpis, dictum
                        sed, iaculis a, condimentum nec, nisi.
                      </p>
                    </v-card-text>
                  </v-card>
                </v-tab-item>
                <v-tab-item>
                  <v-card flat>
                    <v-card-text>
                      <p>
                        Fusce a quam. Phasellus nec sem in justo pellentesque facilisis. Nam eget dui. Proin viverra,
                        ligula sit amet ultrices semper, ligula arcu tristique sapien, a accumsan nisi mauris ac eros.
                        In dui magna, posuere eget, vestibulum et, tempor auctor, justo.
                      </p>

                      <p class="mb-0">
                        Cras sagittis. Phasellus nec sem in justo pellentesque facilisis. Proin sapien ipsum, porta a,
                        auctor quis, euismod ut, mi. Donec quam felis, ultricies nec, pellentesque eu, pretium quis,
                        sem. Nam at tortor in tellus interdum sagittis.
                      </p>
                    </v-card-text>
                  </v-card>
                </v-tab-item>
              </v-tabs>

              <v-list>
                <v-list-item>
                  <v-list-item-title>
                    {{ object.name }}
                  </v-list-item-title>
                  <v-list-item-title>
                    {{ object.signature }}
                  </v-list-item-title>
                  <v-list-item-subtitle>
                    {{ object.id }}
                  </v-list-item-subtitle>
                </v-list-item>
                <cases-notes :selected-case="object"/>
              </v-list>

            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </v-container>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {groupByKey} from "@/data/functions";
import CasesNotes from "@/components/legal-app/cases/cases-notes";


export default {
  name: "index",
  components: {CasesNotes, Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],

  data: () => ({

    listOfCases: [],
    name: "",
    signature: "",
    description: "",
    groupedCases: [],
    tab: null,


  }),

  async fetch() {
    try {
      await this.searchListOfCases()
    } finally {
      return this.groupByKey()


    }

  },
  methods: {
    async searchListOfCases() {
      try {
        let listOfCases = await this.$axios.$get(`/api/legal-app-cases/client/${this.$route.params.client}/cases`)
        console.warn('list of cases', listOfCases)
        this.listOfCases = listOfCases

      } catch (e) {
        console.warn('list of cases fetch error', e)
      }

    },

    groupByKey() {
      let input = this.listOfCases;
      let key = 'group';
      const groups = groupByKey(input, key)
      this.groupedCases = groups
      console.log('group by category fired', groups)
    }

  },


}
</script>

<style scoped>
.expansion {
  border-left: 3px solid #B41946 !important;
}

</style>
