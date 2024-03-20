/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{js,jsx,ts,tsx}'],
  theme: {
    screens: {
      sm: '480px',
      md: '768px',
      lg: '976px',
      xl: '1440px',
    },
    colors: {
      green: '#00CC5F',
      red: '#FF3C38',
      blue: '#4E73A6',
      'gray-dark': '#273444',
      gray: '#8492a6',
      'background': '#222222',
      'gray-light': '#e9edf2',
      white: '#fff',
    },
    fontFamily: {
      sans: ['Helvetica Neue', 'sans-serif'],
      serif: ['Palatino', 'serif'],
    },
  },
  plugins: [],
};