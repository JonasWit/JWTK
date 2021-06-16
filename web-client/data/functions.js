import {round} from 'mathjs'

export const hasOccurrences = (searchIndex, query) => {
  const queryParts = query.toLowerCase().split(' ');
  if (queryParts.length > 0) {
    return queryParts.map(x => searchIndex.indexOf(x) > -1).reduce((a, b) => a && b);
  }
  return true;
};

export const vatRate = (vat) => {
  return (vat / 100);
};

export const vatAmount = (vat, amount) => {
  let vatAmountRounded = ((vat / 100) * amount)
  return round(vatAmountRounded, 2)
};


export const rateNet = (rate, vat) => {
  let netRounded = (rate - ((rate * (vat / 100))))
  console.log('rounded invoiceRateNet:', netRounded)
  return round(netRounded, 2)
};

export const amountNet = (amount, vat) => {
    let roundedAmount = ((amount - (amount * (vat / 100))))
    console.log('rounded amount:', roundedAmount)
    return round(roundedAmount, 2)
  }
;

