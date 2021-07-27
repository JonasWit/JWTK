<template>
  <v-container fluid>
    <section>
      <div class="content white--text text-center d-flex align-center">
        <div>
          <h1>Zainteresowała Cię nasza oferta?</h1>
          <h5>
            Zapraszamy do kontaktu!
          </h5>
          <h5>Nasz adres email to: <a href="mailto:biuro@systemywp.pl">biuro@systemywp.pl</a></h5>
          <h5>Możesz również zostawić nam swój numer telefonu, a oddzwonimy w ciągu 24 godzin.</h5>
        </div>
      </div>
    </section>
    <v-container>
      <v-form ref="contactForm" v-model="validation.valid" class="my-7">
        <v-card elevation="15" class="py-7 px-7">
          <v-text-field v-model="name" label="Imię" required :rules="validation.nameRules"></v-text-field>
          <v-text-field v-model="email" label="E-mail" :rules="validation.emailRules"></v-text-field>
          <v-text-field v-model="phone" label="Nr kontaktowy" required :rules="validation.phoneRules"></v-text-field>
          <v-checkbox v-model="checkbox"
                      label="Wyrażam zgodę na przetwarzanie moich danych osobowych i akceptuję Politykę prywatności"
                      required :rules="validation.checkboxRules" color="success"></v-checkbox>
          <v-card-actions>
            <v-btn class="mt-3" @click="reset" outlined color="warning">
              Wyczyść
            </v-btn>
            <v-spacer></v-spacer>
            <v-btn :disabled="!valid" @click.prevent="submit" class="mr-4 mt-3" outlined color="error">
              Wyślij
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-form>
    </v-container>
  </v-container>
</template>

<script>

import {emailRule, notEmptyAndLimitedRule, notEmptyRule, phoneNumberRule} from "../../../data/vuetify-validations";

export default {
  name: "home-contact",
  data: () => ({
    name: "",
    email: "",
    phone: "",
    checkbox: false,
    valid: true,
    validation: {

      nameRules: notEmptyAndLimitedRule("Nazwa jest wymagana. Dozwolona liczba znaków pomiędzy 4, a 50", 4, 50),
      emailRules: emailRule(),
      phoneRules: phoneNumberRule(),
      checkboxRules: notEmptyRule("Proszę wyrazić zgodę, aby kontynuować")
    }
  }),
  methods: {
    submit() {
      if (!this.$refs.contactForm.validate()) return;
      console.warn("Sending contact form");
      if (this.loading) return;
      this.loading = true;

      const payload = {
        name: this.name,
        email: this.email,
        phone: this.phone
      };

      return this.$axios.$post("/api/portal/contact/request", payload)
        .then((result) => {
          console.log("SUCCESS!", result);
          this.$notifier.showSuccessMessage("Forma wysłana pomyślnie!");
          this.loading = false;
          this.$refs.contactForm.reset();
        })
        .catch((e) => {
          console.log('error - contact form', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj ponownie.");
        }).finally(() => {
          this.loading = false;
        });
    },
    reset() {
      this.$refs.contactForm.reset();
      // this.$refs.createDataAccessKeyForm.reset();
      // this.$refs.createDataAccessKeyForm.resetValidation();
    }
  }
}
;
</script>

<style scoped>
h1 {
  font-size: 65px;
  font-weight: 800 !important;
  line-height: 60px;
  padding: 10px 0;
  letter-spacing: -.03em !important;
}

h5 {
  font-size: 25px;
  font-weight: 400 !important;
  padding: 10px 0;
  margin-left: 10px;
}

@media only screen and (max-width: 780px) {
  .content h1 {
    font-size: 30px;
    line-height: 30px;
  }

  .content h5 {
    font-size: 15px;
    padding: 2px 0px;
  }
}

section {
  position: relative;
  width: 100%;
  height: 45vh;
  display: flex;
  justify-content: center;
  /*align-items: center;*/
  overflow: hidden;
  /*background-color: azure;*/
  /*background: linear-gradient(45deg, midnightblue, #000000)*/
}

section .content {
  position: relative;
  z-index: 1;
  text-align: left;
  margin-top: 25px;

}

section:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  /*background: black;*/
  background: linear-gradient(45deg, cornflowerblue, #000000);
  border-radius: 0 0 50% 50%/0 0 100% 100%;
  transform: scaleX(1.5);
}

</style>
