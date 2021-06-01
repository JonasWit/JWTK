export const emailRule = (message) => [
  v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || message
];

export const notEmptyRule = (message) => [
  v => !!v || message
];

export const notEmptyAndLimitedRule = (message, min, max) => [
  v => !!v || message,
  v => (v?.length >= min && v?.length <= max) || message,
];



