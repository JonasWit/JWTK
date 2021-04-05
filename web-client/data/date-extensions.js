﻿export function addDays(date, days) {
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
  let options = {weekday: 'long', year: 'numeric', month: 'long', day: 'numeric'};
  return `${result.toLocaleDateString("pl-PL", options)} ${result.toLocaleTimeString("pl-PL")}`;
}