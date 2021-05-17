const initState = () => ({});

export const state = initState;

export const actions = {
  error({commit}, message) {
    console.error('!!!Error from API!!!', message);
  },
  success({commit}, message) {
    console.error('!!!Success from API!!!', message);
  },
};
