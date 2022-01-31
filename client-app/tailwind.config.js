module.exports = {
  purge: ['./public/**/*.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode: 'class', // or 'media' or 'class'
  theme: {
    extend: {
      fontFamily: {
        body: ['Nunito']
      },
      colors: {
        customClassicBlue: '#34568B',
        customUltraViolet: '#6B5B95',
        customSerenity: '#92A8D1',
      },
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
