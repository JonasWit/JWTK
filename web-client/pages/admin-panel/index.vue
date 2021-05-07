<template>
  <v-card>
    <v-toolbar>
      <v-tabs v-model="tab" centered>
        <v-tab v-for="(t, i) in tabs" :key="`portal-admin-tab-${i}`">
          {{ t.title }}
        </v-tab>
      </v-tabs>
    </v-toolbar>
    <v-tabs-items v-model="tab">
      <v-tab-item v-for="(t, i) in tabs" :key="`portal-admin-tab-body-${i}`">
        <component class="ma-3" :is="t.component"/>
      </v-tab-item>
    </v-tabs-items>
  </v-card>
</template>

<script>
import UsersAdminPanel from "@/components/portal-admin/users-management/user-admin-panel";
import AccessKeysPanel from "@/components/portal-admin/access-keys/access-keys-admin-panel";
import LogManagerPanel from "@/components/portal-admin/log-manager/portal-log-manager-panel";
import LogAppPanel from "@/components/portal-admin/log-manager/api-log-manager-panel";

export default {
  middleware: ["portal-admin"],
  data: () => ({
    tab: 0,
    tabs: [
      {title: "Users", component: UsersAdminPanel},
      {title: "Access Keys", component: AccessKeysPanel},
      {title: "Portal Logs", component: LogManagerPanel},
      {title: "App Logs", component: LogAppPanel},
    ]
  })
};
</script>

<style scoped>

</style>
