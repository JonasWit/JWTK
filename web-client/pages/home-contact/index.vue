<template>
  <v-container>
    <h1>Zainteresowała Cię nasza oferta?</h1>
    <p class="font-weight-bold">Zapraszamy do kontaktu!</p>
    <p>Nasz adres email to: <a href="mailto:biuro@systemywp.pl">biuro@systemywp.pl</a></p>
    <p>Możesz również zostawić nam swój numer telefonu, a oddzwonimy w ciągu 24 godzin.</p>

    <form ref="form">
      <v-text-field v-model="name" label="Imię" required :rules="validation.nameRules"></v-text-field>
      <v-text-field v-model="email" label="E-mail" :rules="validation.emailRules"></v-text-field>
      <v-text-field v-model="phone" label="Nr kontaktowy" required :rules="validation.phoneRules"></v-text-field>
      <v-checkbox v-model="checkbox"
                  label="Wyrażam zgodę na przetwarzanie moich danych osobowych i akceptuję Politykę prywatności"
                  required :rules="validation.checkboxRules"></v-checkbox>

      <v-btn class="mr-4 mt-3" @click.stop.prevent="submit">
        Wyślij
      </v-btn>
      <v-btn class="mt-3" @click="reset">
        Wyczyść
      </v-btn>
    </form>

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
    validation: {
      nameRules: [
        v => !!v || 'Imię jest wymagane',
        v => v.length >= 3 || 'Minimalna liczba znaków to 3.',
        v => v.length <= 20 || 'Malsymalna liczba znaków to 20.',

      ],
      emailRules: [
        v => !!v || 'E-mail jest wymagany.',
        v => /^(([^<>()[\]\\.,;:\s@']+(\.[^<>()\\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(v) || 'Format adresu e-mail jest niepoprawny.'],
      phoneRules: [
        v => /^\+?([0-9]{2})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$/.test(v) || 'Podaj numer telefonu w formacie +48 676 676 676.',
        v => v.length >= 9 || 'Minimalna liczba znaków to 10 '],
      checkboxRules: [
        v => !!v || 'Proszę wyrazić zgodę, aby kontynuować.'
      ],

    }
  }),
  methods: {
    submit() {
      if (!this.$refs.form.validate()) return;

      if (this.loading) return;
      this.loading = true;

      const payload = {
        name: this.name,
        email: this.email,
        phone: this.phone
      };

      return this.$axios.$post("/api/portal/contact/request", payload)
        .catch((e) => {
          console.log('error - contact form', e)
        }).finally(() => {
          this.loading = false;
        });
    },
    reset() {
      this.name = '',
        this.email = '',
        this.phone = '',
        this.checkbox = false;

      // this.$refs.createDataAccessKeyForm.reset();
      // this.$refs.createDataAccessKeyForm.resetValidation();
    }
  }
}
;
</script>

<style scoped>

</style>
