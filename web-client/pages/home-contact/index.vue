<template>
  <v-container class="mx-4">
    <h1 class="my-2">Zainteresowała Cię nasza oferta?</h1>
    <p class="font-weight-bold">Zapraszamy do kontaktu!</p>
    <p>Nasz adres email to: <a href="mailto:biuro@systemywp.pl">biuro@systemywp.pl</a></p>
    <p>Możesz również zostawić nam swój numer telefonu, a oddzwonimy w ciągu 24 godzin.</p>

    <v-form ref="contactForm" v-model="validation.valid">
      <v-text-field v-model="name" label="Imię" required :rules="validation.nameRules"></v-text-field>
      <v-text-field v-model="email" label="E-mail" :rules="validation.emailRules"></v-text-field>
      <v-text-field v-model="phone" label="Nr kontaktowy" required :rules="validation.phoneRules"></v-text-field>
      <v-checkbox v-model="checkbox"
                  label="Wyrażam zgodę na przetwarzanie moich danych osobowych i akceptuję Politykę prywatności"
                  required :rules="validation.checkboxRules" color="success"></v-checkbox>

      <v-btn :disabled="!valid" @click.prevent="submit" class="mr-4 mt-3" color="secondary">
        Wyślij
      </v-btn>
      <v-btn class="mt-3" @click="reset" color="primary">
        Wyczyść
      </v-btn>
    </v-form>

  </v-container>
</template>

<script>

export default {
  name: "home-contact",
  data: () => ({
    name: "",
    email: "",
    phone: "",
    checkbox: false,
    valid: true,
    validation: {

      nameRules: [
        v => !!v || 'Imię jest wymagane',
        v => (v?.length >= 3 && v?.length <= 20) || 'Imię musi mieć conajmniej 3 znaki i nie więcej niż 20 znaków.',
      ],
      emailRules: [
        v => !!v || 'E-mail jest wymagany.',
        v => /^(([^<>()[\]\\.,;:\s@']+(\.[^<>()\\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(v) || 'Format adresu e-mail jest niepoprawny.'],
      phoneRules: [
        v => /^\+(?:[0-9]•?){6,14}[0-9]$/.test(v) || 'Podaj numer telefonu w formacie bez spacji np. +48676676676.',
        v => (v && v.length) >= 12 || 'Minimalna liczba znaków to 12 '],
      checkboxRules: [
        v => !!v || 'Proszę wyrazić zgodę, aby kontynuować.'
      ],

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

</style>
