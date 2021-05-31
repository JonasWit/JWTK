export const portalAdminRoutes = {
  items: [
    {
      id: '1',
      route: '/admin-panel/users',
      name: 'Users',
      icon: `mdi-account-group`,
      text: 'Users Management',
    },
    {
      id: '2',
      route: '/admin-panel/legal-app-keys',
      name: 'Legal App Keys',
      icon: `mdi-key-change`,
      text: 'Create, Update, Edit Access Keys',
    },
    {
      id: '3',
      route: '/admin-panel/medical-app-keys',
      name: 'Medical App Keys',
      icon: `mdi-key-change`,
      text: 'Create, Update, Edit Access Keys',
    },
    {
      id: '4',
      route: '/admin-panel/log-api',
      name: 'API Log',
      icon: 'mdi-math-log',
      text: 'Logs from API application'
    },
    {
      id: '5',
      route: '/admin-panel/log-portal',
      name: 'Portal Log',
      icon: 'mdi-math-log',
      text: 'Logs from Portal actions',
    },
  ],
};
