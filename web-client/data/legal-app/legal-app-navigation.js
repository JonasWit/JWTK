export const legalappRoute = {
  items: [
    {
      id: '1',
      route: '/legal-application',
      name: 'Strona główna',
      icon: `mdi-home`,
      text: 'Strona główna',
      adminAccess: false
    },
    {
      id: '2',
      route: '/legal-application/clients',
      name: 'Klienci',
      icon: `mdi-account-tie`,
      text: 'Lista Twoich klientów',
      adminAccess: false
    },
    {
      id: '3',
      route: '/legal-application/reminders',
      name: 'Kalendarz',
      icon: 'mdi-calendar',
      text: 'Zaplanuj spotkania lub dodaj zadania',
      adminAccess: false
    },
    // {
    //   id: '3',
    //   route: '/legal-application/analytics',
    //   name: 'Statystyki',
    //   icon: 'mdi-chart-bar',
    //   text: 'Prześledź zestawienia',
    // },
    {
      id: '4',
      route: '/legal-application/archive',
      name: 'Archiwum Klientów',
      icon: 'mdi-archive',
      text: 'Lista zarchiwzowanych klinetów',
      adminAccess: true
    },
    {
      id: '5',
      route: '/legal-application/help',
      name: 'Pomoc',
      icon: 'mdi-help',
      text: 'Pomoc',
      adminAccess: false
    },
  ],
};
