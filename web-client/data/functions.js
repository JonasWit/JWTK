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

export const groupByKey = (input, key) => {
  return input.reduce((acc, currentValue) => {
    let groupKey = currentValue[key];
    if (!acc[groupKey]) {
      acc[groupKey] = [];
    }
    acc[groupKey].push(currentValue);
    return acc;
  }, {});
};

export function handleError(error) {
  if (error.response.data) {
    console.log(error.response.data);
  } else {
    console.log(error)
  }
}

export function countNotifications(events, deadlines) {
  let eventsCount = events.length
  let deadlinesCount = deadlines.length
  return eventsCount + deadlinesCount
}

