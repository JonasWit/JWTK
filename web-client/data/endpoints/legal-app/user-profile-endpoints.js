//User-profile Controller

export const deleteUser = (profileId) => {
  return `/api/users/personal-data/clear/${profileId}`;//DELETE
};

export const editPersonalData = () => {
  return "/api/users/personal-data/update"; //PUT
};

export const revokeLegalAppUser = () => {
  return '/api/portal-admin/key-admin/legal-app/access-key/revoke'; //POST
};

export const revokeMedAppUser = () => {
  return '/api/portal-admin/key-admin/medical-app/access-key/revoke'; //POST
};

export const revokeRestaurantAppUser = () => {
  return '/api/portal-admin/key-admin/restaurant-app/access-key/revoke';//POST
};

