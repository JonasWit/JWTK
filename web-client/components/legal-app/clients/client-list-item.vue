<template>
  <v-expansion-panels>
    <v-expansion-panel class="expansion">
      <v-expansion-panel-header>Nazwa: {{ clientItem.name }}
        <template v-slot:actions>
          <v-icon color="primary">
            $expand
          </v-icon>
        </template>
      </v-expansion-panel-header>
      <v-expansion-panel-content>
        <v-card class="my-5">
          <v-tabs vertical>
            <v-tab class="d-flex justify-start">
              <v-icon left>
                mdi-account-box
              </v-icon>
              Dane podstawowe
            </v-tab>
            <v-tab class="d-flex justify-start">
              <v-icon left>
                mdi-folder-lock
              </v-icon>
              Panel dostępów
            </v-tab>
            <v-tab-item>
              <v-card flat>
                <v-card-text>
                  <v-row class="align-center">
                    <v-col class="mx-2">
                      <v-list class="d-flex justify-space-between">
                        <v-list-item-content>
                          <v-list-item-title> Nazwa:{{ clientItem.id }} {{ clientItem.name }}</v-list-item-title>
                        </v-list-item-content>
                      </v-list>
                    </v-col>
                    <v-col class="mx-2">
                      <v-list class="d-flex justify-space-between">
                        <v-list-item-content>
                          <v-list-item-subtitle>Dodano: {{ formatDate(clientItem.created) }}</v-list-item-subtitle>
                          <v-list-item-subtitle>Dodane przez: {{ clientItem.createdBy }}</v-list-item-subtitle>
                          <v-list-item-subtitle>Edytowano: {{ formatDate(clientItem.updated) }}</v-list-item-subtitle>
                          <v-list-item-subtitle>Edytowane przez: {{ clientItem.updatedBy }}</v-list-item-subtitle>
                        </v-list-item-content>
                      </v-list>
                    </v-col>
                    <v-col class="mx-2">
                      <v-list class="d-flex justify-md-end justify-sm-space-between">
                        <delete-client-dialog :selected-client="clientItem"/>
                        <archive-client-dialog :selected-client="clientItem"/>
                        <edit-client-name-dialog :selected-client="clientItem"/>
                        <go-to-client-panel :client-item="clientItem"/>
                      </v-list>
                    </v-col>
                  </v-row>
                </v-card-text>
              </v-card>
            </v-tab-item>
            <v-tab-item>
              <v-card flat>
                <allowed-users :client-item-for-action="clientItem"/>
                <grant-access :client-item="clientItem"/>
              </v-card>
            </v-tab-item>
          </v-tabs>
        </v-card>
      </v-expansion-panel-content>
    </v-expansion-panel>
  </v-expansion-panels>
</template>

<script>

import DeleteClientDialog from "@/components/legal-app/clients/dialogs/delete-client-dialog";
import ArchiveClientDialog from "@/components/legal-app/clients/dialogs/archive-client-dialog";
import GoToClientPanel from "@/components/legal-app/clients/go-to-client-panel";
import EditClientNameDialog from "@/components/legal-app/clients/dialogs/edit-client-name-dialog";
import {formatDate} from "@/data/date-extensions";
import AllowedUsers from "@/components/legal-app/clients/accesses-panel/allowed-users";
import GrantAccess from "@/components/legal-app/clients/accesses-panel/grant-access";


export default {
  name: "client-list-item",
  components: {
    GrantAccess, AllowedUsers, EditClientNameDialog, GoToClientPanel, ArchiveClientDialog, DeleteClientDialog
  },
  props: {
    clientItem: {
      type: Object,
      required: true
    }
  },
  methods: {
    formatDate(date) {
      return formatDate(date)
    }
  },
};
</script>

<style scoped>

</style>
