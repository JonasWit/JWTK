//Case Controller

export const getArchivedCases = (clientId) => {
  return `/api/legal-app-cases/client/${clientId}/cases/archive`;//GET
};
export const getCases = (clientId) => {
  return `/api/legal-app-cases/client/${clientId}/cases`;//GET
};
export const getCase = (caseId) => {
  return `/api/legal-app-cases/case/${caseId}`;//GET
};
export const createCase = (clientId) => {
  return `/api/legal-app-cases/client/${clientId}/case`;//POST
};
export const updateCase = (caseId) => {
  return `/api/legal-app-cases/case/${caseId}`;//PUT
};
export const deleteCase = (caseId) => {
  return `/api/legal-app-cases/case/${caseId}`;//DELETE
};
export const archiveCase = (caseId) => {
  return `/api/legal-app-cases/archive/case/${caseId}`;//PUT
};

//Case Access Controller

export const getClientAllowedUsers = (caseId) => {
  return `/api/legal-app-case-access/case/${caseId}/allowed-users`;//GET
};
export const grantAccess = (caseId) => {
  return `/api/legal-app-case-access/case/${caseId}/grant-access`;//POST
};
export const revokeAccess = (caseId) => {
  return `/api/legal-app-case-access/case/${caseId}/revoke-access`;//POST
};
export const getClientEligibleUsers = (caseId) => {
  return `/api/legal-app-case-access/case/${caseId}/eligible-users`;//GET
};

//Case Deadlines Controller

export const getDeadlines = (caseId) => {
  return `/api/legal-app-cases/deadlines/case/${caseId}/list`;//GET
};
export const createDeadline = (caseId) => {
  return `/api/legal-app-cases/deadlines/case/${caseId}`;//POST
};
export const deleteDeadline = (caseId, deadlineId) => {
  return `/api/legal-app-cases/deadlines/case/${caseId}/item/${deadlineId}/delete`;//DELETE
};

//Case Notes Controller

export const getNotes = (caseId) => {
  return `/api/legal-app-cases-notes/case/${caseId}/list`;//GET
};
export const getNote = (caseId, noteId) => {
  return `/api/legal-app-cases-notes/case/${caseId}/note/${noteId}`;//GET
};
export const createNote = (caseId) => {
  return `/api/legal-app-cases-notes/case/${caseId}`;//POST
};
export const deleteNote = (caseId, noteId) => {
  return `/api/legal-app-cases-notes/case/${caseId}/note/${noteId}`;//DELETE
};
export const updateNote = (caseId, noteId) => {
  return `/api/legal-app-cases-notes/case/${caseId}/note/${noteId}`;//PUT
};
