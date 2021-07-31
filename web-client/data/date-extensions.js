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

  return dd + '/' + mm + '/' + yyyy;
};

export function formatDateWithHours(date) {
  let result = new Date(date);
  let options = {year: 'numeric', month: 'long', day: 'numeric', weekday: 'long'};
  return `${result.toLocaleDateString("pl-PL", options)}`;
}

export function formatDateToMonth(date) {
  let result = new Date(date);
  let options = {year: 'numeric', month: 'long'};
  return `${result.toLocaleDateString("pl-PL", options)}`;
}

export function addHoursToDate(date, hours) {
  return new Date(new Date(date).setHours(date.getHours() + hours));
}

export function formatDateForCalendar(date) {
  const eventDate = new Date(date).toUTCString();
  const month = (eventDate.getUTCMonth() + 1).padStart(2, '0');
  const day = (eventDate.getUTCDate()).padStart(2, '0');
  const year = eventDate.getUTCFullYear();
  return [year, month, day].join('-');

}

export function formatDateToLocaleTimeZone(date) {
  let d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear(),
    hours = d.getHours(),
    minutes = d.getMinutes();

  if (month.length < 2) {
    month = '0' + month;
  }
  if (day.length < 2) {
    day = '0' + day;
  }
  if (minutes < 10) {
    minutes = "0" + minutes;
  }
  return [year, month, day,].join('-') + " " + hours + ":" + minutes;
}

export function formatDateToLocaleTimeZoneWithoutTime(date) {
  let d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();
  if (month.length < 2) {
    month = '0' + month;
  }
  if (day.length < 2) {
    day = '0' + day;
  }
  return [year, month, day,].join('-');
}

export function todayDate() {
  return (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10);
}

export function queryDate(date) {
  let fromDate = (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10);
  let toDate = (addDays(date, 36000)).toISOString().substr(0, 10);
  return `?from=${fromDate}&to=${toDate}`;
}

export function queryDateExtended(date, addedDays) {
  let fromDate = (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10);
  let toDate = (addDays(date, addedDays)).toISOString().substr(0, 10);
  return `?from=${fromDate}&to=${toDate}`;
}

export function queryDateForFloatingBell(date) {
  let fromDate = (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10);
  let toDate = (addDays(date, 2)).toISOString().substr(0, 10);
  return `?from=${fromDate}&to=${toDate}`;
}


