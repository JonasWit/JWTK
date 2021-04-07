const initState = () => ({});

export const state = initState;

export const actions = {
  error({commit}, message) {
    console.log('Error from API', message);
  },
  success({commit}, message) {
    console.log('Success from API', message);
  },
};
