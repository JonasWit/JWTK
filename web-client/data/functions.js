export const hasOccurrences = (searchIndex, query) => {
  const queryParts = query.toLowerCase().split(' ');
  if (queryParts.length > 0) {
    return queryParts.map(x => searchIndex.indexOf(x) > -1).reduce((a, b) => a && b);
  }
  return true;
};

Date.prototype.addDays = function (days) {
  const date = new Date(this.valueOf());
  date.setDate(date.getDate() + days);
  return date;
};

Date.prototype.deductDays = function (days) {
  const date = new Date(this.valueOf());
  date.setDate(date.getDate() - days);
  return date;
};
