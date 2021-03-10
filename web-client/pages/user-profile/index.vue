<template>
  <div>
    <v-card>
      <v-img height="250" src="">
        <v-row align="center" class="d-flex">
          <div class="pl-5">
            <input class="d-none" type="file" accept="image/*" ref="profileImageInput" @change="changeProfileImage"/>
            <v-hover v-slot:default="{hover}">
              <v-avatar>
                <v-btn :disabled="uploadingImage" icon v-if="hover" @click="$refs.profileImageInput.click()">
                  <v-icon>mdi-account-edit</v-icon>
                </v-btn>
                <img v-else-if="profile.image" :src="profile.image" alt="profile image"/>
                <v-icon x-large v-else>mdi-account</v-icon>
              </v-avatar>
            </v-hover>
          </div>
          <div>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="title">
                  {{ profile.username }}
                </v-list-item-title>
                <v-list-item-subtitle>{{ userRole }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </div>
          <v-spacer/>
          <div class="pa-5">
            <h1>test</h1>
          </div>
        </v-row>
      </v-img>
    </v-card>
    <div>
      <h3 class="text-h4 text-center my-2">Dostępne Aplikacje</h3>

    </div>

  </div>
</template>

<script>
import {mapActions, mapGetters, mapState} from "vuex";

export default {
  middleware: ["client"],
  data: () => ({
    uploadingImage: false,
  }),
  computed: {
    ...mapState('auth', ['profile']),
    ...mapGetters('auth', ['userRole'])
  },
  methods: {
    ...mapActions('auth', ['initialize']),
    changeProfileImage(e) {
      if (this.uploadingImage) return;
      this.uploadingImage = true;

      const fileInput = e.target;
      const formData = new FormData;

      formData.append('image', fileInput.files[0]);

      return this.$axios.$put('/api/users/me/image', formData)
        .then(() => {
          this.initialize();
          fileInput.value = "";
          this.uploadingImage = false;
        });
    },
  }
};
</script>

<style scoped>

</style>
