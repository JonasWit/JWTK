export function addDays(date, days) {
  let result = new Date(date);
  result.setDate(result.getDate() + days);
  return result;
}

export function deductDays(date, days) {
  let result = new Date(date);
  result.setDate(result.getDate() - days);
  return result;
}

export function deductYears(date, years) {
  return new Date(result.getFullYear() - years, result.getMonth(), result.getDay());
}

export function addYears(date, years) {
  return new Date(result.getFullYear() + years, result.getMonth(), result.getDay());
}

export function formatDate(date) {
  let result = new Date(date);
  let options = {year: 'numeric', month: 'long', day: 'numeric'};
  return `${result.toLocaleDateString("pl-PL", options)}`;
}

export const colorDate = (date) => {
  let today = new Date();
  const expireDate = Date.parse(date);

  if (expireDate > addDays(today, 7)) {
    return "success--text";
  }
  if (expireDate < addDays(today, 7) && expireDate > today) {
    return "warning--text";
  }
  if (expireDate < today) {
    return "error--text";
  }
  return "error--text";
};
