//logs
export const getPortalLogs = (query) => {
  return `/api/portal-admin/log-admin/logs/split${query}`;
};
export const getAPILogs = (query) => {
  return `/api/portal-admin/log-admin/logs/server/split${query}`;
};

//Users
export const getCurrentUser = () => {
  return '/api/users/me';
};
export const getUsers = () => {
  return '/api/portal-admin/user-admin/users';
};
export const getLegalAppRelatedUsers = () => {
  return '/api/legal-app-admin/general/all-related-users';
};
export const getMedicalAppRelatedUsers = () => {
  return '/api/medical-app-admin/general/all-related-users';
};
export const getRestaurantAppRelatedUsers = () => {
  return '/api/restaurant-app-admin/general/all-related-users';
};
export const lockUser = () => {
  return '/api/portal-admin/user-admin/user/lock';
};
export const unlockUser = () => {
  return '/api/portal-admin/user-admin/user/unlock';
};
export const changeRole = () => {
  return '/api/portal-admin/user-admin/user/change-role';
};

//App Access
export const grantLegalAppAccessEndpoint = () => {
  return '/api/portal-admin/user-admin/user/grant/legal-app';
};
export const revokeLegalAppAccessEndpoint = () => {
  return '/api/portal-admin/user-admin/user/revoke/legal-app';
};
export const grantMedicalAppAccessEndpoint = () => {
  return '/api/portal-admin/user-admin/user/grant/medical-app';
};
export const revokeMedicalAppAccessEndpoint = () => {
  return '/api/portal-admin/user-admin/user/revoke/medical-app';
};
export const grantRestaurantAppAccessEndpoint = () => {
  return '/api/portal-admin/user-admin/user/grant/restaurant-app';
};
export const revokeRestaurantAppAccessEndpoint = () => {
  return '/api/portal-admin/user-admin/user/revoke/restaurant-app';
};

//Keys
export const grantAccessKey = (keyType) => {
  return `/api/portal-admin/key-admin/${keyType}/user/grant/access-key`;
};
export const updateAccessKey = (keyType, id) => {
  return `/api/portal-admin/key-admin/${keyType}/access-key/update/${id}`;
};
export const deleteAccessKey = (keyType, id) => {
  return `/api/portal-admin/key-admin/${keyType}/access-key/delete/${id}`;
};

export const revokeLegalAppKey = () => {
  return '/api/portal-admin/key-admin/legal-app/access-key/revoke';
};
export const revokeMedicalAppKey = () => {
  return '/api/portal-admin/key-admin/medical-app/access-key/revoke';
};
export const revokeRestaurantAppKey = () => {
  return '/api/portal-admin/key-admin/restaurant-app/access-key/revoke';
};

export const getLegalAppKeys = () => {
  return '/api/portal-admin/key-admin/legal-app/access-keys';
};
export const getMedicalAppKeys = () => {
  return '/api/portal-admin/key-admin/medical-app/access-keys';
};
export const getRestaurantAppKeys = () => {
  return '/api/portal-admin/key-admin/restaurant-app/access-keys';
};

