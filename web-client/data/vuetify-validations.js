export const emailRule = (message) => [
  v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || message
];

export const notEmptyRule = (message) => [
  v => !!v || message
];

export const dataAccessKeyRule = () => [
  v => !!v || "Key is required!",
  v => (v?.length >= 10 && v?.length <= 50) || "Between 10 and 50 characters!",
];

export const nameLengthRule = () => [
  v => !!v || "Nazwa nie może być pusta!",
  v => (v?.length >= 4 && v?.length <= 50) || "Dopuszczalna liczba znaków pomiędzy 4 a 50!",
]



