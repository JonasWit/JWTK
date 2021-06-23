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

export function formatDateForInvoice(date) {
  let result = new Date(date);
  let options = {year: 'numeric', month: 'short', day: 'numeric'};
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

export const timeStamp = () => {
  let today = new Date();
  let dd = String(today.getDate()).padStart(2, '0');
  let mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
  let yyyy = today.getFullYear();

  return today = dd + '/' + mm + '/' + yyyy;
};

export function formatDateWithHours(date) {
  let result = new Date(date);
  let options = {year: 'numeric', month: 'long', day: 'numeric', weekday: 'long'};
  return `${result.toLocaleDateString("pl-PL", options)}`;
};

export function formatDateToMonth(date) {
  let result = new Date(date);
  let options = {year: 'numeric', month: 'long'};
  return `${result.toLocaleDateString("pl-PL", options)}`;
}

