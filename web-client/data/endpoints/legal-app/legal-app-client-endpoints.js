//Client Controller

export const GetClient = (clientId) => {
  return `/api/legal-app-clients/client/${clientId}`;//GET
};
export const GetClientsBasicList = () => {
  return `/api/legal-app-clients/clients/basic-list`;//GET
};
export const GetClients = () => {
  return `/api/legal-app-clients/clients`;//GET
};
export const CreateClient = () => {
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

export const CreateContact = (clientId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact`;//POST
};
export const GetContacts = (clientId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contacts`;//GET
};
export const GetContact = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}`;//GET
};
export const CreateContactEmail = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/emails`;//POST
};
export const DeleteContactEmail = (clientId, contactId, itemId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/email/${itemId}`;//DELETE
};
export const CreateContactPhoneNumber = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/phone-number`;//POST
};
export const DeleteContactPhoneNumber = (clientId, contactId, itemId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/phone-number/${itemId}`;//DELETE
};
export const CreateContactPhysicalAddress = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/address`;//POST
};
export const DeleteContactPhysicalAddress = (clientId, contactId, itemId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}/address/${itemId}`;//DELETE
};
export const DeleteContact = (clientId, contactId) => {
  return `/api/legal-app-client-contacts/client/${clientId}/contact/${contactId}`;//DELETE
};
export const UpdateContact = (clientId, contactId) => {
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

export const GetWorkRecords = (clientId) => {
  return `/api/legal-app-clients-work/client/${clientId}/work-records`;//GET
};

export const CreateWorkRecord = (clientId) => {
  return `/api/legal-app-clients-work/client/${clientId}/work-record`;//POST
};

export const UpdateWorkRecord = (clientId, workRecordId) => {
  return `/api/legal-app-clients-work/client/${clientId}/work-record/${workRecordId}`;//PUT
};

export const DeleteWorkRecord = (clientId, workRecordId) => {
  return `/api/legal-app-clients-work/client/${clientId}/work-record/${workRecordId}`;//DELETE
};






























