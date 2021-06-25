//Client Controller

export const getArchivedClient = () => {
  return `/api/legal-app-clients/clients/archive`;//GET
};
export const GetClient = (clientId) => {
  return `/api/legal-app-clients/client/${clientId}`;//GET
};
export const getClientsBasicList = () => {
  return `/api/legal-app-clients/clients/basic-list`;//GET
};
export const getClients = () => {
  return `/api/legal-app-clients/clients`;//GET
};
export const createClient = () => {
  return `/api/legal-app-clients/create`;//POST
};
export const UpdateClient = (clientId) => {
  return `/api/legal-app-clients/update/${clientId}`;//PUT
};
export const ArchiveClient = (clientId) => {
  return `/api/legal-app-clients/archive/${clientId}`;//PUT
};
export const DeleteClient = (clientId) => {
  return `/api/legal-app-clients/delete/${clientId}`;//DELETE
};

//Client Access Controller

export const GetClientAllowedUsers = (clientId) => {
  return `/api/legal-app-client-access/client/${clientId}/allowed-users`;//GET
};
export const GrantAccess = (clientId, userId) => {
  return `/api/legal-app-client-access/client/${clientId}/grant-access/${userId}`;//POST
};
export const RevokeAccess = (clientId, userId) => {
  return `/api/legal-app-client-access/client/${clientId}/revoke-access/${userId}`;//POST
};
export const GetClientEligibleUsers = (clientId) => {
  return `/api/legal-app-client-access/client/${clientId}/eligible-users`;//GET
};

//Client Contacts Controller

export const createContact = (clientId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact`;//POST
};
export const getContacts = (clientId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contacts`;//GET
};
export const GetContact = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}`;//GET
};
export const createContactEmail = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/emails`;//POST
};
export const deleteContactEmail = (clientId, contactId, itemId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/email/${itemId}`;//DELETE
};
export const createContactPhoneNumber = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/phone-number`;//POST
};
export const deleteContactPhoneNumber = (clientId, contactId, itemId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/phone-number/${itemId}`;//DELETE
};
export const createContactPhysicalAddress = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/address`;//POST
};
export const deleteContactPhysicalAddress = (clientId, contactId, itemId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/address/${itemId}`;//DELETE
};
export const deleteContact = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}`;//DELETE
};
export const updateContact = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}`;//PUT
};

//Client Notes Controller

export const getNotesTitlesList = (clientId) => {
  return `/api/legal-app-clients-notes/client/${clientId}/notes/titles-list`;//GET
};
export const getNote = (clientId, noteId) => {
  return `/api/legal-app-clients-notes/client/${clientId}/note/${noteId}`;//GET
};
export const createNoteForClient = (clientId) => {
  return `/api/legal-app-clients-notes/client/${clientId}/note`;//POST
};
export const updateNote = (clientId, noteId) => {
  return `/api/legal-app-clients-notes/client/${clientId}/note/${noteId}`;//PUT
};
export const deleteNote = (clientId, noteId) => {
  return `/api/legal-app-clients-notes/client/${clientId}/note/${noteId}`;//DELETE
};

//Client Work Controller

export const getWorkRecords = (clientId) => {
  return `/api/legal-app-clients-work/client/${clientId}/work-records`;//GET
};

export const createWorkRecord = (clientId) => {
  return `/api/legal-app-clients-work/client/${clientId}/work-record`;//POST
};

export const updateWorkRecord = (clientId, workRecordId) => {
  return `/api/legal-app-clients-work/client/${clientId}/work-record/${workRecordId}`;//PUT
};

export const deleteWorkRecord = (clientId, workRecordId) => {
  return `/api/legal-app-clients-work/client/${clientId}/work-record/${workRecordId}`;//DELETE
};

// Billing data
export const addBillingData = () => {
  return `/api/legal-app-billing/create`;//POST
};

export const deleteBillingData = (billingRecordId) => {
  return `/api/legal-app-billing/delete/${billingRecordId}`;//DELETE
};

export const updateBillingData = (billingRecordId) => {
  return `/api/legal-app-billing/update/${billingRecordId}`;//PUT
};





























