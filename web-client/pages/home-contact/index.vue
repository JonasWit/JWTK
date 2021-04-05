<template>
  <v-container>
    <h1>Zainteresowała Cię nasza oferta?</h1>
    <p class="font-weight-bold">Zapraszamy do kontaktu!</p>
    <p>Nasz adres email to: <a href="mailto:biuro@systemywp.pl">biuro@systemywp.pl</a></p>
    <p>Możesz również zostawić nam swój numer telefonu, a oddzwonimy w ciągu 24 godzin.</p>

    <form>
      <v-text-field
        v-model="name"
        :error-messages="nameErrors"
        :counter="10"
        label="Imię"
        required
        @input="$v.name.$touch()"
        @blur="$v.name.$touch()"
      ></v-text-field>
      <v-text-field
        v-model="email"
        :error-messages="emailErrors"
        label="E-mail"
        required
        @input="$v.email.$touch()"
        @blur="$v.email.$touch()"
      ></v-text-field>
      <v-text-field
        v-model="phone"
        :error-messages="emailErrors"
        label="Nr kontaktowy"
        required
        @input="$v.phone.$touch()"
        @blur="$v.phone.$touch()"
      ></v-text-field>
      <v-checkbox
        v-model="checkbox"
        :error-messages="checkboxErrors"
        label="Wyrażam zgodę na przetwarzanie moich danych osobowych i akceptuję Politykę prywatności"
        required
        @change="$v.checkbox.$touch()"
        @blur="$v.checkbox.$touch()"
      ></v-checkbox>

      <v-btn
        class="mr-4"
        @click="submit"
      >
        Wyślij
      </v-btn>
      <v-btn @click="clear">
        Wyczyść
      </v-btn>
    </form>

  </v-container>
</template>

<script>

export default {
  name: "home-contact",


};
</script>

<style scoped>

</style>
