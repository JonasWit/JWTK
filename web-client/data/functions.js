export const hasOccurrences = (searchIndex, query) => {
  const queryParts = query.toLowerCase().split(' ');
  if (queryParts.length > 0) {
    return queryParts.map(x => searchIndex.indexOf(x) > -1).reduce((a, b) => a && b);
  }
  return true;
};

export const vatAmount = (rate, vat) => {
  return (Math.round((rate * vat) / ((100 + vat) / 100)).toFixed(2));
};

export const vatRate = (vat) => {
  return Math.round(vat / 100);

};

export const rateNet = (rate, vat) => {
  return Math.round(rate - ((rate * vat) / ((100 + vat)) / 100)).toFixed(2)
};

export const amountNet = (amount, vat) => {
  return Math.round(amount - ((amount * (vat / 100)) / ((100 + vat) / 100))).toFixed(2);
};

