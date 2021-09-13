export const emailRule = () => [
  v => !!v || 'E-mail jest wymagany.',
  v => /^(([^<>()[\]\\.,;:\s@']+(\.[^<>()\\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(v) || 'Format adresu e-mail jest niepoprawny.'
];

export const notEmptyRule = (message) => [
  v => !!v || message
];
export const selectOption = (message) => [
  v => v?.length === 0 || message

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
  v => /^[0-9]+$/.test(v) || 'Dozwolone tylko liczby!',
  v => (v?.length >= 7 && v?.length <= 20) || 'Dopuszcalna liczba znaków pomiędzy 7, a 20',
];

export const postalCode = () => [
  // v => /[0-9]{2}-[0-9]{3}/.test(v) || 'Podaj kod pocztowy w poprawnym formacie np. 33-322',
  v => v?.length <= 6 || 'Dozowlona liczba znaków to 6 '

];

export const hoursValidation = () => [
  v => !!v || "Pole obowiązkowe.",
  v => /^[0-9]+$/.test(v) || 'Dozwolone tylko liczby!',
  v => (v && v <= 360) || "Maksymalna liczba godzin nie może przekraczać 360 godzin",


];
export const minutesValidation = () => [
  v => !!v || "Pole obowiązkowe.",
  v => /^[0-9]+$/.test(v) || 'Dozwolone tylko liczby!',
  v => (v && v <= 59) || "Maksymalna liczba minut nie może przekraczać 59 minut",


];

export const dateValidation = () => [
  v => v >= (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10) || "Data terminu nie może być w przeszłości",
];



