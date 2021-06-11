export const emailRule = () => [
  v => !!v || 'E-mail jest wymagany.',
  v => /^(([^<>()[\]\\.,;:\s@']+(\.[^<>()\\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(v) || 'Format adresu e-mail jest niepoprawny.'
];

export const notEmptyRule = (message) => [
  v => !!v || message
];

export const notEmptyAndLimitedRule = (message, min, max) => [
  v => !!v || message,
  v => (v?.length >= min && v?.length <= max) || message,
];

export const numberOnly = () => [
  v => /^[0-9]+$/.test(v) || 'Dozwolone tylko liczby!',
];

export const lengthRule = (message, min, max) => [
  v => (v?.length >= min && v?.length <= max) || message,

];

export const phoneNumberRule = () => [
  v => /^\+(?:[0-9]•?){6,14}[0-9]$/.test(v) || 'Podaj numer telefonu w formacie bez spacji np. +48676676676.',
  v => (v && v.length) >= 12 || 'Minimalna liczba znaków to 12 '
];

export const postalCode = () => [
  v => /[0-9]{2}-[0-9]{3}/.test(v) || 'Podaj kod pocztowy w poprawnym formacie np. 33-322',
  v => (v && v.length) === 6 || 'Dozowlona liczba znaków to 6 '

];
