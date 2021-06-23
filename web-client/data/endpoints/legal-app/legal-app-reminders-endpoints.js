//Reminders Controller

export const getReminders = () => {
  return `/api/legal-app-reminders/list`;//GET
};

export const getRemindersFromTo = () => {
  return `/api/legal-app-reminders/list/limit`;//GET
};

export const getReminder = (reminderId) => {
  return `/api/legal-app-reminders/reminder/${reminderId}`;//GET
};

export const createReminder = () => {
  return `/api/legal-app-reminders/reminder`;//POST
};

export const updateReminder = (reminderId) => {
  return `/api/legal-app-reminders/reminder/${reminderId}`;//PUT
};

export const deleteReminder = (reminderId) => {
  return `/api/legal-app-reminders/reminder/${reminderId}`;//DELETE
};