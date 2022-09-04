module.exports = {
  purge: ['./public/**/*.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode: 'class', // or 'media' or 'class'
  theme: {
    extend: {
      fontFamily: {
        body: ['Helvetica'],
        sans: ['Helvetica', 'sans-serif'],
      },
      colors: {
        customClassicBlue: '#34568B',
        customUltraViolet: '#6B5B95',
        customSerenity: '#92A8D1',
        customPrimaryViolet: '#3c2560',
        customPrimaryVioletLight: '#694e8e',
        customPrimaryVioletDark: '#140036',
        customSecondaryGold: '#ffd606',
        customSecondaryGoldLight: '#ffff54',
        customSecondaryGoldDark: '#c7a500',
      },

    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
